// Localization/LocaleEN.cs
// English locale en-US

namespace MagicPostMaster
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// English localization source for Magic PostMaster [MPM].</summary>
    public sealed class LocaleEN : IDictionarySource
    {
        private readonly Setting m_Setting;

        /// <summary>
        /// Constructs the English locale generator.</summary>
        /// <param name="setting">Settings object used for locale IDs.</param>
        public LocaleEN(Setting setting)
        {
            m_Setting = setting;
        }

        /// <summary>
        /// Generates all English localization entries for this mod.</summary>
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod title
                { m_Setting.GetSettingsLocaleID(), "Magic PostMaster [MPM]" },

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
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "Fix Low Local mail" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "When enabled, extra local mail appears \"magically\" if storage is too low.\n " +
                    "Does not spawn extra vans — the mail just appears in the building for use." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "Local mail threshold" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "If local mail goes below this percentage you choose, then, the post office pulls in more local mail.\n" +
                    "It is the percentage of max storage." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "Local mail fetch amount" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "Percentage to add when fetching local mail (magic top-up).\n" +
                    "20% = add 2,000 if vanilla max = 10,000"},

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)),
                    "Fix mail overflow" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "When enabled, post offices and\\or sorting facilities perform a small \"magic\" cleanup when\n " +
                    "excess mail thresholds are reached: excess stored mail is treated as delivered and removed.\n " +
                    "This fix prevents facilities from getting stuck \"full\" forever.\n " +
                    "Disable this to keep pure vanilla behavior." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "Post office overflow threshold" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "When total mail reaches this percentage of storage, the overflow handling is triggered." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "Sorting overflow threshold" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "When total mail at the sorting facilities reaches this percentage of total storage,\n " +
                    "overflow \"magic\" handling is triggered." },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "Change capacities" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "Enable this to modify van and truck capacities. When off, all capacity sliders below are\n " +
                    "hidden and vanilla (game) values are used even if you left the sliders at different numbers." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "Post van mail load" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "Controls how much mail each post van can carry. 100% = vanilla payload." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "Post van fleet size" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "Controls how many post vans each postal building can own and dispatch.\n" +
                    "<100% = vanilla fleet size.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "Post truck fleet size" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "Controls how many post trucks each sorting facility (and any facility with Post-trucks)\n " +
                    "can own and dispatch.\n " +
                    "<100% = vanilla fleet size.>" },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "Sorting speed" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "Sorting speed multiplier for postal **Sorting** facilities. Applies to the facility's base sorting rate\n " +
                    "<100% = vanilla>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "Sorting storage capacity" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "Controls **mail storage** at a Sorting facility.\n " +
                    "<100% = vanilla>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "Fix low unsorted mail" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "When enabled, \"magic\" extra unsorted mail appears in Sorting facilities if storage supplies get too low.\n " +
                    "This keeps sorting active without waiting for deliveries.\n" +
                    "Temp fix for current bug where Sorting facilities don't get enough mail if Cargo harbor is present" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "Unsorted mail threshold" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "If unsorted mail goes below this percentage of capacity, the facility magically pulls in the \"fetch\" amount of unsorted mail." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "Unsorted mail fetch amount" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "Additional mail to add when fetching unsorted mail (magic top-up).\n" +
                    "Percentage of max storage capacity." },

                // ---- Reset ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "Reset to game defaults" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)),
                    "Restore all postal settings to the game’s original behaviour (vanilla)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "Reset to Recommended" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "Quick Start: apply Recommended postal settings." },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), "" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)),
                    "Summary of post offices, post vans, sorting facilities, and post trucks processed in the last game update (~every game 45 min)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "City mail" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "Shows recent city-wide mail flow.\n\n" +
                     "Accumulated = how much mail citizens generated.\n" +
                     "Processed   = how much mail the network actually handled.\n\n" +
                     "- If Processed is consistently higher than Accumulated, your postal network has enough capacity " +
                     "and you could lower the postal budget or vehicles if desired.\n" +
                     "- If Accumulated stays above Processed for long periods, the city is generating more mail than\n " +
                     "can handle — add more facilities, vans or tweak your settings." },

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
                    "Open **Paradox** webpage for **Magic PostMaster** and other mods." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)),
                    "Open **Discord** feedback chat in a browser." },
            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
