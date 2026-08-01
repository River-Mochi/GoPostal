// <copyright file="MailDiagnosticSystem.ProducerRequests.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Systems/Diagnostics/MailDiagnosticSystem.ProducerRequests.cs
// Purpose: proves whether the same missing producer request survives multiple snapshots.

#if DEBUG
namespace MagicMail
{
    using System.Collections.Generic;
    using Game.Buildings;
    using Unity.Entities;

    public sealed partial class MailDiagnosticSystem
    {
        private readonly Dictionary<Entity, MissingProducerRequestState>
            m_MissingProducerRequests = new();
        private readonly HashSet<Entity> m_MissingProducerRequestsSeen = new();
        private readonly List<Entity> m_MissingProducerRequestsToRemove = new();
        private readonly List<KeyValuePair<Entity, MissingProducerRequestState>>
            m_PersistentMissingProducerRequests = new();

        private void ResetProducerRequestEvidence()
        {
            m_MissingProducerRequests.Clear();
            m_MissingProducerRequestsSeen.Clear();
            m_MissingProducerRequestsToRemove.Clear();
            m_PersistentMissingProducerRequests.Clear();
        }

        private void BeginProducerRequestEvidence()
        {
            m_MissingProducerRequestsSeen.Clear();
        }

        private void TrackMissingProducerRequest(
            Entity producerEntity,
            MailProducer producer)
        {
            m_MissingProducerRequestsSeen.Add(producerEntity);

            if (m_MissingProducerRequests.TryGetValue(
                    producerEntity,
                    out MissingProducerRequestState state) &&
                state.RequestEntity == producer.m_MailRequest)
            {
                state.ConsecutiveSnapshots++;
            }
            else
            {
                state = new MissingProducerRequestState
                {
                    RequestEntity = producer.m_MailRequest,
                    ConsecutiveSnapshots = 1,
                };
            }

            state.SendingMail = producer.m_SendingMail;
            state.ReceivingMail = producer.receivingMail;
            m_MissingProducerRequests[producerEntity] = state;
        }

        private void EndProducerRequestEvidence()
        {
            m_MissingProducerRequestsToRemove.Clear();
            foreach (Entity producerEntity in m_MissingProducerRequests.Keys)
            {
                if (!m_MissingProducerRequestsSeen.Contains(producerEntity))
                {
                    m_MissingProducerRequestsToRemove.Add(producerEntity);
                }
            }

            foreach (Entity producerEntity in m_MissingProducerRequestsToRemove)
            {
                m_MissingProducerRequests.Remove(producerEntity);
            }

            var persistentTwoPlus = 0;
            var persistentThreePlus = 0;
            var maxSnapshots = 0;
            m_PersistentMissingProducerRequests.Clear();

            foreach (KeyValuePair<Entity, MissingProducerRequestState> pair in
                     m_MissingProducerRequests)
            {
                int snapshots = pair.Value.ConsecutiveSnapshots;
                if (snapshots >= 2)
                {
                    persistentTwoPlus++;
                    m_PersistentMissingProducerRequests.Add(pair);
                }

                if (snapshots >= 3)
                {
                    persistentThreePlus++;
                }

                if (snapshots > maxSnapshots)
                {
                    maxSnapshots = snapshots;
                }
            }

            AddLine(
                $"[MAIL REQUEST REF] missingNow={m_MissingProducerRequests.Count} " +
                $"sameMissing2Plus={persistentTwoPlus} " +
                $"sameMissing3Plus={persistentThreePlus} maxSnapshots={maxSnapshots}");

            m_PersistentMissingProducerRequests.Sort(
                static (left, right) =>
                    right.Value.ConsecutiveSnapshots.CompareTo(
                        left.Value.ConsecutiveSnapshots));

            int detailCount = System.Math.Min(5, m_PersistentMissingProducerRequests.Count);
            for (int i = 0; i < detailCount; i++)
            {
                KeyValuePair<Entity, MissingProducerRequestState> pair =
                    m_PersistentMissingProducerRequests[i];
                MissingProducerRequestState state = pair.Value;

                AddLine(
                    $"[MAIL REQUEST REF DETAIL] producer={pair.Key} " +
                    $"request={state.RequestEntity}:missing " +
                    $"snapshots={state.ConsecutiveSnapshots} " +
                    $"send={state.SendingMail} receive={state.ReceivingMail}");
            }
        }

        private struct MissingProducerRequestState
        {
            public Entity RequestEntity;
            public int ConsecutiveSnapshots;
            public int SendingMail;
            public int ReceivingMail;
        }
    }
}
#endif
