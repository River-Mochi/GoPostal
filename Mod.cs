// <copyright file="Mod.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// Mod.cs
// Entry point for MagicMail [MM].

namespace MagicMail
{
    using System;
    using System.Reflection;

    using Colossal.IO.AssetDatabase;
    using Colossal.Localization;
    using Colossal.Logging;
    using CS2Shared.RiverMochi;

    using Game;
    using Game.Modding;
    using Game.SceneFlow;

    /// <summary>
    /// Registers Magic Mail settings, localization, and systems.
    /// </summary>
    public sealed class Mod : IMod
    {
        public const string ModId = "MagicMail";
        public const string ModName = "Magic Mail";
        public const string ModTag = "[MM]";

#if DEBUG
        private const string kBuildType = "DEBUG";
#else
        private const string kBuildType = "RELEASE";
#endif

        public static readonly string ModVersion =
            Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "1.0.0";

        internal static readonly ILog s_Log =
            LogManager.GetLogger(ModId).SetShowsErrorsInUI(
#if DEBUG
                true
#else
                false
#endif
            );

        public static Setting? Settings
        {
            get;
            private set;
        }

        private static bool s_BannerLogged;

        public void OnLoad(UpdateSystem updateSystem)
        {
            // Direct-file logging keeps MagicMail messages out of Player.log.
            LogUtils.Configure(ModId, s_Log);

            if (!s_BannerLogged)
            {
                s_BannerLogged = true;
                LogUtils.Info($"{ModName} {ModTag} v{ModVersion} [{kBuildType}] OnLoad");
            }

            GameManager? gameManager = GameManager.instance;
            if (gameManager == null)
            {
                LogUtils.Error("GameManager.instance is null in Mod.OnLoad.");
                return;
            }

            Setting setting = new Setting(this);
            Settings = setting;

            LocalizationManager? localizationManager = gameManager.localizationManager;
            if (localizationManager == null)
            {
                LogUtils.Warn("LocalizationManager is null; locale sources were not registered.");
            }
            else
            {
                try
                {
                    localizationManager.AddSource("en-US", new LocaleEN(setting));
                    localizationManager.AddSource("de-DE", new LocaleDE(setting));
                    localizationManager.AddSource("fr-FR", new LocaleFR(setting));
                    localizationManager.AddSource("es-ES", new LocaleES(setting));
                    localizationManager.AddSource("it-IT", new LocaleIT(setting));
                    localizationManager.AddSource("ja-JP", new LocaleJA(setting));
                    localizationManager.AddSource("ko-KR", new LocaleKO(setting));
                    localizationManager.AddSource("pl-PL", new LocalePL(setting));
                    localizationManager.AddSource("pt-BR", new LocalePT_BR(setting));
                    localizationManager.AddSource("pt-PT", new LocalePT_PT(setting));
                    localizationManager.AddSource("zh-HANS", new LocaleZH_CN(setting));
                    localizationManager.AddSource("zh-HANT", new LocaleZH_HANT(setting));
                    localizationManager.AddSource("th-TH", new LocaleTH(setting));
                    localizationManager.AddSource("vi-VN", new LocaleVI(setting));
                }
                catch (Exception ex)
                {
                    LogUtils.Error("Localization registration failed.", ex);
                }
            }

            AssetDatabase.global.LoadSettings(
                ModId,
                setting,
                new Setting(this));

            setting.RegisterInOptionsUI();

            updateSystem.UpdateBefore<MagicMailSystem>(
                SystemUpdatePhase.GameSimulation);

            updateSystem.UpdateBefore<MailCapacitySystem>(
                SystemUpdatePhase.GameSimulation);

#if DEBUG
            // Fast read-only sampler catches sorting truck visits between main snapshots.
            updateSystem.UpdateAfter<MailSortingTrafficDiagnosticSystem>(
                SystemUpdatePhase.GameSimulation);

            // Full read-only snapshots every 90 in-game minutes.
            updateSystem.UpdateAfter<MailDiagnosticSystem>(
                SystemUpdatePhase.GameSimulation);
#endif
        }

        public void OnDispose()
        {
            Settings?.UnregisterInOptionsUI();
            Settings = null;
        }
    }
}
