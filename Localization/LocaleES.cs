// Localization/LocaleES.cs
// Spanish locale es-ES

namespace MagicPostMaster
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// Spanish localization source for Magic PostMaster [MPM].</summary>
    public sealed class LocaleES : IDictionarySource
    {
        private readonly Setting m_Setting;

        /// <summary>
        /// Constructs the Spanish locale generator.</summary>
        /// <param name="setting">Settings object used for locale IDs.</param>
        public LocaleES(Setting setting)
        {
            m_Setting = setting;
        }

        /// <summary>
        /// Generates all Spanish localization entries for this mod.</summary>
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod title
                { m_Setting.GetSettingsLocaleID(), "Magic PostMaster [MPM]" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "Acciones" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab),  "Estado" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab),   "Acerca de" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup),          "Oficina de correos" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup),             "Furgonetas y camiones" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "Centro de clasificación" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup),               "Restablecer" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "Vista general de la ciudad" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "Última actualización" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup),  "Información" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "Enlaces" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "Corregir poco correo local" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "Cuando está activado, aparece \"mágicamente\" correo local extra si el almacén está demasiado bajo.\n " +
                    "No genera furgonetas adicionales: el correo aparece directamente en el edificio." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "Umbral de correo local" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "Si el correo local baja por debajo de este porcentaje elegido, la oficina de correos obtiene automáticamente más correo local.\n" +
                    "Es un porcentaje de la capacidad máxima de almacenamiento." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "Cantidad de correo local a añadir" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "Porcentaje añadido cuando se rellena mágicamente el correo local.\n" +
                    "20 % = +2.000 si el máximo vanilla = 10.000." },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)),
                    "Corregir desbordamiento de correo" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "Cuando está activado, las oficinas de correos y/o los centros de clasificación realizan una pequeña limpieza \"mágica\" cuando\n " +
                    "se alcanzan los umbrales de exceso de correo: el correo sobrante se considera entregado y se elimina.\n " +
                    "Esto evita que los edificios se queden bloqueados como \"llenos\" para siempre.\n " +
                    "Desactiva esta opción para mantener el comportamiento vanilla." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "Umbral de desbordamiento de oficina" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "Cuando el correo total alcanza este porcentaje de la capacidad de almacenamiento, se activa la gestión de desbordamiento." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "Umbral de desbordamiento de centro de clasificación" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "Cuando el correo total en los centros de clasificación alcanza este porcentaje de almacenamiento total,\n " +
                    "se activa el manejo \"mágico\" del desbordamiento." },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "Cambiar capacidades" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "Activa esta opción para modificar las capacidades de furgonetas y camiones. Si está desactivada, todos los deslizadores de abajo se ocultan\n " +
                    "y se usan los valores vanilla del juego aunque los deslizadores muestren otros números." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "Carga de correo de la furgoneta" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "Controla cuánto correo puede transportar cada furgoneta postal. 100 % = carga vanilla." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "Tamaño de la flota de furgonetas" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "Controla cuántas furgonetas puede poseer y enviar cada edificio postal.\n" +
                    "<100 % = tamaño de flota vanilla.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "Tamaño de la flota de camiones postales" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "Controla cuántos camiones postales puede poseer y enviar cada centro de clasificación (y cualquier edificio con camiones de correo).\n " +
                    "<100 % = tamaño de flota vanilla.>" },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "Velocidad de clasificación" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "Multiplicador de velocidad de clasificación para los **centros de clasificación** postales. Se aplica a la velocidad base de clasificación del edificio.\n " +
                    "<100 % = vanilla>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "Capacidad de almacenamiento del centro de clasificación" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "Controla la **capacidad de almacenamiento de correo** en un centro de clasificación.\n " +
                    "<100 % = vanilla>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "Corregir poco correo sin clasificar" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "Cuando está activado, aparece correo sin clasificar adicional de forma \"mágica\" en los centros de clasificación si el stock es demasiado bajo.\n " +
                    "Esto mantiene la clasificación activa sin esperar a nuevas entregas.\n" +
                    "Arreglo temporal para el error actual en el que los centros de clasificación no reciben suficiente correo cuando hay un puerto de carga." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "Umbral de correo sin clasificar" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "Si el correo sin clasificar baja por debajo de este porcentaje de la capacidad, el edificio hace aparecer mágicamente la cantidad configurada de correo sin clasificar." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "Cantidad de correo sin clasificar a añadir" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "Correo adicional añadido cuando se rellena mágicamente el correo sin clasificar.\n" +
                    "Porcentaje de la capacidad máxima de almacenamiento." },

                // ---- Reset ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "Restablecer valores del juego" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)),
                    "Restaura todos los ajustes postales al comportamiento original del juego (vanilla)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "Restablecer valores recomendados" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "Inicio rápido: aplica los ajustes postales recomendados." },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), "" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)),
                    "Resumen de oficinas de correos, furgonetas, centros de clasificación y camiones postales procesados en la última actualización del juego (~cada 45 minutos de juego)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "Correo de la ciudad" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "Muestra el flujo reciente de correo a nivel de ciudad.\n\n" +
                     "Correo generado = cantidad de correo producida por los ciudadanos.\n" +
                     "Correo procesado   = cantidad de correo realmente manejada por la red.\n\n" +
                     "- Si el correo procesado se mantiene por encima del generado, la red postal tiene suficiente capacidad " +
                     "y se puede reducir el presupuesto o la flota si se desea.\n" +
                     "- Si el generado se mantiene por encima del procesado durante mucho tiempo, la ciudad genera más correo de lo que\n " +
                     "la red puede manejar: añade más edificios, vehículos o ajusta los parámetros." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "Actividad" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)),
                    "Cuenta los rellenos de correo y las limpiezas de desbordamiento realizadas en la última actualización." },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)),
                    "Nombre visible de este mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Versión" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)),
                    "Versión actual del mod." },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)),
                    "Abre la página de **Paradox** para **Magic PostMaster** y otros mods." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)),
                    "Abre el chat de comentarios en **Discord** en el navegador." },
            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
