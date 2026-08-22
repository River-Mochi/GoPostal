// <copyright file="LocaleFR.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// LocaleFR.cs
// French locale fr-FR

namespace MagicMail
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// French localization source for Magic Mail [MM].</summary>
    public sealed class LocaleFR : IDictionarySource
    {
        private readonly Setting m_Setting;

        /// <summary>
        /// Constructs the French locale generator.</summary>
        /// <param name="setting">Settings object used for locale IDs.</param>
        public LocaleFR(Setting setting)
        {
            m_Setting = setting;
        }

        /// <summary>
        /// Generates all French localization entries for this mod.</summary>
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod title
                { m_Setting.GetSettingsLocaleID(), "Magic Mail + Postal Dispatch" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "Actions" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab), "Statut" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab), "À propos" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup), "Aide à la distribution postale" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup), "Fourgons et camions" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "Centre de tri" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup), "Réinitialiser" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "Scan de la ville" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "Dernière mise à jour" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup), "Infos" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "Liens" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "Corriger le manque de courrier local" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "Si activé, un peu de courrier apparaît quand le stock devient trop bas.\n" +
                    "Aucun fourgon supplémentaire n'est créé : c'est un peu magique... mais réel :)"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "Seuil de courrier local" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "Si le courrier local passe sous le pourcentage choisi,\n" +
                    "le bureau de poste récupère davantage de courrier local.\n" +
                    "C'est un pourcentage de la capacité maximale du bâtiment.\n" +
                    "Ex. : <stockage max = 100 000> et <seuil = 5%>,\n" +
                    "si le courrier local < <5 000>, du courrier supplémentaire est récupéré."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "Quantité de courrier local" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "Pourcentage ajouté lors de la récupération de courrier local (appoint magique).\n" +
                    "Si le maximum vanilla = <100 000> et que cette valeur est <10%>,\n" +
                    "alors <10 000> sont ajoutés si nécessaire."
                },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)), "Corriger le débordement de courrier" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "Quand il y a trop de courrier, les installations font un petit nettoyage magique.\n" +
                    "Le courrier stocké en trop est considéré comme livré puis supprimé.\n" +
                    "Cela évite qu'une installation reste bloquée pleine indéfiniment.\n" +
                    "Désactivez cette option pour garder le comportement vanilla pur."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "Seuil de débordement du bureau de poste" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "Quand le courrier total d'un bureau de poste atteint ce pourcentage, le mod\n" +
                    "supprime assez de courrier stocké pour revenir à ce niveau."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "Seuil de débordement du centre de tri" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "Quand le courrier total d'un centre de tri atteint ce pourcentage, le mod\n" +
                    "supprime assez de courrier stocké pour revenir à ce niveau."
                },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "Modifier les capacités" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "Activez ceci pour modifier les capacités des fourgons et camions. Si désactivé,\n" +
                    "tous les curseurs de capacité ci-dessous sont masqués et\n" +
                    "les valeurs vanilla du jeu sont utilisées même si les curseurs étaient réglés autrement."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "Charge du fourgon postal" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "Contrôle la quantité de courrier transportée par chaque fourgon postal.\n" +
                    "<100% = charge vanilla.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "Taille de la flotte de fourgons" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "Contrôle combien de fourgons chaque bâtiment postal peut posséder et envoyer.\n" +
                    "<100% = flotte vanilla.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "Taille de la flotte de camions" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "Contrôle combien de camions postaux chaque centre de tri (et toute installation avec des camions postaux)\n" +
                    "peut posséder et envoyer.\n" +
                    "<100% = flotte vanilla.>"
                },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "Vitesse de tri" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "Multiplicateur pour les centres de **tri**. S'applique au taux de tri de base.\n" +
                    "<100% = vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "Capacité de stockage du tri" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "Contrôle le **stockage du courrier**.\n" +
                    "<100% = vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "Corriger le manque de courrier non trié" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "Si activé, un peu de courrier non trié apparaît comme par magie quand les réserves deviennent trop basses.\n" +
                    "Cela permet aux centres de tri de rester actifs.\n" +
                    "C'est une solution temporaire à un bug actuel où les centres de tri ne reçoivent pas assez de courrier lorsqu'un port de fret est présent."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "Seuil de courrier non trié" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "Si le courrier non trié passe sous ce faible pourcentage de la capacité totale,\n" +
                    "un peu de courrier non trié supplémentaire est récupéré."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "Quantité de courrier non trié" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "Courrier supplémentaire ajouté lors de la récupération de courrier non trié (appoint magique).\n" +
                    "La quantité est un pourcentage de la capacité maximale.\n" +
                    "Si vanilla <max = 250 000> et que cette valeur est <10%>, alors <25 000> sont ajoutés."
                },

                // ---- RESET BUTTONS ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "Valeurs du jeu" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)), "Restaure tous les réglages au comportement d'origine du jeu (vanilla)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "Recommandé" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "**Démarrage rapide** – applique tous les réglages postaux recommandés.\n" +
                    "Mode facile : un clic et c'est fait !"
                },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), string.Empty },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)), "Résumé des bureaux de poste, fourgons, centres de tri et camions postaux traités lors du dernier scan en arrière-plan." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "Courrier mensuel" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "Affiche le flux de courrier récent dans toute la ville.\n" +
                    "\n" +
                    "**Accumulation** = quantité de courrier générée par les citoyens.\n" +
                    "**Traité** = quantité réellement gérée par le réseau postal.\n" +
                    "\n" +
                    "- Si Traité est souvent supérieur à Accumulation, votre réseau postal a assez de capacité.\n" +
                    "- Si Accumulation reste longtemps au-dessus de Traité,\n" +
                    "la ville génère plus de courrier que le réseau ne peut en gérer.\n" +
                    "Ajoutez des installations ou des fourgons, ou ajustez vos réglages."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "Activité" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)), "Compte les appoints de courrier et nettoyages de débordement effectués lors de la dernière mise à jour." },

                // ---- Status text templates (for MagicMailSystem) ----
                { "MM_STATUS_NO_FACILITIES", "Aucune installation postale traitée pour le moment. Ouvrez une ville et laissez tourner la simulation." },

                { "MM_STATUS_NO_ACTIVITY", "Aucune activité enregistrée pour le moment." },

                { "MM_STATUS_SUMMARY", "{0} bureaux de poste | {1} fourgons postaux | {2} centres de tri | {3} camions postaux" },

                { "MM_STATUS_ACTIVITY", "{0} appoints de courrier local | {1} appoints de courrier non trié | {2} nettoyages de débordement" },

                { "MM_STATUS_CITY_MAIL_NOT_READY", "Les statistiques de courrier de la ville ne sont pas encore disponibles. Ouvrez une ville et laissez tourner la simulation." },

                { "MM_STATUS_CITY_MAIL", "{0} accumulé | {1} traité" },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "Nom affiché de ce mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "Version actuelle du mod." },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)), "Ouvre la page **Paradox** de **Magic Mail** et des autres mods." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Ouvre le chat de retours **Discord** dans le navigateur." },

            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
