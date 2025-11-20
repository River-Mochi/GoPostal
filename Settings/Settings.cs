// Settings/Setting.cs
// Options UI and configuration for PostMaster [PM].

namespace PostMaster
{
    using System;
    using Colossal.IO.AssetDatabase;
    using Game.Modding;
    using Game.Settings;
    using Game.UI;
    using Unity.Entities;
    using UnityEngine;

    /// <summary>
    /// Settings definition and UI bindings for PostMaster [PM].
    /// </summary>
    [FileLocation("ModsSettings/PostMaster/PostMaster")]
    [SettingsUITabOrder(
        kActionsTab,
        kStatusTab,
        kAboutTab)]
    [SettingsUIGroupOrder(
        ResetGroup,
        PostOfficeGroup,
        PostVanGroup,
        PostSortingFacilityGroup,
        StatusSummaryGroup,
        StatusActivityGroup,
        kAboutInfoGroup,
        kAboutLinksGroup)]
    [SettingsUIShowGroupName(
        PostOfficeGroup,
        PostSortingFacilityGroup,
        PostVanGroup,
        ResetGroup,
        StatusSummaryGroup,
        StatusActivityGroup,
        kAboutLinksGroup)]
    public sealed class Setting : ModSetting
    {
        // ---- TABS ----

        public const string kActionsTab = "Actions";
        public const string kStatusTab = "Status";
        public const string kAboutTab = "About";

        // ---- ACTION GROUPS (Actions tab) ----

        public const string PostOfficeGroup = "PostOffice";
        public const string PostSortingFacilityGroup = "PostSortingFacility";
        public const string PostVanGroup = "PostVan";
        public const string ResetGroup = "Reset";

        // ---- STATUS GROUPS (Status tab) ----

        public const string StatusSummaryGroup = "StatusSummary";
        public const string StatusActivityGroup = "StatusActivity";

        // ---- ABOUT GROUPS (About tab) ----

        public const string kAboutInfoGroup = "AboutInfo";
        public const string kAboutLinksGroup = "AboutLinks";

        // ---- LINKS ----

        private const string kUrlParadox =
            "https://mods.paradoxplaza.com/uploaded?orderBy=desc&sortBy=best&time=alltime";
        private const string kUrlDiscord =
            "https://discord.gg/HTav7ARPs2";

        /// <summary>
        /// Internal flag used to avoid resetting options on every load.
        /// </summary>
        [SettingsUIHidden]
        public bool NotFirstTime
        {
            get;
            set;
        }

        /// <summary>
        /// Constructs the settings object and initializes defaults on first creation.
        /// </summary>
        /// <param name="mod">Mod instance passed by the game.</param>
        public Setting(IMod mod)
            : base(mod)
        {
            // Only apply defaults the first time this settings asset is created.
            if (!NotFirstTime)
            {
                SetDefaults();
                NotFirstTime = true;
            }
        }

        /// <summary>
        /// Applies settings at runtime and ensures the managed system is enabled.
        /// </summary>
        public override void Apply()
        {
            base.Apply();

            World? world = World.DefaultGameObjectInjectionWorld;
            PostMasterSystem? system = world?.GetExistingSystemManaged<PostMasterSystem>();
            if (system != null)
            {
                system.Enabled = true;
            }
        }

        // --------------------------------------------------------------------
        // ACTIONS TAB: RESET BUTTONS (top)
        // --------------------------------------------------------------------

        [SettingsUIButtonGroup(ResetGroup)]
        [SettingsUISection(kActionsTab, ResetGroup)]
        [SettingsUIButton]
        public bool ResetToVanilla
        {
            set
            {
                if (!value)
                {
                    return;
                }

                SetToVanilla();
                Apply();
            }
        }

        [SettingsUIButtonGroup(ResetGroup)]
        [SettingsUISection(kActionsTab, ResetGroup)]
        [SettingsUIButton]
        public bool ResetToRecommend
        {
            set
            {
                if (!value)
                {
                    return;
                }

                SetDefaults();
                Apply();
            }
        }

        // --------------------------------------------------------------------
        // ACTIONS TAB: POST OFFICE OPTIONS
        // --------------------------------------------------------------------

        [SettingsUISection(kActionsTab, PostOfficeGroup)]
        public bool PO_GetLocalMail
        {
            get;
            set;
        }

        [SettingsUISection(kActionsTab, PostOfficeGroup)]
        [SettingsUISlider(
            min = 0,
            max = 100,
            step = 1,
            scalarMultiplier = 1,
            unit = Unit.kPercentage)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(PO_GetLocalMail), true)]
        public int PO_GettingThresholdPercentage
        {
            get;
            set;
        }

