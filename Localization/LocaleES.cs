// <copyright file="LocaleES.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// LocaleES.cs
// Spanish locale es-ES

namespace MagicMail
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// Spanish localization source for Magic Mail [MM].</summary>
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
                { m_Setting.GetSettingsLocaleID(), "Magic Mail + Postal Dispatch" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "Acciones" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab), "Estado" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab), "Acerca de" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup), "Ayuda para el reparto postal" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup), "Furgonetas y camiones" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "Centro de clasificación" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup), "Restablecer" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "Escaneo de la ciudad" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "Última actualización" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup), "Información" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "Enlaces" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "Corregir poco correo local" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "Si está activado, aparece un poco de correo cuando el nivel baja demasiado.\n" +
                    "No genera furgonetas extra; es como magia... pero de verdad :)"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "Umbral de correo local" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "Si el correo local baja de este porcentaje que elijas,\n" +
                    "la oficina de correos traerá más correo local.\n" +
                    "Es un porcentaje del almacenamiento máximo del edificio.\n" +
                    "Ej.: <almacenamiento máx. = 100.000> y <umbral = 5%>,\n" +
                    "si el correo local < <5.000>, se trae más correo."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "Cantidad de correo local" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "Porcentaje que se añade al traer correo local (recarga mágica).\n" +
                    "Si el máximo vanilla = <100.000> y está en <10%>,\n" +
                    "se añaden <10.000> cuando hace falta."
                },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)), "Corregir desbordamiento de correo" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "Cuando hay demasiado correo, las instalaciones hacen una pequeña limpieza mágica.\n" +
                    "El correo sobrante se considera entregado y se elimina.\n" +
                    "Así se evita que las instalaciones se queden llenas para siempre.\n" +
                    "Desactívalo para mantener el comportamiento vanilla puro."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "Umbral de desbordamiento de la oficina" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "Cuando el correo total de una oficina llega a este porcentaje, el mod\n" +
                    "elimina lo necesario para volver a este nivel."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "Umbral de desbordamiento del centro" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "Cuando el correo total de un centro de clasificación llega a este porcentaje, el mod\n" +
                    "elimina lo necesario para volver a este nivel."
                },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "Cambiar capacidades" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "Actívalo para modificar las capacidades de furgonetas y camiones. Si está apagado,\n" +
                    "se ocultan todos los controles de capacidad y\n" +
                    "se usan los valores vanilla del juego aunque hayas dejado otros valores en los controles."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "Carga de la furgoneta postal" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "Controla cuánto correo puede llevar cada furgoneta postal.\n" +
                    "<100% = carga vanilla.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "Flota de furgonetas postales" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "Controla cuántas furgonetas puede tener y enviar cada edificio postal.\n" +
                    "<100% = flota vanilla.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "Flota de camiones postales" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "Controla cuántos camiones postales puede tener y enviar cada centro de clasificación\n" +
                    "(y cualquier instalación que tenga camiones postales).\n" +
                    "<100% = flota vanilla.>"
                },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "Velocidad de clasificación" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "Multiplicador para los centros de **clasificación**. Se aplica a su velocidad base.\n" +
                    "<100% = vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "Capacidad de almacenamiento" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "Controla el **almacenamiento de correo**.\n" +
                    "<100% = vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "Corregir poco correo sin clasificar" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "Si está activado, aparece algo de correo sin clasificar cuando las reservas bajan demasiado.\n" +
                    "Así los centros de clasificación siguen funcionando.\n" +
                    "Es una solución temporal a un error actual por el que los centros no reciben suficiente correo si hay un puerto de carga."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "Umbral de correo sin clasificar" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "Si el correo sin clasificar baja de este pequeño porcentaje de la capacidad total,\n" +
                    "se trae un poco más de correo sin clasificar."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "Cantidad de correo sin clasificar" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "Correo adicional que se añade al traer correo sin clasificar (recarga mágica).\n" +
                    "La cantidad es un porcentaje de la capacidad máxima.\n" +
                    "Si vanilla <máx. = 250.000> y está en <10%>, se añaden <25.000>."
                },

                // ---- RESET BUTTONS ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "Valores del juego" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)), "Restaura todos los ajustes al comportamiento original del juego (vanilla)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "Recomendado" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "**Inicio rápido** – aplica todos los ajustes postales recomendados.\n" +
                    "Modo fácil: un clic y listo."
                },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), string.Empty },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)), "Resumen de oficinas de correos, furgonetas, centros de clasificación y camiones procesados en el último escaneo en segundo plano." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "Correo mensual" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "Muestra el flujo reciente de correo en toda la ciudad.\n" +
                    "\n" +
                    "**Acumulado** = cuánto correo generaron los ciudadanos.\n" +
                    "**Procesado** = cuánto correo gestionó realmente la red.\n" +
                    "\n" +
                    "- Si Procesado suele ser mayor que Acumulado, tu red postal tiene capacidad suficiente.\n" +
                    "- Si Acumulado se mantiene por encima de Procesado durante mucho tiempo,\n" +
                    "la ciudad genera más correo del que la red puede manejar.\n" +
                    "Añade más instalaciones o furgonetas, o ajusta tus opciones."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "Actividad" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)), "Cuenta las recargas de correo y las limpiezas de desbordamiento realizadas en la última actualización." },

                // ---- Status text templates (for MagicMailSystem) ----
                { "MM_STATUS_NO_FACILITIES", "Aún no se han procesado instalaciones postales. Abre una ciudad y deja correr la simulación." },

                { "MM_STATUS_NO_ACTIVITY", "Todavía no se ha registrado actividad." },

                { "MM_STATUS_SUMMARY", "{0} oficinas | {1} furgonetas postales | {2} centros de clasificación | {3} camiones postales" },

                { "MM_STATUS_ACTIVITY", "{0} recargas de correo local | {1} recargas de correo sin clasificar | {2} limpiezas de desbordamiento" },

                { "MM_STATUS_CITY_MAIL_NOT_READY", "Las estadísticas de correo de la ciudad aún no están disponibles. Abre una ciudad y deja correr la simulación." },

                { "MM_STATUS_CITY_MAIL", "{0} acumulado | {1} procesado" },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "Nombre visible de este mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Versión" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "Versión actual del mod." },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)), "Abre la página de **Paradox** de **Magic Mail** y otros mods." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Abre el chat de comentarios de **Discord** en el navegador." },

            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
