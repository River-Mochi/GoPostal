// <copyright file="MailDiagnosticSystem.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Systems/Diagnostics/MailDiagnosticSystem.cs
// Purpose: read-only mail snapshots for controlled DEBUG tests.

#if DEBUG
namespace MagicMail
{
    using System;
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
    using Game.Tools;
    using Unity.Entities;

    /// <summary>
    /// Observes vanilla and MagicMail mail flow without changing any entity.</summary>
    public sealed partial class MailDiagnosticSystem : GameSystemBase
    {
        private const int kUpdatesPerDay = 16; // One snapshot per 90 in-game minutes.

        private EntityQuery m_FacilityQuery;
        private MailAccumulationSystem? m_MailAccumulationSystem;
        private PrefabSystem m_PrefabSystem = null!;
        private SimulationSystem m_SimulationSystem = null!;
        private readonly StringBuilder m_Report = new(4096);
        private int m_SnapshotNumber;

        public override int GetUpdateInterval(SystemUpdatePhase phase)
        {
            return 262144 / kUpdatesPerDay;
        }

        public override int GetUpdateOffset(SystemUpdatePhase phase)
        {
            return 240;
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();
            m_SimulationSystem = World.GetOrCreateSystemManaged<SimulationSystem>();
            m_MailAccumulationSystem = World.GetExistingSystemManaged<MailAccumulationSystem>();

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

            LogUtils.Info("[MAIL DIAG] Read-only diagnostic system created.");
        }

        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);

