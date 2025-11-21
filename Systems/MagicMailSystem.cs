// Systems/MagicPostMasterSystem.cs
// Main ECS system that tweaks postal facility capacities, van payloads,
// and handles optional mail overflow cleanup.
// Also exposes city-wide mail stats via MailAccumulationSystem.

namespace MagicPostMaster
{
    using System.Collections.Generic;
    using Colossal.Entities;
    using Game;
    using Game.Common;
    using Game.Economy;
    using Game.Prefabs;
    using Game.Simulation;
    using Game.Tools;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;

    /// <summary>
    /// Simulation system that adjusts post office / sorting facility capacities,
    /// post van mail payloads, and optional overflow cleanup.
    /// Also reads city-wide mail stats from MailAccumulationSystem.
    /// </summary>
    public partial class MagicMailSystem : GameSystemBase
    {
        private EntityQuery m_PostFacilitiesQuery;
        private EntityQuery m_PostVanPrefabsQuery;

        // Baselines so we can scale relative to vanilla and support clean resets.
        private Dictionary<Entity, FacilityBaseline> m_FacilityBaselines = null!;
        private Dictionary<Entity, int> m_PostVanMailBaselines = null!;

        private struct FacilityBaseline
        {
            public int PostVanCapacity;
            public int PostTruckCapacity;
            public int MailCapacity;
            public int SortingRate;
        }

        // ---- CITY-WIDE MAIL STATS (from MailAccumulationSystem) ----

        private MailAccumulationSystem? m_MailAccumulationSystem;

        internal static int s_LastCityAccumulatedMail;
        internal static int s_LastCityProcessedMail;

        // ---- STATUS FIELDS (read by Settings.Status* properties) ----

        internal static int s_LastFacilityCount;
        internal static int s_LastPostOfficeCount;
        internal static int s_LastSortingFacilityCount;
        internal static int s_LastPostVanCapacityTotal;
        internal static int s_LastPostTruckCapacityTotal;
        internal static int s_LastPostOfficeGets;
        internal static int s_LastSortingGets;
        internal static int s_LastOverflowClamps;

        /// <summary>
        /// Returns a short summary of the last facility scan.
        /// </summary>
        public static string GetStatusSummary()
        {
            if (s_LastFacilityCount == 0)
            {
                return "No postal facilities processed yet. Open a city and let the simulation run.";
            }

            return
                $"{s_LastPostOfficeCount} post offices | " +
                $"{s_LastPostVanCapacityTotal} post-vans | " +
                $"{s_LastSortingFacilityCount} sort buildings | " +
                $"{s_LastPostTruckCapacityTotal} post trucks";
        }

        /// <summary>
        /// Returns a short summary of the last update activity.
        /// </summary>
        public static string GetStatusActivity()
        {
            if (s_LastFacilityCount == 0)
            {
                return "No activity recorded yet.";
            }

            return
                $"{s_LastPostOfficeGets} local-mail pulls | " +
                $"{s_LastSortingGets} unsorted-mail pulls | " +
                $"{s_LastOverflowClamps} overflow cleanups";
        }

        /// <summary>
        /// Returns a summary of city-wide mail accumulation/processing
        /// from the vanilla MailAccumulationSystem.
        /// </summary>
        public static string GetStatusCityMail()
        {
            if (s_LastCityAccumulatedMail == 0 && s_LastCityProcessedMail == 0)
            {
                return "City mail stats not available yet. Open a city and let the simulation run.";
            }

            // Slightly widened spacing after "Monthly".
            return
                $"Monthly   {s_LastCityAccumulatedMail,8:N0} accumulated | " +
                $"{s_LastCityProcessedMail:N0} processed";
        }

        /// <summary>
        /// Controls how often the system updates for each phase.
        /// </summary>
        public override int GetUpdateInterval(SystemUpdatePhase phase)
        {
#if DEBUG
            // Matching the vanilla PostFacilityAISystem interval for easier debugging.
            return 256;
#else
            // 32 updates per day ≈ once per 45 in-game minutes.
            return 262144 / 32;
#endif
        }

