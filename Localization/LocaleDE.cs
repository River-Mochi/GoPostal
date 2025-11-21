// Localization/LocaleDE.cs
// German locale de-DE

namespace MagicPostMaster
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// German localization source for Magic PostMaster [MPM].</summary>
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
                { m_Setting.GetSettingsLocaleID(), "Magic PostMaster [MPM]" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "Aktionen" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab),  "Status" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab),   "Info" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup),          "Postamt" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup),             "Postwagen & LKW" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "Sortierzentrum" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup),               "Zurücksetzen" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "Stadtüberblick" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "Letztes Update" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup),  "Info" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "Links" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "Niedrige lokale Post beheben" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "Wenn aktiviert, erscheint bei zu niedrigem Bestand \"magisch\" zusätzliche lokale Post im Postamt.\n " +
                    "Es werden keine zusätzlichen Fahrzeuge erzeugt – die Post liegt direkt im Gebäude zur Verfügung." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "Schwellenwert für lokale Post" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "Wenn lokale Post unter diesen von dir gewählten Prozentsatz fällt, holt das Postamt automatisch mehr lokale Post.\n" +
                    "Es ist der Prozentsatz der maximalen Lagerkapazität." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "Menge der lokalen Postabrufe" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "Prozentsatz, der beim magischen Auffüllen lokaler Post hinzugefügt wird.\n" +
                    "20 % = +2.000, wenn das Vanilla-Limit = 10.000 ist." },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)),
                    "Postüberlauf beheben" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "Wenn aktiviert, führen Postämter und/oder Sortierzentren eine kleine \"magische\" Bereinigung durch,\n " +
                    "sobald die eingestellten Überlaufgrenzen erreicht werden: überschüssige Post wird als zugestellt behandelt und entfernt.\n " +
                    "Dies verhindert, dass Gebäude dauerhaft als \"voll\" stecken bleiben.\n " +
                    "Deaktiviere dies, um das reine Vanilla-Verhalten beizubehalten." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "Überlaufgrenze Postamt" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "Wenn die Gesamtpost diesen Prozentsatz der Lagerkapazität erreicht, wird die Überlaufbehandlung ausgelöst." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "Überlaufgrenze Sortierzentrum" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "Wenn die Gesamtpost in Sortierzentren diesen Prozentsatz der Gesamtspeicherkapazität erreicht,\n " +
                    "wird die \"magische\" Überlaufbehandlung ausgelöst." },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "Kapazitäten ändern" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "Aktiviere dies, um die Kapazität von Postwagen und LKW zu ändern. Wenn deaktiviert, sind alle Regler unten ausgeblendet\n " +
                    "und Vanilla-Werte werden verwendet, auch wenn die Regler andere Werte anzeigen." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "Ladung der Postwagen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "Steuert, wie viel Post ein einzelner Postwagen transportieren kann. 100 % = Vanilla-Ladung." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "Größe der Postwagenflotte" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "Steuert, wie viele Postwagen jedes Postgebäude besitzen und einsetzen kann.\n" +
                    "<100 % = Vanilla-Flottengröße.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "Größe der Post-LKW-Flotte" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "Steuert, wie viele Post-LKW jedes Sortierzentrum (und andere Gebäude mit Post-LKW)\n " +
                    "besitzen und einsetzen kann.\n " +
                    "<100 % = Vanilla-Flottengröße.>" },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "Sortiergeschwindigkeit" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "Multiplikator für die Sortiergeschwindigkeit in postalischen **Sortierzentren**. Wirkt auf die Basis-Sortierleistung der Anlage.\n " +
                    "<100 % = Vanilla>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "Sortierlagerkapazität" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "Steuert die **Postlagerung** im Sortierzentrum.\n " +
                    "<100 % = Vanilla>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "Niedrige unsortierte Post beheben" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "Wenn aktiviert, erscheint \"magisch\" zusätzliche unsortierte Post im Sortierzentrum, wenn der Vorrat zu niedrig wird.\n " +
                    "So bleibt das Sortieren aktiv, ohne auf weitere Lieferungen warten zu müssen.\n" +
                    "Vorübergehender Fix für den aktuellen Fehler, bei dem Sortierzentren zu wenig Post erhalten, wenn ein Frachthafen vorhanden ist." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "Schwellenwert für unsortierte Post" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "Wenn unsortierte Post unter diesen Prozentsatz der Kapazität fällt, zieht die Anlage magisch die eingestellte Abrufmenge an unsortierter Post nach." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "Menge der unsortierten Postabrufe" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "Zusätzliche Post, die beim magischen Auffüllen von unsortierter Post hinzugefügt wird.\n" +
                    "Prozentsatz der maximalen Speicherkapazität." },

                // ---- Reset ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "Auf Spiel-Standard zurücksetzen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)),
                    "Setzt alle Posteinstellungen auf das ursprüngliche Verhalten des Spiels (Vanilla) zurück." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "Auf Empfehlung zurücksetzen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "Schnellstart: Empfohlene Posteinstellungen anwenden." },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), "" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)),
                    "Zusammenfassung der Postämter, Postwagen, Sortierzentren und Post-LKW, die im letzten Spiel-Update (~alle 45 Spielminuten) verarbeitet wurden." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "Stadtpost" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "Zeigt den jüngsten Postfluss in der ganzen Stadt.\n\n" +
                     "Erzeugt = wie viel Post die Bürger produziert haben.\n" +
                     "Verarbeitet   = wie viel Post das Netz tatsächlich bearbeitet hat.\n\n" +
                     "- Wenn Verarbeitet dauerhaft höher ist als Erzeugt, hat das Postnetz genug Kapazität " +
                     "und man kann Budget oder Fahrzeuganzahl bei Bedarf senken.\n" +
                     "- Wenn Erzeugt über längere Zeit über Verarbeitet bleibt, erzeugt die Stadt mehr Post als\n " +
                     "das Netz bewältigen kann – mehr Anlagen, Fahrzeuge oder feinere Einstellungen sind nötig." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "Aktivität" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)),
                    "Zählt die Postauffüllungen und Überlaufbereinigungen, die im letzten Update durchgeführt wurden." },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)),
                    "Anzeigename dieser Mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)),
                    "Aktuelle Mod-Version." },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)),
                    "Öffnet die **Paradox**-Webseite für **Magic PostMaster** und andere Mods." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)),
                    "Öffnet den **Discord**-Feedback-Chat im Browser." },
            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
