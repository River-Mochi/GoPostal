// <copyright file="MagicMailSystem.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// Systems/MagicMailSystem.cs
// Scans postal facilities for magic top-ups + overflow cleanup,
// and exposes city-wide mail stats via MailAccumulationSystem.

namespace MagicMail
{
    using Colossal.Entities;
    using CS2Shared.RiverMochi;
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
    /// Simulation system that runs the "magic" mail behaviour:
    /// - Local/unsorted mail top-ups
    /// - Optional overflow cleanup
    /// - Status counters & city-wide mail stats (read by Setting.cs).
    /// </summary>
    public partial class MagicMailSystem : GameSystemBase
    {
        private EntityQuery m_PostFacilitiesQuery;

        // ---- CITY-WIDE MAIL STATS (from MailAccumulationSystem) ----

        private MailAccumulationSystem? m_MailAccumulationSystem;

        internal static int s_LastCityAccumulatedMail;
        internal static int s_LastCityProcessedMail;

        // ---- STATUS FIELDS (read by Setting.Status* properties) ----

        internal static int s_LastFacilityCount;
        internal static int s_LastPostOfficeCount;
        internal static int s_LastSortingFacilityCount;
        internal static int s_LastPostVanCapacityTotal;
        internal static int s_LastPostTruckCapacityTotal;
        internal static int s_LastPostOfficeGets;
        internal static int s_LastSortingGets;
        internal static int s_LastOverflowClamps;

        /// <summary>
        /// Controls how often the system updates for each phase.</summary>
        private const int UpdatesPerDay = 32;   // ≈ once per 45 in-game minutes.

        public override int GetUpdateInterval(SystemUpdatePhase phase)
        {
            // Debug tests must use the same intervention frequency as Release.
            return 262144 / UpdatesPerDay;
        }

        /// <summary>
        /// Controls when the system runs relative to other systems.</summary>
        public override int GetUpdateOffset(SystemUpdatePhase phase)
        {
            // Keeps this system in a safe slot before the vanilla system.
            return 48;
        }

        /// <summary>
        /// Creates the system and builds the entity queries.</summary>
        protected override void OnCreate()
        {
            base.OnCreate();

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

            RequireForUpdate(m_PostFacilitiesQuery);

            // Try to grab the vanilla MailAccumulationSystem so stats can be surfaced.
            TryResolveMailAccumulationSystem();

#if DEBUG
            LogUtils.Info("MagicMailSystem created.");
#endif
        }

