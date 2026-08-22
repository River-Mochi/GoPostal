// <copyright file="LocaleDE.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// LocaleDE.cs
// German locale de-DE

namespace MagicMail
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// German localization source for Magic Mail [MM].</summary>
    public sealed class LocaleDE : IDictionarySource
    {
        private readonly Setting m_Setting;

        /// <summary>
        /// Constructs the German locale generator.</summary>
        /// <param name="setting">Settings object used for locale IDs.</param>
        public LocaleDE(Setting setting)
        {
            m_Setting = setting;
        }

        /// <summary>
        /// Generates all German localization entries for this mod.</summary>
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod title
                { m_Setting.GetSettingsLocaleID(), "Magic Mail + Postal Dispatch" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "Aktionen" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab), "Status" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab), "Info" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup), "Hilfe bei der Postzustellung" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup), "Postwagen & LKW" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "Sortieranlage" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup), "Zurücksetzen" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "Stadt-Scan" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "Letztes Update" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "Links" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "Zu wenig lokale Post beheben" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "Wenn aktiviert, erscheint etwas zusätzliche Post, sobald der Bestand zu niedrig wird.\n" +
                    "Es werden keine extra Postwagen gespawnt – ein bisschen Magie... aber echt :)"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "Schwelle für lokale Post" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "Fällt die lokale Post unter diesen von dir gewählten Prozentsatz,\n" +
                    "holt das Postamt automatisch mehr lokale Post.\n" +
                    "Der Wert bezieht sich auf die maximale Lagerkapazität des Gebäudes.\n" +
                    "Beispiel: <Max. Lager = 100.000> und <Schwelle = 5%>,\n" +
                    "bei lokaler Post < <5.000> wird Nachschub geholt."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "Menge für lokale Post" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "Prozentsatz, der beim Nachfüllen lokaler Post hinzugefügt wird (magische Auffüllung).\n" +
                    "Wenn Vanilla-Maximum = <100.000> und hier <10%> eingestellt sind,\n" +
                    "werden bei Bedarf <10.000> hinzugefügt."
                },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)), "Post-Überlauf beheben" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "Wenn zu viel Post lagert, machen die Einrichtungen eine kleine magische Aufräumaktion.\n" +
                    "Überschüssige Post gilt als zugestellt und wird entfernt.\n" +
                    "So bleiben Einrichtungen nicht dauerhaft voll hängen.\n" +
                    "Ausschalten, wenn du reines Vanilla-Verhalten möchtest."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "Überlaufgrenze des Postamts" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "Erreicht die gesamte Post im Postamt diesen Prozentsatz, entfernt die Mod\n" +
                    "so viel gespeicherte Post, bis dieser Wert wieder erreicht ist."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "Überlaufgrenze der Sortieranlage" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "Erreicht die gesamte Post in einer Sortieranlage diesen Prozentsatz, entfernt die Mod\n" +
                    "so viel gespeicherte Post, bis dieser Wert wieder erreicht ist."
                },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "Kapazitäten ändern" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "Aktivieren, um Kapazitäten von Postwagen und LKW anzupassen. Wenn aus,\n" +
                    "werden alle Kapazitätsregler darunter ausgeblendet und\n" +
                    "die Vanilla-Spielwerte genutzt, egal welche Werte noch in den Reglern stehen."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "Ladung pro Postwagen" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "Steuert, wie viel Post jeder Postwagen transportieren kann.\n" +
                    "<100% = Vanilla-Ladung.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "Postwagen-Flottengröße" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "Steuert, wie viele Postwagen jedes Postgebäude besitzen und losschicken kann.\n" +
                    "<100% = Vanilla-Flottengröße.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "Post-LKW-Flottengröße" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "Steuert, wie viele Post-LKW jede Sortieranlage (und jede Einrichtung mit Post-LKW)\n" +
                    "besitzen und losschicken kann.\n" +
                    "<100% = Vanilla-Flottengröße.>"
                },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "Sortiergeschwindigkeit" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "Multiplikator für **Sortieranlagen**. Gilt für die normale Sortierrate der Einrichtung.\n" +
                    "<100% = Vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "Lagerkapazität der Sortieranlage" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "Steuert den **Post-Lagerplatz**.\n" +
                    "<100% = Vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "Zu wenig unsortierte Post beheben" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "Wenn aktiviert, erscheint etwas unsortierte Post magisch, sobald der Vorrat zu niedrig wird.\n" +
                    "So bleiben Sortieranlagen aktiv.\n" +
                    "Temporärer Workaround für einen aktuellen Bug, bei dem Sortieranlagen mit einem Frachthafen in der Stadt nicht genug Post bekommen."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "Schwelle für unsortierte Post" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "Fällt unsortierte Post unter diesen niedrigen Prozentsatz der gesamten Lagerkapazität,\n" +
                    "wird etwas zusätzliche unsortierte Post geholt."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "Menge für unsortierte Post" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "Zusätzliche Menge beim Holen unsortierter Post (magische Auffüllung).\n" +
                    "Die Menge ist ein Prozentsatz der maximalen Lagerkapazität.\n" +
                    "Wenn Vanilla <Max = 250.000> und hier <10%> eingestellt sind, werden <25.000> hinzugefügt."
                },

                // ---- RESET BUTTONS ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "Spiel-Standard" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)), "Setzt alle Einstellungen auf das ursprüngliche Spielverhalten (Vanilla) zurück." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "Empfohlen" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "**Schnellstart** – wendet alle empfohlenen Post-Einstellungen an.\n" +
                    "Einfacher Modus: 1 Klick und fertig!"
                },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), string.Empty },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)), "Zusammenfassung der Postämter, Postwagen, Sortieranlagen und Post-LKW aus dem letzten Hintergrund-Scan." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "Monatliche Post" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "Zeigt den aktuellen Postfluss in der ganzen Stadt.\n" +
                    "\n" +
                    "**Angesammelt** = wie viel Post die Bürger erzeugt haben.\n" +
                    "**Verarbeitet** = wie viel Post das Netzwerk tatsächlich geschafft hat.\n" +
                    "\n" +
                    "- Ist Verarbeitet oft höher als Angesammelt, hat dein Postnetz genug Kapazität.\n" +
                    "- Bleibt Angesammelt längere Zeit über Verarbeitet,\n" +
                    "erzeugt die Stadt mehr Post, als das Netz bewältigen kann.\n" +
                    "Baue mehr Einrichtungen oder Postwagen oder passe deine Einstellungen an."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "Aktivität" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)), "Anzahl der Post-Nachfüllungen und Überlauf-Bereinigungen beim letzten Update." },

                // ---- Status text templates (for MagicMailSystem) ----
                { "MM_STATUS_NO_FACILITIES", "Noch keine Posteinrichtungen verarbeitet. Öffne eine Stadt und lass die Simulation kurz laufen." },

                { "MM_STATUS_NO_ACTIVITY", "Noch keine Aktivität aufgezeichnet." },

                { "MM_STATUS_SUMMARY", "{0} Postämter | {1} Postwagen | {2} Sortieranlagen | {3} Post-LKW" },

                { "MM_STATUS_ACTIVITY", "{0} lokale Nachfüllungen | {1} unsortierte Nachfüllungen | {2} Überlauf-Bereinigungen" },

                { "MM_STATUS_CITY_MAIL_NOT_READY", "Stadtpost-Statistiken sind noch nicht verfügbar. Öffne eine Stadt und lass die Simulation kurz laufen." },

                { "MM_STATUS_CITY_MAIL", "{0} angesammelt | {1} verarbeitet" },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "Anzeigename dieser Mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "Aktuelle Mod-Version." },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)), "Öffnet die **Paradox**-Seite für **Magic Mail** und andere Mods." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Öffnet den **Discord**-Feedback-Chat im Browser." },

            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
