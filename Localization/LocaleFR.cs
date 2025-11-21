// Localization/LocaleFR.cs
// French locale fr-FR

namespace MagicPostMaster
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// French localization source for Magic PostMaster [MPM].</summary>
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
                { m_Setting.GetSettingsLocaleID(), "Magic PostMaster [MPM]" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "Actions" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab),  "Statut" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab),   "À propos" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup),          "Bureau de poste" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup),             "Fourgonnettes & camions" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "Centre de tri" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup),               "Réinitialiser" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "Vue d’ensemble de la ville" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "Dernière mise à jour" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup),  "Infos" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "Liens" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "Corriger le faible courrier local" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "Quand cette option est activée, du courrier local supplémentaire apparaît \"magiquement\" si le stock est trop bas.\n " +
                    "Aucune fourgonnette supplémentaire n’est envoyée – le courrier apparaît directement dans le bâtiment." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "Seuil de courrier local" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "Si le courrier local passe en dessous de ce pourcentage choisi, le bureau de poste récupère automatiquement plus de courrier local.\n" +
                    "Il s’agit d’un pourcentage de la capacité de stockage maximale." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "Montant de courrier local à ajouter" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "Pourcentage ajouté lors du remplissage magique du courrier local.\n" +
                    "20 % = +2 000 si la capacité vanilla = 10 000." },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)),
                    "Corriger le débordement de courrier" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "Quand cette option est activée, les bureaux de poste et/ou les centres de tri effectuent un petit nettoyage \"magique\" lorsque\n " +
                    "les seuils de débordement sont atteints : le courrier excédentaire est considéré comme distribué puis supprimé.\n " +
                    "Cela évite que les bâtiments restent bloqués en état \"plein\" pour toujours.\n " +
                    "Désactive cette option pour conserver le comportement vanilla." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "Seuil de débordement du bureau de poste" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "Quand le courrier total atteint ce pourcentage de la capacité de stockage, la gestion de débordement se déclenche." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "Seuil de débordement du centre de tri" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "Quand le courrier total dans les centres de tri atteint ce pourcentage de la capacité totale,\n " +
                    "le traitement de débordement \"magique\" se déclenche." },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "Modifier les capacités" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "Active cette option pour modifier les capacités des fourgons et des camions. Lorsque cette option est désactivée, tous les curseurs ci-dessous sont cachés\n " +
                    "et les valeurs vanilla du jeu sont utilisées, même si les curseurs affichent d’autres nombres." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "Charge de courrier des fourgons" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "Contrôle la quantité de courrier que chaque fourgon postal peut transporter. 100 % = charge vanilla." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "Taille de la flotte de fourgons" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "Contrôle combien de fourgons chaque bâtiment postal peut posséder et utiliser.\n" +
                    "<100 % = taille de flotte vanilla.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "Taille de la flotte de camions postaux" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "Contrôle combien de camions postaux chaque centre de tri (et tout bâtiment avec des camions de poste)\n " +
                    "peut posséder et utiliser.\n " +
                    "<100 % = taille de flotte vanilla.>" },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "Vitesse de tri" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "Multiplicateur de vitesse de tri pour les **centres de tri** postaux. S’applique au taux de tri de base du bâtiment.\n " +
                    "<100 % = vanilla>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "Capacité de stockage du tri" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "Contrôle la **capacité de stockage** dans un centre de tri.\n " +
                    "<100 % = vanilla>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "Corriger le faible courrier non trié" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "Quand cette option est activée, du courrier non trié supplémentaire apparaît \"magiquement\" dans les centres de tri si le stock devient trop faible.\n " +
                    "Cela maintient l’activité de tri sans attendre de nouvelles livraisons.\n" +
                    "Correction temporaire pour le bug actuel où les centres de tri reçoivent trop peu de courrier lorsqu’un port de fret est présent." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "Seuil de courrier non trié" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "Si le courrier non trié passe en dessous de ce pourcentage de la capacité, le bâtiment fait apparaître magiquement la quantité configurée de courrier non trié." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "Montant de courrier non trié à ajouter" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "Courrier supplémentaire ajouté lors du remplissage magique du courrier non trié.\n" +
                    "Pourcentage de la capacité de stockage maximale." },

                // ---- Reset ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "Réinitialiser aux valeurs du jeu" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)),
                    "Restaure tous les réglages postaux au comportement original du jeu (vanilla)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "Réinitialiser aux valeurs recommandées" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "Démarrage rapide : applique les réglages postaux recommandés." },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), "" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)),
                    "Résumé des bureaux de poste, fourgons, centres de tri et camions postaux traités lors de la dernière mise à jour du jeu (~toutes les 45 minutes de jeu)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "Courrier de la ville" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "Affiche le flux de courrier récent à l’échelle de la ville.\n\n" +
                     "Courrier généré = quantité de courrier produite par les citoyens.\n" +
                     "Courrier traité   = quantité de courrier réellement traitée par le réseau.\n\n" +
                     "- Si le courrier traité reste constamment supérieur au courrier généré, ton réseau postal a assez de capacité " +
                     "et tu peux réduire le budget ou les véhicules si tu le souhaites.\n" +
                     "- Si le courrier généré reste au-dessus du courrier traité pendant longtemps, la ville génère plus de courrier que\n " +
                     "le réseau ne peut en traiter – ajoute des bâtiments, des véhicules ou ajuste les réglages." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "Activité" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)),
                    "Compte les remplissages de courrier et les nettoyages de débordement effectués lors de la dernière mise à jour." },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)),
                    "Nom d’affichage de ce mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)),
                    "Version actuelle du mod." },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)),
                    "Ouvre la page **Paradox** pour **Magic PostMaster** et d’autres mods." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)),
                    "Ouvre le salon de discussion **Discord** pour les retours dans un navigateur." },
            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
