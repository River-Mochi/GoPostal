// <copyright file="MailDiagnosticSystem.Traffic.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Systems/Diagnostics/MailDiagnosticSystem.Traffic.cs
// Purpose: summarizes pending requests, post vans, and mail semi trucks.

#if DEBUG
namespace MagicMail
{
    using Colossal.Entities;
    using Game.Common;
    using Game.Economy;
    using Game.Pathfind;
    using Game.Prefabs;
    using Game.Simulation;
    using Unity.Entities;

    public sealed partial class MailDiagnosticSystem
    {
        private void LogRequestSummary()
        {
            int vanRequests = 0;
            int vanDeliverOnly = 0;
            int vanCollectOnly = 0;
            int vanCombined = 0;
            int vanDispatched = 0;
            int vanPathfinding = 0;
            int vanFailed = 0;
            int vanReversed = 0;

            foreach ((RefRO<PostVanRequest> requestRef, RefRO<ServiceRequest> serviceRef, Entity entity) in
                     SystemAPI.Query<RefRO<PostVanRequest>, RefRO<ServiceRequest>>().WithEntityAccess())
            {
                PostVanRequestFlags flags = requestRef.ValueRO.m_Flags;
                bool deliver = (flags & PostVanRequestFlags.Deliver) != 0;
                bool collect = (flags & PostVanRequestFlags.Collect) != 0;

                vanRequests++;
                if (deliver && collect)
                {
                    vanCombined++;
                }
                else if (deliver)
                {
                    vanDeliverOnly++;
                }
                else if (collect)
                {
                    vanCollectOnly++;
                }

                if (EntityManager.HasComponent<Dispatched>(entity))
                {
                    vanDispatched++;
                }

                if (EntityManager.HasComponent<PathInformation>(entity))
                {
                    vanPathfinding++;
                }

                if (serviceRef.ValueRO.m_FailCount > 0)
                {
                    vanFailed++;
                }

                if ((serviceRef.ValueRO.m_Flags & ServiceRequestFlags.Reversed) != 0)
                {
                    vanReversed++;
                }
            }

            int transferRequests = 0;
            int transferDeliver = 0;
            int transferReceive = 0;
            int transferRequireTransport = 0;
            int transferLocal = 0;
            int transferUnsorted = 0;
            int transferOutgoing = 0;
            int transferDispatched = 0;
            int transferPathfinding = 0;
            int transferFailed = 0;
            long transferAmount = 0;

            foreach ((RefRO<MailTransferRequest> requestRef, RefRO<ServiceRequest> serviceRef, Entity entity) in
                     SystemAPI.Query<RefRO<MailTransferRequest>, RefRO<ServiceRequest>>().WithEntityAccess())
            {
                MailTransferRequest request = requestRef.ValueRO;
                MailTransferRequestFlags flags = request.m_Flags;

                transferRequests++;
                transferAmount += request.m_Amount;

                if ((flags & MailTransferRequestFlags.Deliver) != 0)
                {
                    transferDeliver++;
                }

                if ((flags & MailTransferRequestFlags.Receive) != 0)
                {
                    transferReceive++;
                }

                if ((flags & MailTransferRequestFlags.RequireTransport) != 0)
                {
                    transferRequireTransport++;
                }

                if ((flags & MailTransferRequestFlags.LocalMail) != 0)
                {
                    transferLocal++;
                }

                if ((flags & MailTransferRequestFlags.UnsortedMail) != 0)
                {
                    transferUnsorted++;
                }

                if ((flags & MailTransferRequestFlags.OutgoingMail) != 0)
                {
                    transferOutgoing++;
                }

                if (EntityManager.HasComponent<Dispatched>(entity))
                {
                    transferDispatched++;
                }

                if (EntityManager.HasComponent<PathInformation>(entity))
                {
                    transferPathfinding++;
                }

                if (serviceRef.ValueRO.m_FailCount > 0)
                {
                    transferFailed++;
                }
            }

            AddLine(
                $"[MAIL REQUEST VAN] total={vanRequests} deliverOnly={vanDeliverOnly} " +
                $"collectOnly={vanCollectOnly} combined={vanCombined} dispatched={vanDispatched} " +
                $"pathfinding={vanPathfinding} failed={vanFailed} reversed={vanReversed}");

            AddLine(
                $"[MAIL REQUEST SEMI] total={transferRequests} deliverToFacility={transferDeliver} " +
                $"receiveFromFacility={transferReceive} requireTransport={transferRequireTransport} " +
                $"local={transferLocal} unsorted={transferUnsorted} outgoing={transferOutgoing} " +
                $"amount={transferAmount} dispatched={transferDispatched} " +
                $"pathfinding={transferPathfinding} failed={transferFailed}");
        }

