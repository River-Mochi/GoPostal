// <copyright file="MailDiagnosticSystem.Facilities.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Systems/Diagnostics/MailDiagnosticSystem.Facilities.cs
// Purpose: logs effective facility stats, buffers, flags, and assigned requests.

#if DEBUG
namespace MagicMail
{
    using System.Text;
    using Colossal.Entities;
    using CS2Shared.RiverMochi;
    using Game.Buildings;
    using Game.Economy;
    using Game.Pathfind;
    using Game.Prefabs;
    using Game.Routes;
    using Game.Simulation;
    using Unity.Collections;
    using Unity.Entities;

    public sealed partial class MailDiagnosticSystem
    {
        private struct FacilityTraffic
        {
            public int OwnedVans;
            public int ParkedVans;
            public int OwnedMailSemis;
            public int GuestMailSemis;
            public long OwnedSemiLoad;
            public long GuestSemiLoad;
            public int VanDispatches;
            public int SemiDispatches;
            public int OtherDispatches;
        }

        private void LogFacilityDetails()
        {
            using NativeArray<Entity> entities = m_FacilityQuery.ToEntityArray(Allocator.Temp);
            AddLine($"[MAIL FACILITIES] count={entities.Length}");

            foreach (Entity entity in entities)
            {
                if (!EntityManager.TryGetComponent(entity, out PrefabRef prefabRef) ||
                    !EntityManager.TryGetComponent(entity, out Game.Buildings.PostFacility facility) ||
                    !EntityManager.TryGetComponent(prefabRef.m_Prefab, out PostFacilityData prefabData) ||
                    !EntityManager.HasBuffer<Resources>(entity))
                {
                    LogUtils.WarnOnce(
                        "MM.DiagFacilityReadFailed",
                        () => $"[MAIL DIAG] Could not read facility {entity}.");
                    continue;
                }

                DynamicBuffer<Resources> resources = EntityManager.GetBuffer<Resources>(entity, true);
                PostFacilityData effectiveData = prefabData;
                int upgradeCount = 0;
                string upgradeNames = "none";

                if (EntityManager.HasBuffer<InstalledUpgrade>(entity))
                {
                    DynamicBuffer<InstalledUpgrade> upgrades =
                        EntityManager.GetBuffer<InstalledUpgrade>(entity, true);
                    upgradeCount = upgrades.Length;
                    upgradeNames = GetUpgradeNames(upgrades);
                    UpgradeUtils.CombineStats(EntityManager, ref effectiveData, upgrades);
                }

                int local = EconomyUtils.GetResources(Resource.LocalMail, resources);
                int unsorted = EconomyUtils.GetResources(Resource.UnsortedMail, resources);
                int outgoing = EconomyUtils.GetResources(Resource.OutgoingMail, resources);
                int storedTotal = local + unsorted + outgoing;
                int aiStoredBase = local + unsorted;
                int mailboxAmount = EntityManager.TryGetComponent(entity, out MailBox mailbox)
                    ? mailbox.m_MailAmount
                    : 0;

                string role = effectiveData.m_SortingRate == 0 ? "POST_OFFICE" : "SORTING";
                string fill = effectiveData.m_MailCapacity > 0
                    ? $"{storedTotal * 100.0 / effectiveData.m_MailCapacity:0.0}%"
                    : "n/a";

                FacilityTraffic traffic = GetFacilityTraffic(entity);
                string prefabName = GetPrefabName(prefabRef.m_Prefab);

                AddLine(
                    $"[MAIL FAC] entity={entity} prefab=\"{prefabName}\" role={role} " +
                    $"upgrades={upgradeCount}[{upgradeNames}] " +
                    $"prefabCap={prefabData.m_MailCapacity} effectiveCap={effectiveData.m_MailCapacity} " +
                    $"prefabSort={prefabData.m_SortingRate} effectiveSort={effectiveData.m_SortingRate} " +
                    $"vans={effectiveData.m_PostVanCapacity} semis={effectiveData.m_PostTruckCapacity}");

                AddLine(
                    $"[MAIL FAC STORE] entity={entity} L={local} U={unsorted} O={outgoing} " +
                    $"storedTotal={storedTotal} fill={fill} AIstoredBase=L+U={aiStoredBase} " +
                    $"outgoingOmittedByAI={outgoing} mailbox={mailboxAmount} processing={facility.m_ProcessingFactor}");

                AddLine(
                    $"[MAIL FAC STATE] entity={entity} flags={facility.m_Flags} " +
                    $"acceptPriority={facility.m_AcceptMailPriority:0.000} " +
                    $"deliverPriority={facility.m_DeliverMailPriority:0.000} " +
                    $"ownedVans={traffic.OwnedVans} activeVans={traffic.OwnedVans - traffic.ParkedVans} " +
                    $"parkedVans={traffic.ParkedVans} ownedSemis={traffic.OwnedMailSemis} " +
                    $"guestSemis={traffic.GuestMailSemis} ownedSemiLoad={traffic.OwnedSemiLoad} " +
                    $"guestSemiLoad={traffic.GuestSemiLoad} dispatchVan={traffic.VanDispatches} " +
                    $"dispatchSemi={traffic.SemiDispatches} dispatchOther={traffic.OtherDispatches}");

                AddLine(
                    $"[MAIL FAC REQUEST] entity={entity} " +
                    $"deliverToFacility={FormatTransferRequest(facility.m_MailDeliverRequest)} " +
                    $"receiveFromFacility={FormatTransferRequest(facility.m_MailReceiveRequest)} " +
                    $"target={FormatAnyRequest(facility.m_TargetRequest)}");
            }
        }