            if (mode == GameMode.Game &&
                (purpose == Purpose.NewGame || purpose == Purpose.LoadGame))
            {
                m_SnapshotNumber = 0;
                LogUtils.Info("[MAIL DIAG] City loaded; snapshots will begin after simulation starts.");
            }
        }

        protected override void OnUpdate()
        {
            Setting? settings = Mod.Settings;
            GameManager? gameManager = GameManager.instance;
            if (settings == null || gameManager == null || !gameManager.gameMode.IsGame())
            {
                return;
            }

            try
            {
                m_SnapshotNumber++;
                m_Report.Clear();
                AddLine(
                    $"[MAIL DIAG BEGIN] snapshot={m_SnapshotNumber} " +
                    $"frame={m_SimulationSystem.frameIndex}");

                LogSettings(settings);
                AddLine(
                    $"[MAIL MM LAST] facilities={MagicMailSystem.s_LastFacilityCount} " +
                    $"postOffices={MagicMailSystem.s_LastPostOfficeCount} " +
                    $"sorting={MagicMailSystem.s_LastSortingFacilityCount} " +
                    $"localTopups={MagicMailSystem.s_LastPostOfficeGets} " +
                    $"unsortedTopups={MagicMailSystem.s_LastSortingGets} " +
                    $"overflowCleanups={MagicMailSystem.s_LastOverflowClamps}");
                LogCityAndProducerSummary();
                LogRequestSummary();
                LogVehicleSummary();
                LogFacilityDetails();

                AddLine(
                    $"[MAIL DIAG END] snapshot={m_SnapshotNumber} " +
                    $"frame={m_SimulationSystem.frameIndex}");

                // One direct append avoids dozens of file opens per snapshot.
                LogUtils.Info(m_Report.ToString().TrimEnd());
            }
            catch (Exception ex)
            {
                // Diagnostics must never interrupt the simulation.
                LogUtils.Error($"[MAIL DIAG ERROR] snapshot={m_SnapshotNumber}", ex);
            }
        }

        private void LogSettings(Setting settings)
        {
            AddLine(
                $"[MAIL SETTINGS] preset={GetPresetName(settings)} " +
                $"POlocal={settings.PO_GetLocalMail} POthreshold={settings.PO_GettingThresholdPercentage}% " +
                $"POadd={settings.PO_GettingPercentage}% overflow={settings.FixMailOverflow} " +
                $"POoverflow={settings.PO_OverflowPercentage}% PSFoverflow={settings.PSF_OverflowPercentage}% " +
                $"PSFunsorted={settings.PSF_GetUnsortedMail} " +
                $"PSFthreshold={settings.PSF_GettingThresholdPercentage}% " +
                $"PSFadd={settings.PSF_GettingPercentage}% sort={settings.PSF_SortingSpeedPercentage}% " +
                $"storage={settings.PSF_StorageCapacityPercentage}% capacities={settings.ChangeCapacity} " +
                $"vanLoad={settings.PostVanMailLoadPercentage}% vans={settings.PostVanFleetSizePercentage}% " +
                $"trucks={settings.TruckCapacityPercentage}%");
        }

        private void LogCityAndProducerSummary()
        {
            if (m_MailAccumulationSystem == null)
            {
                m_MailAccumulationSystem = World.GetExistingSystemManaged<MailAccumulationSystem>();
            }

            int accumulated = m_MailAccumulationSystem?.LastAccumulatedMail ?? -1;
            int processed = m_MailAccumulationSystem?.LastProcessedMail ?? -1;
            string rawRatio = accumulated > 0
                ? ((double)processed / accumulated).ToString(
                    "0.000",
                    System.Globalization.CultureInfo.InvariantCulture)
                : "n/a";

            int producerCount = 0;
            int sendingBuildings = 0;
            int receivingBuildings = 0;
            int requestRefs = 0;
            int staleRequestRefs = 0;
            int mailDeliveredFlags = 0;
            long sendingBacklog = 0;
            long receivingBacklog = 0;

            foreach ((RefRO<Game.Buildings.MailProducer> producerRef, Entity entity) in SystemAPI
                         .Query<RefRO<Game.Buildings.MailProducer>>()
                         .WithNone<Destroyed, Deleted, Temp>()
                         .WithEntityAccess())
            {
                Game.Buildings.MailProducer producer = producerRef.ValueRO;
                int receiving = producer.receivingMail;

                producerCount++;
                sendingBacklog += producer.m_SendingMail;
                receivingBacklog += receiving;

                if (producer.m_SendingMail > 0)
                {
                    sendingBuildings++;
                }

                if (receiving > 0)
                {
                    receivingBuildings++;
                }

                if (producer.mailDelivered)
                {
                    mailDeliveredFlags++;
                }

                if (producer.m_MailRequest != Entity.Null)
                {
                    requestRefs++;
                    if (!EntityManager.Exists(producer.m_MailRequest))
                    {
                        staleRequestRefs++;
                    }
                }
            }

            AddLine(
                $"[MAIL CITY] accumulated={accumulated} processed={processed} rawRatio={rawRatio} " +
                $"producers={producerCount} sendBacklog={sendingBacklog} receiveBacklog={receivingBacklog} " +
                $"sendBuildings={sendingBuildings} receiveBuildings={receivingBuildings} " +
                $"requestRefs={requestRefs} staleRefs={staleRequestRefs} deliveredFlags={mailDeliveredFlags}");
        }

        private static string GetPresetName(Setting settings)
        {
            bool vanilla =
                !settings.PO_GetLocalMail &&
                !settings.FixMailOverflow &&
                !settings.PSF_GetUnsortedMail &&
                settings.PSF_SortingSpeedPercentage == 100 &&
                settings.PSF_StorageCapacityPercentage == 100 &&
                !settings.ChangeCapacity;

            if (vanilla)
            {
                return "VANILLA";
            }

            bool recommended =
                settings.PO_GetLocalMail &&
                settings.PO_GettingThresholdPercentage == 5 &&
                settings.PO_GettingPercentage == 10 &&
                settings.FixMailOverflow &&
                settings.PO_OverflowPercentage == 85 &&
                settings.PSF_OverflowPercentage == 85 &&
                settings.PSF_GetUnsortedMail &&
                settings.PSF_GettingThresholdPercentage == 5 &&
                settings.PSF_GettingPercentage == 10 &&
                settings.PSF_SortingSpeedPercentage == 200 &&
                settings.PSF_StorageCapacityPercentage == 100 &&
                settings.ChangeCapacity &&
                settings.PostVanMailLoadPercentage == 200 &&
                settings.PostVanFleetSizePercentage == 100 &&
                settings.TruckCapacityPercentage == 100;

            return recommended ? "RECOMMENDED" : "CUSTOM";
        }

        private void AddLine(string line)
        {
            m_Report.AppendLine(line);
        }
    }
}
#endif
