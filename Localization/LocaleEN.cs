// Localization/LocaleEN.cs
// English locale for PostMaster [PM].

namespace PostMaster
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// English localization source for PostMaster [PM].</summary>
    public sealed class LocaleEN : IDictionarySource
    {
        private readonly Setting m_Setting;

        /// <summary>
        /// Constructs the English locale generator.
        /// </summary>
        /// <param name="setting">Settings object used for locale IDs.</param>
        public LocaleEN(Setting setting)
        {
            m_Setting = setting;
        }

        /// <summary>
        /// Generates all English localization entries for this mod.
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod title
                { m_Setting.GetSettingsLocaleID(), "PostMaster [PM]" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "Actions" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab),  "Status" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab),   "About" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup),          "Post office" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup),             "Post vans & trucks" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "Sorting facility" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup),               "Reset" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "City scan" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "Last update" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup),  "Info" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "Links" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "Magic local mail top-up" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "When enabled, post offices \"magic\" in extra local mail if their storage is too low. "
                    + "This does not spawn vans — the mail just appears in the building so it can be processed." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "Local mail threshold" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "If local mail is below this percentage of capacity, the post office pulls in more local mail." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "Local mail fetch amount" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "Percentage of capacity to add when fetching local mail (magic top-up)." },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)),
                    "Fix mail overflow (magic cleanup)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "When enabled, post offices and sorting facilities perform a small \"magic\" cleanup once "
                    + "their overflow thresholds are reached: excess stored mail is treated as delivered and removed, "
                    + "so buildings don’t get stuck full forever. Turn this off to keep pure vanilla overflow behavior." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "Post office overflow threshold" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "When total mail at post offices reaches this percentage of capacity, overflow handling is triggered. "
                    + "With the default 90%, nothing happens until a post office is nearly full." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "Sorting overflow threshold" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "When total mail at sorting facilities reaches this percentage of capacity, "
                    + "overflow handling is triggered. With the default 90%, only very full facilities are touched." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.MoreDeliveries)),
                    "Make more deliveries if needed" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.MoreDeliveries)),
                    "Lets PostMaster be more eager about deliveries when needed. The intent is that if a postal building "
                    + "is holding a backlog of local mail and still has free post vans available, it can request extra "
                    + "delivery trips earlier than vanilla, helping post offices empty out instead of sitting nearly full. "
                    + "If all vans are already in use, this toggle does nothing until a van becomes available again and "
                    + "the game behaves like vanilla. (In the current build this option prepares the behavior and may not "
                    + "yet change deliveries.)" },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)),
                    "Change capacities" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "Enable this to modify van and truck capacities. When off, all capacity sliders below are "
                    + "hidden and vanilla values are used." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "Post van mail load" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "Controls how much mail each post van can carry. 100% = vanilla payload." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "Post van fleet size" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "Controls how many post vans each postal building can own and dispatch. 100% = vanilla fleet size." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "Post truck fleet size" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "Controls how many post trucks each sorting facility (and any other facility with trucks) "
                    + "can own and dispatch. 100% = vanilla fleet size." },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "Sorting speed" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "Sorting speed multiplier for postal sorting facilities. Applies to the facility's base sorting rate "
                    + "(100% = vanilla)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "Sorting storage capacity" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "Controls how much mail sorting facilities can store. Only facilities that actually sort mail "
                    + "are affected (100% = vanilla)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "Magic unsorted mail top-up" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "When enabled, sorting facilities \"magic\" in extra unsorted mail if storage is too low. "
                    + "This keeps sorting lines busy without waiting for extra vans." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "Unsorted mail threshold" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "If unsorted mail is below this percentage of capacity, the facility pulls in more unsorted mail." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "Unsorted mail fetch amount" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "Percentage of capacity to add when fetching unsorted mail (magic top-up)." },

                // ---- Reset ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "Reset to game defaults" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)),
                    "Restore all postal settings to the game’s original behaviour." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "Reset to recommended" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "Apply PostMaster’s recommended postal settings." },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), "" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)),
                    "Summary of post offices, post vans, sorting facilities, and post trucks processed in the last update." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "City mail" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "Shows recent city-wide mail flow.\n\n"
                    + "Accumulated = how much mail citizens generated.\n"
                    + "Processed   = how much mail the network actually handled.\n\n"
                    + "If Processed is consistently higher than Accumulated, your postal network has enough capacity "
                    + "and you could even lower the postal budget.\n"
                    + "If Accumulated stays above Processed for long periods, the city is generating more mail than it "
                    + "can handle — add more facilities, vans or tweak your settings." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "Activity" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)),
                    "Counts of mail pulls and overflow cleanups performed in the last update." },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)),
                    "Display name of this mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)),
                    "Current mod version." },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)),
                    "Open **Paradox** webpage for **PostMaster** and other mods." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)),
                    "Open **Discord** feedback chat in a browser." },
            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.
        /// </summary>
        public void Unload()
        {
        }
    }
}