        /// <summary>
        /// Controls when the system runs relative to other systems.
        /// </summary>
        public override int GetUpdateOffset(SystemUpdatePhase phase)
        {
            // Keeps this system in a safe slot before the vanilla system.
            return 48;
        }

        /// <summary>
        /// Creates the system and builds the entity queries.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();

            m_FacilityBaselines = new Dictionary<Entity, FacilityBaseline>();
            m_PostVanMailBaselines = new Dictionary<Entity, int>();

            // Query for all operational post facilities with a resource buffer.
            m_PostFacilitiesQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[]
                {
                    ComponentType.ReadOnly<PrefabRef>(),
                    ComponentType.ReadOnly<Game.Buildings.PostFacility>(),
                    ComponentType.ReadWrite<Resources>(),
                },
                None = new[]
                {
                    ComponentType.ReadOnly<Destroyed>(),
                    ComponentType.ReadOnly<Deleted>(),
                    ComponentType.ReadOnly<Temp>(),
                },
            });

            // Query for post van prefabs (we only need PostVanData).
            m_PostVanPrefabsQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[]
                {
                    ComponentType.ReadWrite<PostVanData>(),
                },
            });

            RequireForUpdate(m_PostFacilitiesQuery);

            // Try to grab the vanilla MailAccumulationSystem so we can surface its stats.
            TryResolveMailAccumulationSystem();

            Mod.s_Log.Info("MagicPostMasterSystem created.");
        }

        /// <summary>
        /// Per-update simulation logic for all post facilities.
        /// </summary>
        protected override void OnUpdate()
        {
            Setting? settings = Mod.Settings;
            if (settings == null)
            {
                return;
            }

            EntityManager entityManager = EntityManager;

            // Toggle: when false, facility capacities/sorting are forced to vanilla.
            bool changeCapacity = settings.ChangeCapacity;

            // Clamp user inputs defensively.
            var vanMailPercent = math.clamp(settings.PostVanMailLoadPercentage, 100, 500);
            var vanFleetPercent = math.clamp(settings.PostVanFleetSizePercentage, 50, 300);
            var truckFleetPercent = math.clamp(settings.TruckCapacityPercentage, 50, 300);
            var sortingSpeedPercent = math.clamp(settings.PSF_SortingSpeedPercentage, 50, 500);
            var sortingStoragePercent = math.clamp(settings.PSF_StorageCapacityPercentage, 50, 300);
            bool fixOverflow = settings.FixMailOverflow;

            // Apply per-van mail capacity scaling to all post van prefabs
            // (independent of facility capacity toggle).
            ApplyPostVanStats(entityManager, vanMailPercent);

            using NativeArray<Entity> postEntities = m_PostFacilitiesQuery.ToEntityArray(Allocator.Temp);
            int facilityCount = postEntities.Length;
            int postOfficeCount = 0;
            int sortingFacilityCount = 0;
            int postOfficeGets = 0;
            int sortingGets = 0;
            int overflowClamps = 0;
            int totalPostVanCapacity = 0;
            int totalPostTruckCapacity = 0;

#if DEBUG
            Mod.s_Log.Info($"MagicPostMasterSystem.OnUpdate: {facilityCount} post facilities");
#endif

            foreach (Entity postEntity in postEntities)
            {
                if (!entityManager.TryGetComponent(postEntity, out PrefabRef prefabRef))
                {
                    Mod.s_Log.Warn($"Failed to retrieve PrefabRef for {postEntity}.");
                    continue;
                }

                Entity prefab = prefabRef.m_Prefab;

                if (!entityManager.TryGetComponent(prefab, out PostFacilityData postFacilityData))
                {
                    Mod.s_Log.Warn($"Failed to retrieve PostFacilityData for prefab {prefab}.");
                    continue;
                }

                // Capture vanilla baselines for this prefab once.
                if (!m_FacilityBaselines.TryGetValue(prefab, out FacilityBaseline baseline))
                {
                    baseline = new FacilityBaseline
                    {
                        PostVanCapacity = postFacilityData.m_PostVanCapacity,
                        PostTruckCapacity = postFacilityData.m_PostTruckCapacity,
                        MailCapacity = postFacilityData.m_MailCapacity,
                        SortingRate = postFacilityData.m_SortingRate,
                    };
                    m_FacilityBaselines[prefab] = baseline;
                }

                bool facilityChanged = false;

                int targetVanCapacity = baseline.PostVanCapacity;
                int targetTruckCapacity = baseline.PostTruckCapacity;
                int targetSortingRate = baseline.SortingRate;
                int targetMailCapacity = baseline.MailCapacity;

                if (changeCapacity)
                {
                    if (baseline.PostVanCapacity > 0)
                    {
                        targetVanCapacity = (int)math.round(baseline.PostVanCapacity * vanFleetPercent / 100f);
                        targetVanCapacity = math.max(0, targetVanCapacity);
                    }

                    if (baseline.PostTruckCapacity > 0)
                    {
                        targetTruckCapacity = (int)math.round(baseline.PostTruckCapacity * truckFleetPercent / 100f);
                        targetTruckCapacity = math.max(0, targetTruckCapacity);
                    }

                    if (baseline.SortingRate > 0)
                    {
                        targetSortingRate = (int)math.round(baseline.SortingRate * sortingSpeedPercent / 100f);
                        targetSortingRate = math.max(1, targetSortingRate);

                        if (baseline.MailCapacity > 0)
                        {
                            targetMailCapacity = (int)math.round(baseline.MailCapacity * sortingStoragePercent / 100f);
                            targetMailCapacity = math.max(1, targetMailCapacity);
                        }
                    }
                }

                if (postFacilityData.m_PostVanCapacity != targetVanCapacity)
                {
                    postFacilityData.m_PostVanCapacity = targetVanCapacity;
                    facilityChanged = true;
                }

                if (postFacilityData.m_PostTruckCapacity != targetTruckCapacity)
                {
                    postFacilityData.m_PostTruckCapacity = targetTruckCapacity;
                    facilityChanged = true;
                }

                if (postFacilityData.m_SortingRate != targetSortingRate)
                {
                    postFacilityData.m_SortingRate = targetSortingRate;
                    facilityChanged = true;
                }

                if (postFacilityData.m_MailCapacity != targetMailCapacity)
                {
                    postFacilityData.m_MailCapacity = targetMailCapacity;
                    facilityChanged = true;
                }

                if (facilityChanged)
                {
                    entityManager.SetComponentData(prefab, postFacilityData);
                }

                int mailCapacity = postFacilityData.m_MailCapacity;
                int sortingRate = postFacilityData.m_SortingRate;

                if (!entityManager.HasBuffer<Resources>(postEntity))
                {
                    Mod.s_Log.Warn($"Post facility {postEntity} has no Resources buffer.");
                    continue;
                }

                DynamicBuffer<Resources> resources = entityManager.GetBuffer<Resources>(postEntity);

                if (mailCapacity <= 0)
                {
                    Mod.s_Log.Warn($"Mail capacity is zero or less: {mailCapacity} (entity {postEntity})");
                    continue;
                }

                if (sortingRate == 0)
                {
                    // Post office behavior (no sorting capability).
                    postOfficeCount++;
                    HandlePostOffice(
                        postEntity,
                        mailCapacity,
                        settings,
                        resources,
                        fixOverflow,
                        ref postOfficeGets,
                        ref overflowClamps);
                }
                else
                {
                    // Sorting facility behavior.
                    sortingFacilityCount++;
                    HandleSortingFacility(
                        postEntity,
                        mailCapacity,
                        settings,
                        resources,
                        fixOverflow,
                        ref sortingGets,
                        ref overflowClamps);
                }

                // NOTE: Future hook for "MoreDeliveries" could go here, once we tie into
                // the underlying request logic (PostFacilityAISystem thresholds/etc).
                // For now, the toggle is plumbed and logged but does not change delivery
                // request behaviour on its own.

                // For status summary we want total capacity after scaling.
                totalPostVanCapacity += postFacilityData.m_PostVanCapacity;
                totalPostTruckCapacity += postFacilityData.m_PostTruckCapacity;
            }

            // Publish status for the Status tab.
            s_LastFacilityCount = facilityCount;
            s_LastPostOfficeCount = postOfficeCount;
            s_LastSortingFacilityCount = sortingFacilityCount;
            s_LastPostVanCapacityTotal = totalPostVanCapacity;
            s_LastPostTruckCapacityTotal = totalPostTruckCapacity;
            s_LastPostOfficeGets = postOfficeGets;
            s_LastSortingGets = sortingGets;
            s_LastOverflowClamps = overflowClamps;

            // Update city-wide mail stats from the vanilla MailAccumulationSystem.
            if (m_MailAccumulationSystem == null)
            {
                TryResolveMailAccumulationSystem();
            }

            if (m_MailAccumulationSystem != null)
            {
                s_LastCityAccumulatedMail = m_MailAccumulationSystem.LastAccumulatedMail;
                s_LastCityProcessedMail = m_MailAccumulationSystem.LastProcessedMail;
            }
        }

        /// <summary>
        /// Applies van mail capacity multipliers to all post van prefabs.
        /// </summary>
        private void ApplyPostVanStats(EntityManager entityManager, int vanMailPercent)
        {
            using NativeArray<Entity> vanEntities = m_PostVanPrefabsQuery.ToEntityArray(Allocator.Temp);

            foreach (Entity vanPrefab in vanEntities)
            {
                bool changedVanData = false;

                if (!entityManager.TryGetComponent(vanPrefab, out PostVanData vanData))
                {
                    continue;
                }

                // Baseline for van mail capacity.
                if (!m_PostVanMailBaselines.TryGetValue(vanPrefab, out int baseCapacity))
                {
                    baseCapacity = vanData.m_MailCapacity;
                    m_PostVanMailBaselines[vanPrefab] = baseCapacity;
                }

                int targetCapacity = (int)math.round(baseCapacity * vanMailPercent / 100f);
                targetCapacity = math.max(1, targetCapacity);

                if (vanData.m_MailCapacity != targetCapacity)
                {
                    vanData.m_MailCapacity = targetCapacity;
                    changedVanData = true;
                }

                if (changedVanData)
                {
                    entityManager.SetComponentData(vanPrefab, vanData);
                }
            }
        }

        /// <summary>
        /// Handles mail behavior for a pure post office (no sorting).
        /// </summary>
        private static void HandlePostOffice(
            Entity postEntity,
            int mailCapacity,
            Setting settings,
            DynamicBuffer<Resources> resources,
            bool fixOverflow,
            ref int getCounter,
            ref int overflowCounter)
        {
            bool didGet = false;
            bool didOverflow = false;

            int localMailCount = GetResourceAmount(resources, Resource.LocalMail);
            int outgoingMailCount = GetResourceAmount(resources, Resource.OutgoingMail);
            int unsortedMailCount = GetResourceAmount(resources, Resource.UnsortedMail);
            int allMailCount = localMailCount + outgoingMailCount + unsortedMailCount;

            // 1) Pull local mail if under threshold (magic top-up).
            if (settings.PO_GetLocalMail &&
                mailCapacity > 0 &&
                localMailCount * 100 / mailCapacity <= settings.PO_GettingThresholdPercentage)
            {
                int addAmount = mailCapacity * settings.PO_GettingPercentage / 100;
                int oldLocal = localMailCount;

                AddResourceAmount(resources, Resource.LocalMail, addAmount);

                localMailCount = GetResourceAmount(resources, Resource.LocalMail);
                outgoingMailCount = GetResourceAmount(resources, Resource.OutgoingMail);
                unsortedMailCount = GetResourceAmount(resources, Resource.UnsortedMail);
                allMailCount = localMailCount + outgoingMailCount + unsortedMailCount;

                didGet = true;
                Mod.s_Log.Info($"[PO Get] {postEntity}.LocalMail: {oldLocal} -> {localMailCount}");
            }

            // 2) Overflow cleanup (global toggle).
            if (!fixOverflow || allMailCount == 0)
            {
                if (didGet)
                {
                    getCounter++;
                }

                return;
            }

            double overflowRatio = settings.PO_OverflowPercentage / 100.0;
            double fillRatio = (double)allMailCount / mailCapacity;

            if (fillRatio < overflowRatio)
            {
                if (didGet)
                {
                    getCounter++;
                }

                return;
            }

            // Clamp each mail type so total storage is near overflowRatio * capacity.
            int targetTotal = (int)math.round(overflowRatio * mailCapacity);
            if (targetTotal < 0)
            {
                targetTotal = 0;
            }

            // Proportional distribution based on current shares.
            if (allMailCount > 0)
            {
                int targetLocal = (int)math.round((double)localMailCount / allMailCount * targetTotal);
                int targetOutgoing = (int)math.round((double)outgoingMailCount / allMailCount * targetTotal);
                int targetUnsorted = targetTotal - targetLocal - targetOutgoing;

                AddResourceAmount(resources, Resource.LocalMail, targetLocal - localMailCount);
                AddResourceAmount(resources, Resource.OutgoingMail, targetOutgoing - outgoingMailCount);
                AddResourceAmount(resources, Resource.UnsortedMail, targetUnsorted - unsortedMailCount);
            }

            int oldAll = allMailCount;
            localMailCount = GetResourceAmount(resources, Resource.LocalMail);
            outgoingMailCount = GetResourceAmount(resources, Resource.OutgoingMail);
            unsortedMailCount = GetResourceAmount(resources, Resource.UnsortedMail);
            allMailCount = localMailCount + outgoingMailCount + unsortedMailCount;

            didOverflow = true;
            Mod.s_Log.Info($"[PO Overflow] {postEntity}.All: {oldAll} -> {allMailCount}");

            if (didGet)
            {
                getCounter++;
            }

            if (didOverflow)
            {
                overflowCounter++;
            }
        }

        /// <summary>
        /// Handles mail behavior for a sorting facility.
        /// </summary>
        private static void HandleSortingFacility(
            Entity postEntity,
            int mailCapacity,
            Setting settings,
            DynamicBuffer<Resources> resources,
            bool fixOverflow,
            ref int getCounter,
            ref int overflowCounter)
        {
            bool didGet = false;
            bool didOverflow = false;

            int localMailCount = GetResourceAmount(resources, Resource.LocalMail);
            int outgoingMailCount = GetResourceAmount(resources, Resource.OutgoingMail);
            int unsortedMailCount = GetResourceAmount(resources, Resource.UnsortedMail);
            int allMailCount = localMailCount + outgoingMailCount + unsortedMailCount;

            // 1) Pull unsorted mail if under threshold (magic top-up).
            if (settings.PSF_GetUnsortedMail &&
                mailCapacity > 0 &&
                unsortedMailCount * 100 / mailCapacity <= settings.PSF_GettingThresholdPercentage)
            {
                int addAmount = mailCapacity * settings.PSF_GettingPercentage / 100;
                int oldUnsorted = unsortedMailCount;

                AddResourceAmount(resources, Resource.UnsortedMail, addAmount);

                localMailCount = GetResourceAmount(resources, Resource.LocalMail);
                outgoingMailCount = GetResourceAmount(resources, Resource.OutgoingMail);
                unsortedMailCount = GetResourceAmount(resources, Resource.UnsortedMail);
                allMailCount = localMailCount + outgoingMailCount + unsortedMailCount;

                didGet = true;
                Mod.s_Log.Info($"[PSF Get] {postEntity}.UnsortedMail: {oldUnsorted} -> {unsortedMailCount}");
            }

            // 2) Overflow cleanup (global toggle).
            if (!fixOverflow || allMailCount == 0)
            {
                if (didGet)
                {
                    getCounter++;
                }

                return;
            }

            double overflowRatio = settings.PSF_OverflowPercentage / 100.0;
            double fillRatio = (double)allMailCount / mailCapacity;

            if (fillRatio < overflowRatio)
            {
                if (didGet)
                {
                    getCounter++;
                }

                return;
            }

            int targetTotal = (int)math.round(overflowRatio * mailCapacity);
            if (targetTotal < 0)
            {
                targetTotal = 0;
            }

            if (allMailCount > 0)
            {
                int targetLocal = (int)math.round((double)localMailCount / allMailCount * targetTotal);
                int targetOutgoing = (int)math.round((double)outgoingMailCount / allMailCount * targetTotal);
                int targetUnsorted = targetTotal - targetLocal - targetOutgoing;

                AddResourceAmount(resources, Resource.LocalMail, targetLocal - localMailCount);
                AddResourceAmount(resources, Resource.OutgoingMail, targetOutgoing - outgoingMailCount);
                AddResourceAmount(resources, Resource.UnsortedMail, targetUnsorted - unsortedMailCount);
            }

            int oldAll = allMailCount;
            localMailCount = GetResourceAmount(resources, Resource.LocalMail);
            outgoingMailCount = GetResourceAmount(resources, Resource.OutgoingMail);
            unsortedMailCount = GetResourceAmount(resources, Resource.UnsortedMail);
            allMailCount = localMailCount + outgoingMailCount + unsortedMailCount;

            didOverflow = true;
            Mod.s_Log.Info($"[PSF Overflow] {postEntity}.All: {oldAll} -> {allMailCount}");

            if (didGet)
            {
                getCounter++;
            }

            if (didOverflow)
            {
                overflowCounter++;
            }
        }

        // --------------------------------------------------------------------
        // Resource buffer helpers (local replacement for EconomyUtils.*)
        // --------------------------------------------------------------------

        private static int GetResourceAmount(DynamicBuffer<Resources> resources, Resource resource)
        {
            for (int i = 0; i < resources.Length; i++)
            {
                Resources value = resources[i];
                if (value.m_Resource == resource)
                {
                    return value.m_Amount;
                }
            }

            return 0;
        }

        private static int AddResourceAmount(DynamicBuffer<Resources> resources, Resource resource, int amount)
        {
            for (int i = 0; i < resources.Length; i++)
            {
                Resources value = resources[i];
                if (value.m_Resource == resource)
                {
                    long newAmount = (long)value.m_Amount + amount;
                    if (newAmount < int.MinValue)
                    {
                        newAmount = int.MinValue;
                    }
                    else if (newAmount > int.MaxValue)
                    {
                        newAmount = int.MaxValue;
                    }

                    value.m_Amount = (int)newAmount;
                    resources[i] = value;
                    return value.m_Amount;
                }
            }

            resources.Add(new Resources
            {
                m_Resource = resource,
                m_Amount = amount,
            });

            return amount;
        }

        // --------------------------------------------------------------------
        // Internal helpers
        // --------------------------------------------------------------------

        private void TryResolveMailAccumulationSystem()
        {
            try
            {
                m_MailAccumulationSystem = World.GetExistingSystemManaged<MailAccumulationSystem>();
            }
            catch (System.InvalidOperationException)
            {
                if (m_MailAccumulationSystem == null)
                {
                    Mod.s_Log.Warn("MailAccumulationSystem not found; city mail stats unavailable.");
                }
            }
        }
    }
}