        private FacilityTraffic GetFacilityTraffic(Entity facility)
        {
            FacilityTraffic result = default;

            if (EntityManager.HasBuffer<Game.Vehicles.OwnedVehicle>(facility))
            {
                DynamicBuffer<Game.Vehicles.OwnedVehicle> ownedVehicles =
                    EntityManager.GetBuffer<Game.Vehicles.OwnedVehicle>(facility, true);

                foreach (Game.Vehicles.OwnedVehicle ownedVehicle in ownedVehicles)
                {
                    Entity vehicle = ownedVehicle.m_Vehicle;
                    if (EntityManager.HasComponent<Game.Vehicles.PostVan>(vehicle))
                    {
                        result.OwnedVans++;
                        if (EntityManager.HasComponent<Game.Vehicles.ParkedCar>(vehicle))
                        {
                            result.ParkedVans++;
                        }
                    }
                    else if (EntityManager.TryGetComponent(
                                 vehicle,
                                 out Game.Vehicles.DeliveryTruck truck) &&
                             IsMailResource(truck.m_Resource))
                    {
                        result.OwnedMailSemis++;
                        result.OwnedSemiLoad += truck.m_Amount;
                    }
                }
            }

            if (EntityManager.HasBuffer<Game.Vehicles.GuestVehicle>(facility))
            {
                DynamicBuffer<Game.Vehicles.GuestVehicle> guestVehicles =
                    EntityManager.GetBuffer<Game.Vehicles.GuestVehicle>(facility, true);

                foreach (Game.Vehicles.GuestVehicle guestVehicle in guestVehicles)
                {
                    if (EntityManager.TryGetComponent(
                            guestVehicle.m_Vehicle,
                            out Game.Vehicles.DeliveryTruck truck) &&
                        IsMailResource(truck.m_Resource))
                    {
                        result.GuestMailSemis++;
                        result.GuestSemiLoad += truck.m_Amount;
                    }
                }
            }

            if (EntityManager.HasBuffer<ServiceDispatch>(facility))
            {
                DynamicBuffer<ServiceDispatch> dispatches =
                    EntityManager.GetBuffer<ServiceDispatch>(facility, true);

                foreach (ServiceDispatch dispatch in dispatches)
                {
                    if (EntityManager.HasComponent<PostVanRequest>(dispatch.m_Request))
                    {
                        result.VanDispatches++;
                    }
                    else if (EntityManager.HasComponent<MailTransferRequest>(dispatch.m_Request))
                    {
                        result.SemiDispatches++;
                    }
                    else
                    {
                        result.OtherDispatches++;
                    }
                }
            }

            return result;
        }

        private string FormatTransferRequest(Entity requestEntity)
        {
            if (requestEntity == Entity.Null)
            {
                return "none";
            }

            if (!EntityManager.Exists(requestEntity))
            {
                return $"{requestEntity}:missing";
            }

            if (!EntityManager.TryGetComponent(requestEntity, out MailTransferRequest request))
            {
                return $"{requestEntity}:not-transfer";
            }

            return $"{requestEntity}:{request.m_Flags}:amount={request.m_Amount}:" +
                   $"priority={request.m_Priority:0.000}:{FormatRequestState(requestEntity)}";
        }

        private string FormatAnyRequest(Entity requestEntity)
        {
            if (requestEntity == Entity.Null)
            {
                return "none";
            }

            if (!EntityManager.Exists(requestEntity))
            {
                return $"{requestEntity}:missing";
            }

            if (EntityManager.TryGetComponent(requestEntity, out PostVanRequest vanRequest))
            {
                return $"{requestEntity}:VAN:{vanRequest.m_Flags}:" +
                       $"priority={vanRequest.m_Priority}:{FormatRequestState(requestEntity)}";
            }

            if (EntityManager.HasComponent<MailTransferRequest>(requestEntity))
            {
                return FormatTransferRequest(requestEntity);
            }

            return $"{requestEntity}:other";
        }

        private string FormatRequestState(Entity requestEntity)
        {
            string state =
                EntityManager.HasComponent<Dispatched>(requestEntity) ? "dispatched" : "waiting";

            if (EntityManager.HasComponent<PathInformation>(requestEntity))
            {
                state += "+path";
            }

            if (EntityManager.TryGetComponent(requestEntity, out ServiceRequest serviceRequest))
            {
                state += $"+fail={serviceRequest.m_FailCount}+cooldown={serviceRequest.m_Cooldown}";
            }

            return state;
        }

        private string GetUpgradeNames(DynamicBuffer<InstalledUpgrade> upgrades)
        {
            if (upgrades.Length == 0)
            {
                return "none";
            }

            var names = new StringBuilder();
            for (int i = 0; i < upgrades.Length; i++)
            {
                if (i != 0)
                {
                    names.Append(',');
                }

                Entity upgrade = upgrades[i].m_Upgrade;
                if (EntityManager.TryGetComponent(upgrade, out PrefabRef prefabRef))
                {
                    names.Append(GetPrefabName(prefabRef.m_Prefab));
                }
                else
                {
                    names.Append(upgrade);
                }
            }

            return names.ToString();
        }

        private string GetPrefabName(Entity prefabEntity)
        {
            return m_PrefabSystem.TryGetPrefab(prefabEntity, out PrefabBase prefabBase)
                ? prefabBase.name
                : prefabEntity.ToString();
        }
    }
}
#endif