        [SettingsUISection(kActionsTab, PostOfficeGroup)]
        [SettingsUISlider(
            min = 0,
            max = 100,
            step = 1,
            scalarMultiplier = 1,
            unit = Unit.kPercentage)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(PO_GetLocalMail), true)]
        public int PO_GettingPercentage
        {
            get;
            set;
        }

        /// <summary>
        /// Global overflow fix toggle (post offices + sorting facilities).
        /// </summary>
        [SettingsUISection(kActionsTab, PostOfficeGroup)]
        public bool FixMailOverflow
        {
            get;
            set;
        }

        [SettingsUISection(kActionsTab, PostOfficeGroup)]
        [SettingsUISlider(
            min = 0,
            max = 100,
            step = 1,
            scalarMultiplier = 1,
            unit = Unit.kPercentage)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(FixMailOverflow), true)]
        public int PO_OverflowPercentage
        {
            get;
            set;
        }

        [SettingsUISection(kActionsTab, PostOfficeGroup)]
        [SettingsUISlider(
            min = 0,
            max = 100,
            step = 1,
            scalarMultiplier = 1,
            unit = Unit.kPercentage)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(FixMailOverflow), true)]
        public int PSF_OverflowPercentage
        {
            get;
            set;
        }

        /// <summary>
        /// Extra deliveries toggle: allow post offices to be more eager
        /// about deliveries when needed and when vans are free.
        /// (Currently UI-only until wired into the main system.)
        /// </summary>
        [SettingsUISection(kActionsTab, PostOfficeGroup)]
        public bool MoreDeliveries
        {
            get;
            set;
        }

        // --------------------------------------------------------------------
        // ACTIONS TAB: POST VAN OPTIONS (second)
        // --------------------------------------------------------------------

        /// <summary>
        /// Master toggle for changing postal capacities (vans, trucks, payload).
        /// </summary>
        [SettingsUISection(kActionsTab, PostVanGroup)]
        public bool ChangeCapacity
        {
            get;
            set;
        }

        /// <summary>
        /// Post van mail load multiplier (percent).
        /// Applied to PostVanData.m_MailCapacity (payload per van).
        /// 100% = vanilla; higher values let each van carry more mail.
        /// </summary>
        [SettingsUISection(kActionsTab, PostVanGroup)]
        [SettingsUISlider(
            min = 100,
            max = 500,
            step = 10,
            scalarMultiplier = 1,
            unit = Unit.kPercentage)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ChangeCapacity), true)]
        public int PostVanMailLoadPercentage
        {
            get;
            set;
        }

        /// <summary>
        /// Post van fleet size multiplier (percent).
        /// Applied to PostFacilityData.m_PostVanCapacity (vans per facility).
        /// </summary>
        [SettingsUISection(kActionsTab, PostVanGroup)]
        [SettingsUISlider(
            min = 50,
            max = 300,
            step = 10,
            scalarMultiplier = 1,
            unit = Unit.kPercentage)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ChangeCapacity), true)]
        public int PostVanFleetSizePercentage
        {
            get;
            set;
        }

        /// <summary>
        /// Post truck fleet size multiplier (percent).
        /// Applied to PostFacilityData.m_PostTruckCapacity (trucks per facility).
        /// </summary>
        [SettingsUISection(kActionsTab, PostVanGroup)]
        [SettingsUISlider(
            min = 50,
            max = 300,
            step = 10,
            scalarMultiplier = 1,
            unit = Unit.kPercentage)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ChangeCapacity), true)]
        public int TruckCapacityPercentage
        {
            get;
            set;
        }

        // --------------------------------------------------------------------
        // ACTIONS TAB: POST SORTING FACILITY OPTIONS (last)
        // --------------------------------------------------------------------

        /// <summary>
        /// Sorting speed multiplier for sorting facilities (percent).
        /// </summary>
        [SettingsUISection(kActionsTab, PostSortingFacilityGroup)]
        [SettingsUISlider(
            min = 50,
            max = 500,
            step = 10,
            scalarMultiplier = 1,
            unit = Unit.kPercentage)]
        public int PSF_SortingSpeedPercentage
        {
            get;
            set;
        }

        /// <summary>
        /// Storage capacity multiplier for sorting facilities (percent).
        /// Scales PostFacilityData.m_MailCapacity only for facilities that sort mail.
        /// </summary>
        [SettingsUISection(kActionsTab, PostSortingFacilityGroup)]
        [SettingsUISlider(
            min = 50,
            max = 300,
            step = 10,
            scalarMultiplier = 1,
            unit = Unit.kPercentage)]
        public int PSF_StorageCapacityPercentage
        {
            get;
            set;
        }

        [SettingsUISection(kActionsTab, PostSortingFacilityGroup)]
        public bool PSF_GetUnsortedMail
        {
            get;
            set;
        }

        [SettingsUISection(kActionsTab, PostSortingFacilityGroup)]
        [SettingsUISlider(
            min = 0,
            max = 100,
            step = 1,
            scalarMultiplier = 1,
            unit = Unit.kPercentage)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(PSF_GetUnsortedMail), true)]
        public int PSF_GettingThresholdPercentage
        {
            get;
            set;
        }

        [SettingsUISection(kActionsTab, PostSortingFacilityGroup)]
        [SettingsUISlider(
            min = 0,
            max = 100,
            step = 1,
            scalarMultiplier = 1,
            unit = Unit.kPercentage)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(PSF_GetUnsortedMail), true)]
        public int PSF_GettingPercentage
        {
            get;
            set;
        }

        // --------------------------------------------------------------------
        // STATUS TAB
        // --------------------------------------------------------------------

        [SettingsUISection(kStatusTab, StatusSummaryGroup)]
        public string StatusFacilitySummary =>
            PostMasterSystem.GetStatusSummary();

        [SettingsUISection(kStatusTab, StatusSummaryGroup)]
        public string StatusCityMailSummary =>
            PostMasterSystem.GetStatusCityMail();

        [SettingsUISection(kStatusTab, StatusActivityGroup)]
        public string StatusLastActivity =>
            PostMasterSystem.GetStatusActivity();

        // --------------------------------------------------------------------
        // ABOUT TAB: INFO
        // --------------------------------------------------------------------

        [SettingsUISection(kAboutTab, kAboutInfoGroup)]
        public string ModNameDisplay => $"{Mod.ModName} {Mod.ModTag}";

        [SettingsUISection(kAboutTab, kAboutInfoGroup)]
        public string ModVersionDisplay => Mod.ModVersion;

        // --------------------------------------------------------------------
        // ABOUT TAB: LINKS
        // --------------------------------------------------------------------

        [SettingsUIButtonGroup(kAboutLinksGroup)]
        [SettingsUIButton]
        [SettingsUISection(kAboutTab, kAboutLinksGroup)]
        public bool OpenParadox
        {
            set
            {
                if (!value)
                {
                    return;
                }

                TryOpenUrl(kUrlParadox);
            }
        }

        [SettingsUIButtonGroup(kAboutLinksGroup)]
        [SettingsUIButton]
        [SettingsUISection(kAboutTab, kAboutLinksGroup)]
        public bool OpenDiscord
        {
            set
            {
                if (!value)
                {
                    return;
                }

                TryOpenUrl(kUrlDiscord);
            }
        }

        // --------------------------------------------------------------------
        // DEFAULTS
        // --------------------------------------------------------------------

        /// <summary>
        /// Sets recommended default values for mod behavior.
        /// </summary>
        public override void SetDefaults()
        {
            // Post offices: auto-get local mail when low (magic top-up).
            PO_GetLocalMail = true;
            PO_GettingThresholdPercentage = 2;
            PO_GettingPercentage = 15;

            // Global overflow fix for PO + sorting (magic cleanup).
            // Default thresholds: 90% so nothing happens until buildings are nearly full.
            FixMailOverflow = true;
            PO_OverflowPercentage = 90;
            PSF_OverflowPercentage = 90;

            // Sorting facilities: auto-get unsorted mail when low (magic top-up).
            PSF_GetUnsortedMail = true;
            PSF_GettingThresholdPercentage = 2;
            PSF_GettingPercentage = 15;

            PSF_SortingSpeedPercentage = 100;
            PSF_StorageCapacityPercentage = 100;

            // Capacities enabled by default.
            ChangeCapacity = true;
            MoreDeliveries = true; // recommended: allow extra deliveries when needed (future behavior)
            PostVanMailLoadPercentage = 100;
            PostVanFleetSizePercentage = 100;
            TruckCapacityPercentage = 100;
        }

        /// <summary>
        /// Restores a configuration similar to vanilla behavior.
        /// </summary>
        public void SetToVanilla()
        {
            // Vanilla-like behavior: no auto gets, no overflow cleanup, vanilla capacities.
            PO_GetLocalMail = false;
            PO_GettingThresholdPercentage = 2;
            PO_GettingPercentage = 15;

            FixMailOverflow = false;
            // Thresholds kept high so that if player later turns this on under "vanilla" preset,
            // cleanup only kicks in when buildings are nearly full.
            PO_OverflowPercentage = 90;
            PSF_OverflowPercentage = 90;

            PSF_GetUnsortedMail = false;
            PSF_GettingThresholdPercentage = 2;
            PSF_GettingPercentage = 15;

            PSF_SortingSpeedPercentage = 100;
            PSF_StorageCapacityPercentage = 100;

            ChangeCapacity = false;
            MoreDeliveries = false;
            PostVanMailLoadPercentage = 100;
            PostVanFleetSizePercentage = 100;
            TruckCapacityPercentage = 100;
        }

        // --------------------------------------------------------------------
        // HELPERS
        // --------------------------------------------------------------------

        /// <summary>
        /// Opens a URL via Unity’s Application.OpenURL, ignoring failures.
        /// </summary>
        private static void TryOpenUrl(string url)
        {
            try
            {
                Application.OpenURL(url);
            }
            catch (Exception)
            {
                // Silent failure to avoid disrupting the Options UI.
            }
        }
    }
}
