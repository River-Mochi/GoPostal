// <copyright file="MailSortingTrafficDiagnosticSystem.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Systems/Diagnostics/MailSortingTrafficDiagnosticSystem.cs
// Purpose: DEBUG-only sorting-facility truck, request, and resource evidence.

#if DEBUG
namespace MagicMail
{
    using System.Collections.Generic;
    using System.Text;
    using Colossal.Serialization.Entities;
    using CS2Shared.RiverMochi;
    using Game;
    using Game.Buildings;
    using Game.Common;
    using Game.Economy;
    using Game.Prefabs;
    using Game.SceneFlow;
    using Game.Simulation;
    using Game.Vehicles;
    using Unity.Collections;
    using Unity.Entities;

    /// <summary>
    /// Samples sorting facilities at the vanilla post-facility update rate.
    /// It only observes state and writes aggregated evidence every 90 in-game minutes.
    /// </summary>
    public sealed partial class MailSortingTrafficDiagnosticSystem : GameSystemBase
    {
        private const int kUpdatesPerDay = 256;
        private const int kSamplesPerWindow = 16; // 16 * 5.625 minutes = 90 minutes.

        private readonly Dictionary<Entity, FacilityObservation> m_Previous = new();
        private readonly Dictionary<Entity, FacilityWindow> m_Windows = new();
        private readonly List<Entity> m_RemoveFacilities = new();
        private readonly StringBuilder m_Report = new(2048);

        private EntityQuery m_FacilityQuery;
        private PrefabSystem m_PrefabSystem = null!;
        private SimulationSystem m_SimulationSystem = null!;

        public override int GetUpdateInterval(SystemUpdatePhase phase)
        {
            return 262144 / kUpdatesPerDay;
        }

        public override int GetUpdateOffset(SystemUpdatePhase phase)
        {
            return 248;
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();
            m_SimulationSystem = World.GetOrCreateSystemManaged<SimulationSystem>();

            m_FacilityQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[]
                {
                    ComponentType.ReadOnly<PrefabRef>(),
                    ComponentType.ReadOnly<Game.Buildings.PostFacility>(),
                    ComponentType.ReadOnly<Resources>(),
                },
                None = new[]
                {
                    ComponentType.ReadOnly<Destroyed>(),
                    ComponentType.ReadOnly<Deleted>(),
                    ComponentType.ReadOnly<Temp>(),
                },
            });