        private void LogVehicleSummary()
        {
            int vans = 0;
            int parkedVans = 0;
            int deliveringVans = 0;
            int collectingVans = 0;
            int returningVans = 0;
            int disabledVans = 0;
            int targetedVans = 0;
            int minVanCapacity = int.MaxValue;
            int maxVanCapacity = 0;
            long vanDeliveryLoad = 0;
            long vanCollectedLoad = 0;

            foreach ((RefRO<Game.Vehicles.PostVan> vanRef, Entity entity) in SystemAPI
                         .Query<RefRO<Game.Vehicles.PostVan>>()
                         .WithEntityAccess())
            {
                Game.Vehicles.PostVan van = vanRef.ValueRO;
                Game.Vehicles.PostVanFlags flags = van.m_State;

                vans++;
                vanDeliveryLoad += van.m_DeliveringMail;
                vanCollectedLoad += van.m_CollectedMail;

                if (EntityManager.HasComponent<Game.Vehicles.ParkedCar>(entity))
                {
                    parkedVans++;
                }

                if ((flags & Game.Vehicles.PostVanFlags.Delivering) != 0)
                {
                    deliveringVans++;
                }

                if ((flags & Game.Vehicles.PostVanFlags.Collecting) != 0)
                {
                    collectingVans++;
                }

                if ((flags & Game.Vehicles.PostVanFlags.Returning) != 0)
                {
                    returningVans++;
                }

                if ((flags & Game.Vehicles.PostVanFlags.Disabled) != 0)
                {
                    disabledVans++;
                }

                if (van.m_TargetRequest != Entity.Null)
                {
                    targetedVans++;
                }

                if (EntityManager.TryGetComponent(entity, out PrefabRef prefabRef) &&
                    EntityManager.TryGetComponent(prefabRef.m_Prefab, out PostVanData vanData))
                {
                    minVanCapacity = System.Math.Min(minVanCapacity, vanData.m_MailCapacity);
                    maxVanCapacity = System.Math.Max(maxVanCapacity, vanData.m_MailCapacity);
                }
            }

            int mailSemis = 0;
            int localSemis = 0;
            int unsortedSemis = 0;
            int outgoingSemis = 0;
            int loadedSemis = 0;
            int returningSemis = 0;
            int storageTransferSemis = 0;
            int postalOwnedSemis = 0;
            int outsideOrOtherOwnedSemis = 0;
            int returnLoads = 0;
            int minSemiCapacity = int.MaxValue;
            int maxSemiCapacity = 0;
            long semiLoad = 0;
            long semiReturnLoad = 0;

            foreach ((RefRO<Game.Vehicles.DeliveryTruck> truckRef, Entity entity) in SystemAPI
                         .Query<RefRO<Game.Vehicles.DeliveryTruck>>()
                         .WithEntityAccess())
            {
                Game.Vehicles.DeliveryTruck truck = truckRef.ValueRO;
                if (!IsMailResource(truck.m_Resource))
                {
                    continue;
                }

                mailSemis++;
                semiLoad += truck.m_Amount;

                if ((truck.m_Resource & Resource.LocalMail) != Resource.NoResource)
                {
                    localSemis++;
                }

                if ((truck.m_Resource & Resource.UnsortedMail) != Resource.NoResource)
                {
                    unsortedSemis++;
                }

                if ((truck.m_Resource & Resource.OutgoingMail) != Resource.NoResource)
                {
                    outgoingSemis++;
                }

                if ((truck.m_State & Game.Vehicles.DeliveryTruckFlags.Loaded) != 0)
                {
                    loadedSemis++;
                }

                if ((truck.m_State & Game.Vehicles.DeliveryTruckFlags.Returning) != 0)
                {
                    returningSemis++;
                }

                if ((truck.m_State & Game.Vehicles.DeliveryTruckFlags.StorageTransfer) != 0)
                {
                    storageTransferSemis++;
                }

                if (EntityManager.TryGetComponent(entity, out PrefabRef prefabRef) &&
                    EntityManager.TryGetComponent(
                        prefabRef.m_Prefab,
                        out DeliveryTruckData truckData))
                {
                    minSemiCapacity = System.Math.Min(minSemiCapacity, truckData.m_CargoCapacity);
                    maxSemiCapacity = System.Math.Max(maxSemiCapacity, truckData.m_CargoCapacity);
                }

                if (EntityManager.TryGetComponent(entity, out Owner owner) &&
                    EntityManager.HasComponent<Game.Buildings.PostFacility>(owner.m_Owner))
                {
                    postalOwnedSemis++;
                }
                else
                {
                    outsideOrOtherOwnedSemis++;
                }

                if (EntityManager.TryGetComponent(entity, out Game.Vehicles.ReturnLoad returnLoad) &&
                    IsMailResource(returnLoad.m_Resource))
                {
                    returnLoads++;
                    semiReturnLoad += returnLoad.m_Amount;
                }
            }

            AddLine(
                $"[MAIL VEHICLE VAN] total={vans} active={vans - parkedVans} parked={parkedVans} " +
                $"delivering={deliveringVans} collecting={collectingVans} returning={returningVans} " +
                $"disabled={disabledVans} targeted={targetedVans} " +
                $"payloadMin={(minVanCapacity == int.MaxValue ? 0 : minVanCapacity)} " +
                $"payloadMax={maxVanCapacity} deliveryLoad={vanDeliveryLoad} " +
                $"collectedLoad={vanCollectedLoad}");

            AddLine(
                $"[MAIL VEHICLE SEMI] total={mailSemis} local={localSemis} unsorted={unsortedSemis} " +
                $"outgoing={outgoingSemis} loaded={loadedSemis} returning={returningSemis} " +
                $"storageTransfer={storageTransferSemis} " +
                $"postalOwned={postalOwnedSemis} outsideOrOtherOwned={outsideOrOtherOwnedSemis} " +
                $"capacityMin={(minSemiCapacity == int.MaxValue ? 0 : minSemiCapacity)} " +
                $"capacityMax={maxSemiCapacity} load={semiLoad} returnLoads={returnLoads} " +
                $"returnAmount={semiReturnLoad}");
        }

        private static bool IsMailResource(Resource resource)
        {
            const Resource mailResources =
                Resource.LocalMail | Resource.UnsortedMail | Resource.OutgoingMail;

            return (resource & mailResources) != Resource.NoResource;
        }
    }
}
#endif
