// <copyright file="LocaleIT.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// LocaleIT.cs
// Italian locale it-IT

namespace MagicMail
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// Italian localization source for Magic Mail [MM].</summary>
    public sealed class LocaleIT : IDictionarySource
    {
        private readonly Setting m_Setting;

        /// <summary>
        /// Constructs the Italian locale generator.</summary>
        /// <param name="setting">Settings object used for locale IDs.</param>
        public LocaleIT(Setting setting)
        {
            m_Setting = setting;
        }

        /// <summary>
        /// Generates all Italian localization entries for this mod.</summary>
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod title
                { m_Setting.GetSettingsLocaleID(), "Magic Mail + Postal Dispatch" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "Azioni" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab), "Stato" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab), "Informazioni" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup), "Aiuto alla distribuzione postale" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup), "Furgoni e camion" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "Centro di smistamento" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup), "Reimposta" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "Scansione città" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "Ultimo aggiornamento" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "Link" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "Correggi poca posta locale" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "Se attivo, compare un po' di posta quando le scorte diventano troppo basse.\n" +
                    "Non crea furgoni extra: è un po' come magia... ma vera :)"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "Soglia posta locale" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "Se la posta locale scende sotto questa percentuale scelta da te,\n" +
                    "l'ufficio postale recupera altra posta locale.\n" +
                    "È una percentuale della capacità massima dell'edificio.\n" +
                    "Es.: <stoccaggio max = 100.000> e <soglia = 5%>,\n" +
                    "quando la posta locale < <5.000>, viene recuperata altra posta."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "Quantità posta locale" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "Percentuale aggiunta quando viene recuperata posta locale (ricarica magica).\n" +
                    "Se il massimo vanilla = <100.000> e imposti <10%>,\n" +
                    "vengono aggiunti <10.000> quando serve."
                },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)), "Correggi sovraccarico di posta" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "Quando c'è troppa posta, le strutture fanno una piccola pulizia magica.\n" +
                    "La posta in eccesso viene considerata consegnata e rimossa.\n" +
                    "Così le strutture non restano bloccate piene per sempre.\n" +
                    "Disattiva per mantenere il comportamento vanilla puro."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "Soglia sovraccarico ufficio postale" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "Quando la posta totale di un ufficio raggiunge questa percentuale, la mod\n" +
                    "rimuove abbastanza posta da riportarla a questo livello."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "Soglia sovraccarico centro di smistamento" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "Quando la posta totale di un centro di smistamento raggiunge questa percentuale, la mod\n" +
                    "rimuove abbastanza posta da riportarla a questo livello."
                },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "Modifica capacità" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "Attiva per modificare le capacità di furgoni e camion. Se disattivato,\n" +
                    "tutti i cursori di capacità qui sotto vengono nascosti e\n" +
                    "si usano i valori vanilla del gioco anche se hai lasciato i cursori su altri valori."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "Carico del furgone postale" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "Controlla quanta posta può trasportare ogni furgone postale.\n" +
                    "<100% = carico vanilla.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "Flotta di furgoni postali" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "Controlla quanti furgoni ogni edificio postale può possedere e inviare.\n" +
                    "<100% = flotta vanilla.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "Flotta di camion postali" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "Controlla quanti camion postali ogni centro di smistamento (e ogni struttura con camion postali)\n" +
                    "può possedere e inviare.\n" +
                    "<100% = flotta vanilla.>"
                },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "Velocità di smistamento" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "Moltiplicatore per i centri di **smistamento**. Si applica alla velocità base della struttura.\n" +
                    "<100% = vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "Capacità di stoccaggio" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "Controlla lo **stoccaggio della posta**.\n" +
                    "<100% = vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "Correggi poca posta non smistata" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "Se attivo, compare un po' di posta non smistata quando le scorte diventano troppo basse.\n" +
                    "Così i centri di smistamento restano attivi.\n" +
                    "È una soluzione temporanea a un bug attuale per cui i centri non ricevono abbastanza posta se è presente un porto merci."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "Soglia posta non smistata" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "Se la posta non smistata scende sotto questa bassa percentuale della capacità totale,\n" +
                    "viene recuperata un po' di posta non smistata in più."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "Quantità posta non smistata" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "Posta extra aggiunta quando si recupera posta non smistata (ricarica magica).\n" +
                    "La quantità è una percentuale della capacità massima.\n" +
                    "Se vanilla <max = 250.000> e imposti <10%>, vengono aggiunti <25.000>."
                },

                // ---- RESET BUTTONS ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "Valori del gioco" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)), "Ripristina tutte le impostazioni al comportamento originale del gioco (vanilla)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "Consigliato" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "**Avvio rapido** – applica tutte le impostazioni postali consigliate.\n" +
                    "Modalità facile: un clic e fatto!"
                },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), string.Empty },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)), "Riepilogo di uffici postali, furgoni, centri di smistamento e camion postali elaborati nell'ultima scansione in background." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "Posta mensile" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "Mostra il flusso recente della posta in tutta la città.\n" +
                    "\n" +
                    "**Accumulata** = quanta posta hanno generato i cittadini.\n" +
                    "**Gestita** = quanta posta la rete ha effettivamente elaborato.\n" +
                    "\n" +
                    "- Se Gestita è spesso maggiore di Accumulata, la rete postale ha abbastanza capacità.\n" +
                    "- Se Accumulata resta sopra Gestita per molto tempo,\n" +
                    "la città genera più posta di quanta la rete possa gestire.\n" +
                    "Aggiungi strutture o furgoni, oppure modifica le impostazioni."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "Attività" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)), "Conta le ricariche di posta e le pulizie di sovraccarico eseguite nell'ultimo aggiornamento." },

                // ---- Status text templates (for MagicMailSystem) ----
                { "MM_STATUS_NO_FACILITIES", "Nessuna struttura postale ancora elaborata. Apri una città e lascia girare la simulazione." },

                { "MM_STATUS_NO_ACTIVITY", "Nessuna attività registrata finora." },

                { "MM_STATUS_SUMMARY", "{0} uffici postali | {1} furgoni postali | {2} centri di smistamento | {3} camion postali" },

                { "MM_STATUS_ACTIVITY", "{0} ricariche posta locale | {1} ricariche posta non smistata | {2} pulizie sovraccarico" },

                { "MM_STATUS_CITY_MAIL_NOT_READY", "Le statistiche della posta cittadina non sono ancora disponibili. Apri una città e lascia girare la simulazione." },

                { "MM_STATUS_CITY_MAIL", "{0} accumulata | {1} gestita" },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "Nome visualizzato di questa mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Versione" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "Versione attuale della mod." },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)), "Apre la pagina **Paradox** di **Magic Mail** e delle altre mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Apre la chat di feedback **Discord** nel browser." },

            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