            RequireForUpdate(m_FacilityQuery);
            LogUtils.Info("[MAIL SORT DIAG] Sorting truck monitor created.");
        }

        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);

            if (mode == GameMode.Game &&
                (purpose == Purpose.NewGame || purpose == Purpose.LoadGame))
            {
                m_Previous.Clear();
                m_Windows.Clear();
                m_RemoveFacilities.Clear();
                LogUtils.Info("[MAIL SORT DIAG] Evidence windows reset for loaded city.");
            }
        }

        protected override void OnUpdate()
        {
            GameManager? gameManager = GameManager.instance;
            if (gameManager == null || !gameManager.gameMode.IsGame())
            {
                return;
            }

            using NativeArray<Entity> facilities = m_FacilityQuery.ToEntityArray(Allocator.Temp);
            var sortingFacilities = new HashSet<Entity>();
            var prefabNames = new Dictionary<Entity, string>();

            foreach (Entity facilityEntity in facilities)
            {
                if (!TryGetEffectiveFacilityData(
                        facilityEntity,
                        out PostFacilityData effectiveData,
                        out string prefabName) ||
                    effectiveData.m_SortingRate == 0)
                {
                    continue;
                }

                sortingFacilities.Add(facilityEntity);
                prefabNames[facilityEntity] = prefabName;
            }

            var targetedTrucks = new Dictionary<Entity, Dictionary<Entity, TruckObservation>>();
            foreach ((RefRO<DeliveryTruck> truckRef, RefRO<Target> targetRef, Entity truckEntity) in
                     SystemAPI.Query<RefRO<DeliveryTruck>, RefRO<Target>>().WithEntityAccess())
            {
                DeliveryTruck truck = truckRef.ValueRO;
                if (!IsMailResource(truck.m_Resource))
                {
                    continue;
                }

                Entity target = targetRef.ValueRO.m_Target;
                if (!sortingFacilities.Contains(target))
                {
                    continue;
                }

                if (!targetedTrucks.TryGetValue(
                        target,
                        out Dictionary<Entity, TruckObservation>? trucks))
                {
                    trucks = new Dictionary<Entity, TruckObservation>();
                    targetedTrucks.Add(target, trucks);
                }

                trucks[truckEntity] = ReadTruck(truckEntity, truck, isGuest: false, isTarget: true);
            }

            uint frame = m_SimulationSystem.frameIndex;
            foreach (Entity facilityEntity in sortingFacilities)
            {
                FacilityObservation current = ReadFacility(
                    facilityEntity,
                    targetedTrucks.TryGetValue(
                        facilityEntity,
                        out Dictionary<Entity, TruckObservation>? targetMap)
                        ? targetMap
                        : null);

                if (!m_Previous.TryGetValue(
                        facilityEntity,
                        out FacilityObservation? previous))
                {
                    m_Previous[facilityEntity] = current;
                    m_Windows[facilityEntity] = new FacilityWindow(frame, current);
                    continue;
                }

                if (!m_Windows.TryGetValue(
                        facilityEntity,
                        out FacilityWindow? window))
                {
                    window = new FacilityWindow(frame, previous);
                    m_Windows[facilityEntity] = window;
                }

                CompareFacility(
                    facilityEntity,
                    prefabNames[facilityEntity],
                    frame,
                    previous,
                    current,
                    window);

                m_Previous[facilityEntity] = current;

                if (window.Samples >= kSamplesPerWindow)
                {
                    WriteWindow(
                        facilityEntity,
                        prefabNames[facilityEntity],
                        frame,
                        current,
                        window);

                    m_Windows[facilityEntity] = new FacilityWindow(frame, current);
                }
            }

            m_RemoveFacilities.Clear();
            foreach (Entity facilityEntity in m_Previous.Keys)
            {
                if (!sortingFacilities.Contains(facilityEntity))
                {
                    m_RemoveFacilities.Add(facilityEntity);
                }
            }

            foreach (Entity facilityEntity in m_RemoveFacilities)
            {
                m_Previous.Remove(facilityEntity);
                m_Windows.Remove(facilityEntity);
            }
        }

        private FacilityObservation ReadFacility(
            Entity facilityEntity,
            Dictionary<Entity, TruckObservation>? targetMap)
        {
            DynamicBuffer<Resources> resources =
                EntityManager.GetBuffer<Resources>(facilityEntity, true);
            Game.Buildings.PostFacility facility =
                EntityManager.GetComponentData<Game.Buildings.PostFacility>(facilityEntity);

            var observation = new FacilityObservation
            {
                Local = EconomyUtils.GetResources(Resource.LocalMail, resources),
                Unsorted = EconomyUtils.GetResources(Resource.UnsortedMail, resources),
                Outgoing = EconomyUtils.GetResources(Resource.OutgoingMail, resources),
                ProcessingFactor = facility.m_ProcessingFactor,
                DeliverRequest = ReadRequest(facility.m_MailDeliverRequest),
                ReceiveRequest = ReadRequest(facility.m_MailReceiveRequest),
            };

            if (targetMap != null)
            {
                foreach (KeyValuePair<Entity, TruckObservation> pair in targetMap)
                {
                    observation.Trucks[pair.Key] = pair.Value;
                }
            }

            if (EntityManager.HasBuffer<GuestVehicle>(facilityEntity))
            {
                DynamicBuffer<GuestVehicle> guests =
                    EntityManager.GetBuffer<GuestVehicle>(facilityEntity, true);

                foreach (GuestVehicle guest in guests)
                {
                    Entity truckEntity = guest.m_Vehicle;
                    if (!EntityManager.HasComponent<DeliveryTruck>(truckEntity))
                    {
                        continue;
                    }

                    DeliveryTruck truck =
                        EntityManager.GetComponentData<DeliveryTruck>(truckEntity);
                    if (!IsMailResource(truck.m_Resource))
                    {
                        continue;
                    }

                    TruckObservation guestObservation =
                        ReadTruck(truckEntity, truck, isGuest: true, isTarget: false);

                    if (observation.Trucks.TryGetValue(
                            truckEntity,
                            out TruckObservation existing))
                    {
                        guestObservation.IsTarget = existing.IsTarget;
                    }

                    observation.Trucks[truckEntity] = guestObservation;
                }
            }

            return observation;
        }

        private TruckObservation ReadTruck(
            Entity truckEntity,
            DeliveryTruck truck,
            bool isGuest,
            bool isTarget)
        {
            var result = new TruckObservation
            {
                Resource = truck.m_Resource,
                Amount = truck.m_Amount,
                State = truck.m_State,
                IsGuest = isGuest,
                IsTarget = isTarget,
                ReturnResource = Resource.NoResource,
                ReturnAmount = 0,
            };

            if (EntityManager.HasComponent<ReturnLoad>(truckEntity))
            {
                ReturnLoad returnLoad =
                    EntityManager.GetComponentData<ReturnLoad>(truckEntity);
                result.ReturnResource = returnLoad.m_Resource;
                result.ReturnAmount = returnLoad.m_Amount;
            }

            return result;
        }

        private RequestObservation ReadRequest(Entity requestEntity)
        {
            if (requestEntity == Entity.Null)
            {
                return default;
            }

            var result = new RequestObservation
            {
                Entity = requestEntity,
                Exists = EntityManager.Exists(requestEntity),
            };

            if (!result.Exists ||
                !EntityManager.HasComponent<MailTransferRequest>(requestEntity))
            {
                return result;
            }

            MailTransferRequest request =
                EntityManager.GetComponentData<MailTransferRequest>(requestEntity);
            result.Flags = request.m_Flags;
            result.Amount = request.m_Amount;
            result.HasTransferRequest = true;
            result.Pathfinding =
                EntityManager.HasComponent<Game.Pathfind.PathInformation>(requestEntity);
            result.Dispatched =
                EntityManager.HasComponent<Dispatched>(requestEntity);

            if (EntityManager.HasComponent<ServiceRequest>(requestEntity))
            {
                ServiceRequest serviceRequest =
                    EntityManager.GetComponentData<ServiceRequest>(requestEntity);
                result.FailCount = serviceRequest.m_FailCount;
                result.Cooldown = serviceRequest.m_Cooldown;
            }

            return result;
        }

        private void CompareFacility(
            Entity facilityEntity,
            string prefabName,
            uint frame,
            FacilityObservation previous,
            FacilityObservation current,
            FacilityWindow window)
        {
            window.Samples++;
            window.EndLocal = current.Local;
            window.EndUnsorted = current.Unsorted;
            window.EndOutgoing = current.Outgoing;

            if (current.ProcessingFactor > 0)
            {
                window.ProcessingActiveSamples++;
            }

            foreach (KeyValuePair<Entity, TruckObservation> pair in current.Trucks)
            {
                Entity truckEntity = pair.Key;
                TruckObservation currentTruck = pair.Value;

                if (!previous.Trucks.TryGetValue(
                        truckEntity,
                        out TruckObservation previousTruck))
                {
                    if (currentTruck.IsGuest)
                    {
                        window.GuestEntries++;
                        window.GuestEntryLoad += currentTruck.Amount;
                        LogTruckEvent(
                            "ENTER",
                            facilityEntity,
                            prefabName,
                            truckEntity,
                            frame,
                            currentTruck);
                    }

                    if (currentTruck.IsTarget)
                    {
                        window.TargetStarts++;
                    }

                    continue;
                }

                if (!previousTruck.IsGuest && currentTruck.IsGuest)
                {
                    window.GuestEntries++;
                    window.GuestEntryLoad += currentTruck.Amount;
                    LogTruckEvent(
                        "ENTER",
                        facilityEntity,
                        prefabName,
                        truckEntity,
                        frame,
                        currentTruck);
                }

                if (!previousTruck.IsTarget && currentTruck.IsTarget)
                {
                    window.TargetStarts++;
                }

                TrackAmountDelta(
                    previousTruck.Resource,
                    previousTruck.Amount,
                    currentTruck.Resource,
                    currentTruck.Amount,
                    ref window.CargoIncrease,
                    ref window.CargoDecrease,
                    ref window.CargoResourceChanges);

                TrackAmountDelta(
                    previousTruck.ReturnResource,
                    previousTruck.ReturnAmount,
                    currentTruck.ReturnResource,
                    currentTruck.ReturnAmount,
                    ref window.ReturnIncrease,
                    ref window.ReturnDecrease,
                    ref window.ReturnResourceChanges);
            }

            foreach (KeyValuePair<Entity, TruckObservation> pair in previous.Trucks)
            {
                Entity truckEntity = pair.Key;
                TruckObservation previousTruck = pair.Value;

                if (!current.Trucks.TryGetValue(
                        truckEntity,
                        out TruckObservation currentTruck))
                {
                    if (previousTruck.IsGuest)
                    {
                        window.GuestExits++;
                        window.GuestExitLoad += previousTruck.Amount;
                        LogTruckEvent(
                            "EXIT",
                            facilityEntity,
                            prefabName,
                            truckEntity,
                            frame,
                            previousTruck);
                    }

                    if (previousTruck.IsTarget)
                    {
                        window.TargetStops++;
                    }

                    continue;
                }

                if (previousTruck.IsGuest && !currentTruck.IsGuest)
                {
                    window.GuestExits++;
                    window.GuestExitLoad += previousTruck.Amount;
                    LogTruckEvent(
                        "EXIT",
                        facilityEntity,
                        prefabName,
                        truckEntity,
                        frame,
                        previousTruck);
                }

                if (previousTruck.IsTarget && !currentTruck.IsTarget)
                {
                    window.TargetStops++;
                }
            }

            CompareRequest(
                "DELIVER",
                facilityEntity,
                prefabName,
                frame,
                previous.DeliverRequest,
                current.DeliverRequest,
                ref window.DeliverRequests);

            CompareRequest(
                "RECEIVE",
                facilityEntity,
                prefabName,
                frame,
                previous.ReceiveRequest,
                current.ReceiveRequest,
                ref window.ReceiveRequests);
        }

        private void CompareRequest(
            string kind,
            Entity facilityEntity,
            string prefabName,
            uint frame,
            RequestObservation previous,
            RequestObservation current,
            ref RequestWindow window)
        {
            if (current.Entity != previous.Entity)
            {
                if (current.Entity != Entity.Null)
                {
                    window.Created++;
                }

                if (previous.Entity != Entity.Null)
                {
                    window.Replaced++;
                }

                LogUtils.Info(
                    $"[MAIL SORT REQUEST] frame={frame} facility={facilityEntity} " +
                    $"prefab=\"{prefabName}\" kind={kind} " +
                    $"old={FormatRequest(previous)} new={FormatRequest(current)}");
            }

            if (current.Entity != Entity.Null && !current.Exists)
            {
                window.MissingSamples++;
            }

            if (current.Entity == previous.Entity && current.Entity != Entity.Null)
            {
                if (!previous.Pathfinding && current.Pathfinding)
                {
                    window.PathStarts++;
                }

                if (!previous.Dispatched && current.Dispatched)
                {
                    window.DispatchStarts++;
                }

                if (current.FailCount > previous.FailCount)
                {
                    window.FailIncreases += current.FailCount - previous.FailCount;
                }
            }
        }

        private static void TrackAmountDelta(
            Resource previousResource,
            int previousAmount,
            Resource currentResource,
            int currentAmount,
            ref long increase,
            ref long decrease,
            ref int resourceChanges)
        {
            if (previousResource != currentResource)
            {
                resourceChanges++;
                return;
            }

            int delta = currentAmount - previousAmount;
            if (delta > 0)
            {
                increase += delta;
            }
            else if (delta < 0)
            {
                decrease += -delta;
            }
        }

        private static void LogTruckEvent(
            string action,
            Entity facilityEntity,
            string prefabName,
            Entity truckEntity,
            uint frame,
            TruckObservation truck)
        {
            LogUtils.Info(
                $"[MAIL SORT TRUCK {action}] frame={frame} facility={facilityEntity} " +
                $"prefab=\"{prefabName}\" truck={truckEntity} resource={truck.Resource} " +
                $"amount={truck.Amount} state={truck.State} returnResource={truck.ReturnResource} " +
                $"returnAmount={truck.ReturnAmount} target={truck.IsTarget}");
        }

        private void WriteWindow(
            Entity facilityEntity,
            string prefabName,
            uint frame,
            FacilityObservation current,
            FacilityWindow window)
        {
            m_Report.Clear();
            m_Report.AppendLine(
                $"[MAIL SORT WINDOW] facility={facilityEntity} prefab=\"{prefabName}\" " +
                $"frame={window.StartFrame}->{frame} samples={window.Samples} " +
                $"processingActive={window.ProcessingActiveSamples}/{window.Samples}");
            m_Report.AppendLine(
                $"[MAIL SORT RESOURCE] L={window.StartLocal}->{current.Local}" +
                $"({Signed(current.Local - window.StartLocal)}) " +
                $"U={window.StartUnsorted}->{current.Unsorted}" +
                $"({Signed(current.Unsorted - window.StartUnsorted)}) " +
                $"O={window.StartOutgoing}->{current.Outgoing}" +
                $"({Signed(current.Outgoing - window.StartOutgoing)}) " +
                $"total={window.StartTotal}->{current.Total}" +
                $"({Signed(current.Total - window.StartTotal)})");
            m_Report.AppendLine(
                $"[MAIL SORT TRUCK WINDOW] guestIn={window.GuestEntries} " +
                $"guestOut={window.GuestExits} guestEntryLoad={window.GuestEntryLoad} " +
                $"guestExitLoad={window.GuestExitLoad} targetStart={window.TargetStarts} " +
                $"targetStop={window.TargetStops} cargoUp={window.CargoIncrease} " +
                $"cargoDown={window.CargoDecrease} cargoResourceChanges={window.CargoResourceChanges} " +
                $"returnUp={window.ReturnIncrease} returnDown={window.ReturnDecrease} " +
                $"returnResourceChanges={window.ReturnResourceChanges}");
            m_Report.AppendLine(
                $"[MAIL SORT REQUEST WINDOW] deliver={FormatRequestWindow(window.DeliverRequests)} " +
                $"receive={FormatRequestWindow(window.ReceiveRequests)} " +
                $"deliverNow={FormatRequest(current.DeliverRequest)} " +
                $"receiveNow={FormatRequest(current.ReceiveRequest)}");

            LogUtils.Info(m_Report.ToString().TrimEnd());
        }

        private bool TryGetEffectiveFacilityData(
            Entity facilityEntity,
            out PostFacilityData effectiveData,
            out string prefabName)
        {
            effectiveData = default;
            prefabName = facilityEntity.ToString();

            if (!EntityManager.HasComponent<PrefabRef>(facilityEntity))
            {
                return false;
            }

            PrefabRef prefabRef =
                EntityManager.GetComponentData<PrefabRef>(facilityEntity);
            if (!EntityManager.HasComponent<PostFacilityData>(prefabRef.m_Prefab))
            {
                return false;
            }

            effectiveData =
                EntityManager.GetComponentData<PostFacilityData>(prefabRef.m_Prefab);

            if (EntityManager.HasBuffer<InstalledUpgrade>(facilityEntity))
            {
                DynamicBuffer<InstalledUpgrade> upgrades =
                    EntityManager.GetBuffer<InstalledUpgrade>(facilityEntity, true);
                UpgradeUtils.CombineStats(EntityManager, ref effectiveData, upgrades);
            }

            if (m_PrefabSystem.TryGetPrefab(
                    prefabRef.m_Prefab,
                    out PrefabBase prefabBase))
            {
                prefabName = prefabBase.name;
            }

            return true;
        }

        private static bool IsMailResource(Resource resource)
        {
            const Resource mailResources =
                Resource.LocalMail | Resource.UnsortedMail | Resource.OutgoingMail;

            return (resource & mailResources) != Resource.NoResource;
        }

        private static string FormatRequest(RequestObservation request)
        {
            if (request.Entity == Entity.Null)
            {
                return "none";
            }

            if (!request.Exists)
            {
                return $"{request.Entity}:missing";
            }

            if (!request.HasTransferRequest)
            {
                return $"{request.Entity}:not-transfer";
            }

            return $"{request.Entity}:{request.Flags}:amount={request.Amount}:" +
                   $"path={request.Pathfinding}:dispatched={request.Dispatched}:" +
                   $"fail={request.FailCount}:cooldown={request.Cooldown}";
        }

        private static string FormatRequestWindow(RequestWindow window)
        {
            return $"created={window.Created},replaced={window.Replaced}," +
                   $"pathStarts={window.PathStarts},dispatchStarts={window.DispatchStarts}," +
                   $"failIncreases={window.FailIncreases},missingSamples={window.MissingSamples}";
        }

        private static string Signed(int value)
        {
            return value > 0 ? $"+{value}" : value.ToString();
        }

        private sealed class FacilityObservation
        {
            public int Local;
            public int Unsorted;
            public int Outgoing;
            public int ProcessingFactor;
            public RequestObservation DeliverRequest;
            public RequestObservation ReceiveRequest;
            public Dictionary<Entity, TruckObservation> Trucks { get; } = new();

            public int Total => Local + Unsorted + Outgoing;
        }

        private sealed class FacilityWindow
        {
            public FacilityWindow(uint startFrame, FacilityObservation start)
            {
                StartFrame = startFrame;
                StartLocal = start.Local;
                StartUnsorted = start.Unsorted;
                StartOutgoing = start.Outgoing;
                EndLocal = start.Local;
                EndUnsorted = start.Unsorted;
                EndOutgoing = start.Outgoing;
            }

            public uint StartFrame;
            public int Samples;
            public int ProcessingActiveSamples;
            public int StartLocal;
            public int StartUnsorted;
            public int StartOutgoing;
            public int EndLocal;
            public int EndUnsorted;
            public int EndOutgoing;
            public int GuestEntries;
            public int GuestExits;
            public int TargetStarts;
            public int TargetStops;
            public long GuestEntryLoad;
            public long GuestExitLoad;
            public long CargoIncrease;
            public long CargoDecrease;
            public int CargoResourceChanges;
            public long ReturnIncrease;
            public long ReturnDecrease;
            public int ReturnResourceChanges;
            public RequestWindow DeliverRequests;
            public RequestWindow ReceiveRequests;

            public int StartTotal => StartLocal + StartUnsorted + StartOutgoing;
        }

        private struct TruckObservation
        {
            public Resource Resource;
            public int Amount;
            public DeliveryTruckFlags State;
            public Resource ReturnResource;
            public int ReturnAmount;
            public bool IsGuest;
            public bool IsTarget;
        }

        private struct RequestObservation
        {
            public Entity Entity;
            public bool Exists;
            public bool HasTransferRequest;
            public MailTransferRequestFlags Flags;
            public int Amount;
            public int FailCount;
            public int Cooldown;
            public bool Pathfinding;
            public bool Dispatched;
        }

        private struct RequestWindow
        {
            public int Created;
            public int Replaced;
            public int PathStarts;
            public int DispatchStarts;
            public int FailIncreases;
            public int MissingSamples;
        }
    }
}
#endif
