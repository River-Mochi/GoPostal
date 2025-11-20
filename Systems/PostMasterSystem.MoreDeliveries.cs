// Systems/PostMasterSystem.MoreDeliveries.cs
// Partial: "More Deliveries" toggle wiring.
//
// When enabled, this gently nudges the vanilla PostFacilityAISystem to
// request post vans more eagerly, but ONLY when:
//   - there is free van capacity (van flags set), and
//   - the facility doesn't already have a target request in flight.
//
// Capacity, routing, and thresholds remain governed by vanilla code.

namespace PostMaster
{
    using Game.Buildings;
    using Game.Economy;
    using Game.Prefabs;
    using Game.Settings;
    using Game.Simulation;
    using Unity.Entities;

    public partial class PostMasterSystem
    {
        /// <summary>
        /// Invoked from PostMasterSystem.OnUpdate for each post facility.
        /// </summary>
        private void MaybeNudgeDeliveries(
            EntityManager entityManager,
            Entity facilityEntity,
            Entity prefabEntity,
            PostFacilityData prefabPostFacilityData,
            Setting settings,
            bool moreDeliveries)
        {
            if (!moreDeliveries)
            {
                return;
            }

            // Relies entirely on vanilla PostFacility + flags to know whether it's
            // safe to nudge. If anything is missing, bail out.
            if (!entityManager.HasComponent<Game.Buildings.PostFacility>(facilityEntity))
            {
                return;
            }

            Game.Buildings.PostFacility postFacility =
                entityManager.GetComponentData<Game.Buildings.PostFacility>(facilityEntity);

            // Explicit "free vans" check:
            // In PostFacilityAISystem, these flags are only set when num2 > 0,
            // i.e. there is remaining van capacity AND either:
            //   - there is local mail to deliver, or
            //   - there is free storage to collect mail.
            const PostFacilityFlags VanFlags =
                PostFacilityFlags.CanDeliverMailWithVan |
                PostFacilityFlags.CanCollectMailWithVan;

            if ((postFacility.m_Flags & VanFlags) == 0)
            {
                // No vans free, or nothing useful to deliver/collect.
                // Behaviour stays purely vanilla.
                return;
            }

            // Don't spam new requests if the facility already has a target request
            // tracked by vanilla. This mirrors RequestTargetIfNeeded's first guard.
            Entity targetRequest = postFacility.m_TargetRequest;
            if (targetRequest != Entity.Null &&
                entityManager.HasComponent<ServiceRequest>(targetRequest))
            {
                return;
            }

            // At this point:
            //   - MoreDeliveries is ON
            //   - Facility has at least one free van and useful work
            //   - No existing target request is active
            //
            // We can safely create an extra vanilla-style PostVanRequest. Capacity
            // is still enforced inside PostFacilityAISystem via its own counters.

            Entity requestEntity = entityManager.CreateEntity();
            entityManager.AddComponentData(requestEntity, new ServiceRequest(true));

            // PostVanRequest signature in vanilla:
            //   new PostVanRequest(facility, flags, count)
            //
            // PostFacilityAISystem.TrySpawnPostVan never reads the count; it uses
            // its own "availableDeliveryVans" counter instead. So we can simply
            // pass 1 here without affecting capacity safety.
            entityManager.AddComponentData(
                requestEntity,
                new PostVanRequest(
                    facilityEntity,
                    (PostVanRequestFlags)0,
                    1));

            // Use the same request group (32) as vanilla RequestTargetIfNeeded.
            entityManager.AddComponentData(requestEntity, new RequestGroup(32u));

#if DEBUG
            Mod.s_Log.Debug(
                $"[MoreDeliveries] Nudged facility {facilityEntity.Index} to request an extra van. " +
                "Free van capacity is available and no target request was active.");
#endif
        }
    }
}
