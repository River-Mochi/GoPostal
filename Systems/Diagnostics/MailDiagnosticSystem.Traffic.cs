// <copyright file="MailDiagnosticSystem.Traffic.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Systems/Diagnostics/MailDiagnosticSystem.Traffic.cs
// Purpose: summarizes pending requests, post vans, and mail delivery trucks.

#if DEBUG
namespace MagicMail
{
    using Game.Common;
    using Game.Economy;
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

                if (EntityManager.HasComponent<Game.Pathfind.PathInformation>(entity))
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

                if (EntityManager.HasComponent<Game.Pathfind.PathInformation>(entity))
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
                $"[MAIL REQUEST TRUCK] total={transferRequests} deliverToFacility={transferDeliver} " +
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

                if (EntityManager.HasComponent<Game.Prefabs.PrefabRef>(entity))
                {
                    Game.Prefabs.PrefabRef prefabRef =
                        EntityManager.GetComponentData<Game.Prefabs.PrefabRef>(entity);

                    if (EntityManager.HasComponent<Game.Prefabs.PostVanData>(prefabRef.m_Prefab))
                    {
                        Game.Prefabs.PostVanData vanData =
                            EntityManager.GetComponentData<Game.Prefabs.PostVanData>(prefabRef.m_Prefab);
                        minVanCapacity = System.Math.Min(minVanCapacity, vanData.m_MailCapacity);
                        maxVanCapacity = System.Math.Max(maxVanCapacity, vanData.m_MailCapacity);
                    }
                }
            }

            int mailTrucks = 0;
            int localTrucks = 0;
            int unsortedTrucks = 0;
            int outgoingTrucks = 0;
            int loadedTrucks = 0;
            int returningTrucks = 0;
            int storageTransferTrucks = 0;
            int postalOwnedTrucks = 0;
            int outsideOrOtherOwnedTrucks = 0;
            int returnLoads = 0;
            int minTruckCapacity = int.MaxValue;
            int maxTruckCapacity = 0;
            long truckLoad = 0;
            long truckReturnLoad = 0;

            foreach ((RefRO<Game.Vehicles.DeliveryTruck> truckRef, Entity entity) in SystemAPI
                         .Query<RefRO<Game.Vehicles.DeliveryTruck>>()
                         .WithEntityAccess())
            {
                Game.Vehicles.DeliveryTruck truck = truckRef.ValueRO;
                if (!IsMailResource(truck.m_Resource))
                {
                    continue;
                }

                mailTrucks++;
                truckLoad += truck.m_Amount;

                if ((truck.m_Resource & Resource.LocalMail) != Resource.NoResource)
                {
                    localTrucks++;
                }

                if ((truck.m_Resource & Resource.UnsortedMail) != Resource.NoResource)
                {
                    unsortedTrucks++;
                }

                if ((truck.m_Resource & Resource.OutgoingMail) != Resource.NoResource)
                {
                    outgoingTrucks++;
                }

                if ((truck.m_State & Game.Vehicles.DeliveryTruckFlags.Loaded) != 0)
                {
                    loadedTrucks++;
                }

                if ((truck.m_State & Game.Vehicles.DeliveryTruckFlags.Returning) != 0)
                {
                    returningTrucks++;
                }

                if ((truck.m_State & Game.Vehicles.DeliveryTruckFlags.StorageTransfer) != 0)
                {
                    storageTransferTrucks++;
                }

                if (EntityManager.HasComponent<Game.Prefabs.PrefabRef>(entity))
                {
                    Game.Prefabs.PrefabRef prefabRef =
                        EntityManager.GetComponentData<Game.Prefabs.PrefabRef>(entity);

                    if (EntityManager.HasComponent<Game.Prefabs.DeliveryTruckData>(prefabRef.m_Prefab))
                    {
                        Game.Prefabs.DeliveryTruckData truckData =
                            EntityManager.GetComponentData<Game.Prefabs.DeliveryTruckData>(prefabRef.m_Prefab);
                        minTruckCapacity = System.Math.Min(minTruckCapacity, truckData.m_CargoCapacity);
                        maxTruckCapacity = System.Math.Max(maxTruckCapacity, truckData.m_CargoCapacity);
                    }
                }

                if (EntityManager.HasComponent<Game.Common.Owner>(entity))
                {
                    Game.Common.Owner owner =
                        EntityManager.GetComponentData<Game.Common.Owner>(entity);
                    if (EntityManager.HasComponent<Game.Buildings.PostFacility>(owner.m_Owner))
                    {
                        postalOwnedTrucks++;
                    }
                    else
                    {
                        outsideOrOtherOwnedTrucks++;
                    }
                }
                else
                {
                    outsideOrOtherOwnedTrucks++;
                }

                if (EntityManager.HasComponent<Game.Vehicles.ReturnLoad>(entity))
                {
                    Game.Vehicles.ReturnLoad returnLoad =
                        EntityManager.GetComponentData<Game.Vehicles.ReturnLoad>(entity);
                    if (IsMailResource(returnLoad.m_Resource))
                    {
                        returnLoads++;
                        truckReturnLoad += returnLoad.m_Amount;
                    }
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
                $"[MAIL VEHICLE TRUCK] total={mailTrucks} local={localTrucks} " +
                $"unsorted={unsortedTrucks} outgoing={outgoingTrucks} loaded={loadedTrucks} " +
                $"returning={returningTrucks} storageTransfer={storageTransferTrucks} " +
                $"postalOwned={postalOwnedTrucks} outsideOrOtherOwned={outsideOrOtherOwnedTrucks} " +
                $"capacityMin={(minTruckCapacity == int.MaxValue ? 0 : minTruckCapacity)} " +
                $"capacityMax={maxTruckCapacity} load={truckLoad} returnLoads={returnLoads} " +
                $"returnAmount={truckReturnLoad}");
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
