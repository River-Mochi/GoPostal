// Mod.cs
// Entry point for PostMaster [PM].

namespace PostMaster
{
    using System.Reflection;             // Assembly version
    using Colossal;                      // IDictionarySource
    using Colossal.IO.AssetDatabase;     // AssetDatabase.LoadSettings
    using Colossal.Localization;         // LocalizationManager
    using Colossal.Logging;              // ILog, LogManager
    using Game;                          // UpdateSystem, SystemUpdatePhase
    using Game.Modding;                  // IMod
    using Game.SceneFlow;                // GameManager

    /// <summary>
    /// Mod entry point for PostMaster [PM].
    /// Registers settings, locales, and the ECS system.
    /// </summary>
    public sealed class Mod : IMod
    {
        // ---- PUBLIC CONSTANTS / METADATA ----

        public const string ModId = "PostMaster";
        public const string ModName = "PostMaster";
        public const string ModTag = "[PM]";

        /// <summary>
        /// Read Version from .csproj (3-part).
        /// </summary>
        public static readonly string ModVersion =
            Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "1.0.0";

        /// <summary>
        /// Logger for this mod.
        /// </summary>
        public static readonly ILog s_Log =
            LogManager.GetLogger(ModId).SetShowsErrorsInUI(false);

        /// <summary>
        /// Global settings instance.
        /// </summary>
        public static Setting? Settings
        {
            get; private set;
        }

        // ---- PRIVATE STATE ----

        private static bool s_BannerLogged;

        // --------------------------------------------------------------------
        // IMod
        // --------------------------------------------------------------------

        /// <summary>
        /// Called by the game during world initialization.
        /// </summary>
        /// <param name="updateSystem">Update system scheduler.</param>
        public void OnLoad(UpdateSystem updateSystem)
        {
            // One-time banner.
            if (!s_BannerLogged)
            {
                s_BannerLogged = true;
                s_Log.Info($"{ModName} {ModTag} v{ModVersion} OnLoad");
            }

            GameManager? gameManager = GameManager.instance;
            if (gameManager == null)
            {
                s_Log.Error("GameManager.instance is null in Mod.OnLoad.");
                return;
            }

            // Settings must exist before locales so labels resolve correctly.
            Setting setting = new Setting(this);
            Settings = setting;

            // Register locales.
            AddLocaleSource("en-US", new LocaleEN(setting));

            // Ready for future locales (uncomment once you add the files):
            // AddLocaleSource("fr-FR",  new LocaleFR(setting));
            // AddLocaleSource("de-DE",  new LocaleDE(setting));
            // AddLocaleSource("es-ES",  new LocaleES(setting));
            // AddLocaleSource("it-IT",  new LocaleIT(setting));
            // AddLocaleSource("ja-JP",  new LocaleJA(setting));
            // AddLocaleSource("ko-KR",  new LocaleKO(setting));
            // AddLocaleSource("pt-BR",  new LocalePT_BR(setting));
            // AddLocaleSource("zh-HANS", new LocaleZH_CN(setting));
            // AddLocaleSource("zh-HANT", new LocaleZH_HANT(setting));

            // Load persisted settings or create defaults on first run.
            AssetDatabase.global.LoadSettings(ModId, setting, new Setting(this));

            // Register in Options UI once we have locales.
            setting.RegisterInOptionsUI();

            // Schedule the system before the vanilla postal system in the GameSimulation phase.
            updateSystem.UpdateBefore<PostMasterSystem>(SystemUpdatePhase.GameSimulation);
        }

        /// <summary>
        /// Called by the game when the mod is unloaded.
        /// </summary>
        public void OnDispose()
        {
            s_Log.Info("OnDispose");

            if (Settings != null)
            {
                Settings.UnregisterInOptionsUI();
                Settings = null;
            }
        }

        // --------------------------------------------------------------------
        // Localization helper
        // --------------------------------------------------------------------

        /// <summary>
        /// Wrapper around LocalizationManager.AddSource that catches exceptions
        /// so fragile localization mods can't crash us.
        /// </summary>
        private static void AddLocaleSource(string localeId, IDictionarySource source)
        {
            if (string.IsNullOrEmpty(localeId))
            {
                return;
            }

            LocalizationManager? lm = GameManager.instance?.localizationManager;
            if (lm == null)
            {
                s_Log.Warn($"AddLocaleSource: No LocalizationManager; cannot add source for '{localeId}'.");
                return;
            }

            try
            {
                lm.AddSource(localeId, source);
            }
            catch (System.Exception ex)
            {
                s_Log.Warn($"AddLocaleSource: AddSource for '{localeId}' failed: {ex.GetType().Name}: {ex.Message}");
            }
        }
    }
}