        /// <summary>
        /// Per-update simulation logic for all post facilities.</summary>
        protected override void OnUpdate()
        {
            Setting? settings = Mod.Settings;
            if (settings == null)
            {
                return;
            }

            var entityManager = EntityManager;

            bool fixOverflow = settings.FixMailOverflow;

            using NativeArray<Entity> postEntities = m_PostFacilitiesQuery.ToEntityArray(Allocator.Temp);
            var facilityCount = postEntities.Length;
            var postOfficeCount = 0;
            var sortingFacilityCount = 0;
            var postOfficeGets = 0;
            var sortingGets = 0;
            var overflowClamps = 0;
            var totalPostVanCapacity = 0;
            var totalPostTruckCapacity = 0;

            foreach (var postEntity in postEntities)
            {
                if (!entityManager.TryGetComponent(postEntity, out PrefabRef prefabRef))
                {
                    LogUtils.WarnOnce(
                        "MM.MissingPrefabRef",
                        () => $"Failed to retrieve PrefabRef for {postEntity}.");
                    continue;
                }

                Entity prefab = prefabRef.m_Prefab;

                if (!entityManager.TryGetComponent(prefab, out PostFacilityData postFacilityData))
                {
                    LogUtils.WarnOnce(
                        "MM.MissingPostFacilityData",
                        () => $"Failed to retrieve PostFacilityData for prefab {prefab}.");
                    continue;
                }

                var mailCapacity = postFacilityData.m_MailCapacity;
                var sortingRate = postFacilityData.m_SortingRate;

                if (!entityManager.HasBuffer<Resources>(postEntity))
                {
                    LogUtils.WarnOnce(
                        "MM.MissingResources",
                        () => $"Post facility {postEntity} has no Resources buffer.");
                    continue;
                }

                DynamicBuffer<Resources> resources = entityManager.GetBuffer<Resources>(postEntity);

                if (mailCapacity <= 0)
                {
                    LogUtils.WarnOnce(
                        "MM.InvalidMailCapacity",
                        () => $"Mail capacity is zero or less: {mailCapacity} (entity {postEntity}).");
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

                // For status summary, track total capacity after scaling.
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
        /// Handles mail behavior for a pure post office (no sorting).</summary>
        private static void HandlePostOffice(
            Entity postEntity,
            int mailCapacity,
            Setting settings,
            DynamicBuffer<Resources> resources,
            bool fixOverflow,
            ref int getCounter,
            ref int overflowCounter)
        {
            var didGet = false;
            var didOverflow = false;

            var localMailCount = GetResourceAmount(resources, Resource.LocalMail);
            var outgoingMailCount = GetResourceAmount(resources, Resource.OutgoingMail);
            var unsortedMailCount = GetResourceAmount(resources, Resource.UnsortedMail);
            var allMailCount = localMailCount + outgoingMailCount + unsortedMailCount;

            // 1) Pull local mail if under threshold (magic top-up).
            if (settings.PO_GetLocalMail &&
                mailCapacity > 0 &&
                localMailCount * 100 / mailCapacity <= settings.PO_GettingThresholdPercentage)
            {
                var addAmount = mailCapacity * settings.PO_GettingPercentage / 100;
#if DEBUG
                var beforeTopUpLocal = localMailCount;
#endif

                AddResourceAmount(resources, Resource.LocalMail, addAmount);

                localMailCount = GetResourceAmount(resources, Resource.LocalMail);
                outgoingMailCount = GetResourceAmount(resources, Resource.OutgoingMail);
                unsortedMailCount = GetResourceAmount(resources, Resource.UnsortedMail);
                allMailCount = localMailCount + outgoingMailCount + unsortedMailCount;

                didGet = true;
#if DEBUG
                LogUtils.Info(
                    $"[MM EVENT PO_GET] entity={postEntity} " +
                    $"L={beforeTopUpLocal}->{localMailCount} U={unsortedMailCount} O={outgoingMailCount} cap={mailCapacity}");
#endif
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

            var overflowRatio = settings.PO_OverflowPercentage / 100.0;
            var fillRatio = (double)allMailCount / mailCapacity;

            if (fillRatio < overflowRatio)
            {
                if (didGet)
                {
                    getCounter++;
                }

                return;
            }

            // Clamp each mail type so total storage is near overflowRatio * capacity.
            var targetTotal = (int)math.round(overflowRatio * mailCapacity);
            if (targetTotal < 0)
            {
                targetTotal = 0;
            }

            // Proportional distribution based on current shares.
            if (allMailCount > 0)
            {
                var targetLocal = (int)math.round((double)localMailCount / allMailCount * targetTotal);
                var targetOutgoing = (int)math.round((double)outgoingMailCount / allMailCount * targetTotal);
                var targetUnsorted = targetTotal - targetLocal - targetOutgoing;

                AddResourceAmount(resources, Resource.LocalMail, targetLocal - localMailCount);
                AddResourceAmount(resources, Resource.OutgoingMail, targetOutgoing - outgoingMailCount);
                AddResourceAmount(resources, Resource.UnsortedMail, targetUnsorted - unsortedMailCount);
            }

#if DEBUG
            var beforeOverflowLocal = localMailCount;
            var beforeOverflowOutgoing = outgoingMailCount;
            var beforeOverflowUnsorted = unsortedMailCount;
            var beforeOverflowAll = allMailCount;
#endif
            localMailCount = GetResourceAmount(resources, Resource.LocalMail);
            outgoingMailCount = GetResourceAmount(resources, Resource.OutgoingMail);
            unsortedMailCount = GetResourceAmount(resources, Resource.UnsortedMail);
            allMailCount = localMailCount + outgoingMailCount + unsortedMailCount;

            didOverflow = true;
#if DEBUG
            LogUtils.Info(
                $"[MM EVENT PO_OVERFLOW] entity={postEntity} " +
                $"L={beforeOverflowLocal}->{localMailCount} U={beforeOverflowUnsorted}->{unsortedMailCount} " +
                $"O={beforeOverflowOutgoing}->{outgoingMailCount} total={beforeOverflowAll}->{allMailCount} cap={mailCapacity}");
#endif

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
        /// Handles mail behavior for a sorting facility.</summary>
        private static void HandleSortingFacility(
            Entity postEntity,
            int mailCapacity,
            Setting settings,
            DynamicBuffer<Resources> resources,
            bool fixOverflow,
            ref int getCounter,
            ref int overflowCounter)
        {
            var didGet = false;
            var didOverflow = false;

            var localMailCount = GetResourceAmount(resources, Resource.LocalMail);
            var outgoingMailCount = GetResourceAmount(resources, Resource.OutgoingMail);
            var unsortedMailCount = GetResourceAmount(resources, Resource.UnsortedMail);
            var allMailCount = localMailCount + outgoingMailCount + unsortedMailCount;

            // 1) Pull unsorted mail if under threshold (magic top-up).
            if (settings.PSF_GetUnsortedMail &&
                mailCapacity > 0 &&
                unsortedMailCount * 100 / mailCapacity <= settings.PSF_GettingThresholdPercentage)
            {
                var addAmount = mailCapacity * settings.PSF_GettingPercentage / 100;
#if DEBUG
                var beforeTopUpUnsorted = unsortedMailCount;
#endif

                AddResourceAmount(resources, Resource.UnsortedMail, addAmount);

                localMailCount = GetResourceAmount(resources, Resource.LocalMail);
                outgoingMailCount = GetResourceAmount(resources, Resource.OutgoingMail);
                unsortedMailCount = GetResourceAmount(resources, Resource.UnsortedMail);
                allMailCount = localMailCount + outgoingMailCount + unsortedMailCount;

                didGet = true;
#if DEBUG
                LogUtils.Info(
                    $"[MM EVENT PSF_GET] entity={postEntity} " +
                    $"L={localMailCount} U={beforeTopUpUnsorted}->{unsortedMailCount} O={outgoingMailCount} cap={mailCapacity}");
#endif
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

            var overflowRatio = settings.PSF_OverflowPercentage / 100.0;
            var fillRatio = (double)allMailCount / mailCapacity;

            if (fillRatio < overflowRatio)
            {
                if (didGet)
                {
                    getCounter++;
                }

                return;
            }

            var targetTotal = (int)math.round(overflowRatio * mailCapacity);
            if (targetTotal < 0)
            {
                targetTotal = 0;
            }

            if (allMailCount > 0)
            {
                var targetLocal = (int)math.round((double)localMailCount / allMailCount * targetTotal);
                var targetOutgoing = (int)math.round((double)outgoingMailCount / allMailCount * targetTotal);
                var targetUnsorted = targetTotal - targetLocal - targetOutgoing;

                AddResourceAmount(resources, Resource.LocalMail, targetLocal - localMailCount);
                AddResourceAmount(resources, Resource.OutgoingMail, targetOutgoing - outgoingMailCount);
                AddResourceAmount(resources, Resource.UnsortedMail, targetUnsorted - unsortedMailCount);
            }

#if DEBUG
            var beforeOverflowLocal = localMailCount;
            var beforeOverflowOutgoing = outgoingMailCount;
            var beforeOverflowUnsorted = unsortedMailCount;
            var beforeOverflowAll = allMailCount;
#endif
            localMailCount = GetResourceAmount(resources, Resource.LocalMail);
            outgoingMailCount = GetResourceAmount(resources, Resource.OutgoingMail);
            unsortedMailCount = GetResourceAmount(resources, Resource.UnsortedMail);
            allMailCount = localMailCount + outgoingMailCount + unsortedMailCount;

            didOverflow = true;
#if DEBUG
            LogUtils.Info(
                $"[MM EVENT PSF_OVERFLOW] entity={postEntity} " +
                $"L={beforeOverflowLocal}->{localMailCount} U={beforeOverflowUnsorted}->{unsortedMailCount} " +
                $"O={beforeOverflowOutgoing}->{outgoingMailCount} total={beforeOverflowAll}->{allMailCount} cap={mailCapacity}");
#endif

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
            for (var i = 0; i < resources.Length; i++)
            {
                var value = resources[i];
                if (value.m_Resource == resource)
                {
                    return value.m_Amount;
                }
            }

            return 0;
        }

        private static int AddResourceAmount(DynamicBuffer<Resources> resources, Resource resource, int amount)
        {
            for (var i = 0; i < resources.Length; i++)
            {
                var value = resources[i];
                if (value.m_Resource == resource)
                {
                    var newAmount = (long)value.m_Amount + amount;
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
                    LogUtils.WarnOnce(
                        "MM.MailAccumulationSystemMissing",
                        () => "MailAccumulationSystem not found; city mail stats unavailable.");
                }
            }
        }
    }
}
