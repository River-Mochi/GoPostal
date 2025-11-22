// Localization/LocaleIT.cs
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
                { m_Setting.GetSettingsLocaleID(), "Magic Mail [MM]" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "Azioni" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab),  "Stato" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab),   "Info" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup),          "Ufficio postale" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup),             "Furgoni e camion postali" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "Centro di smistamento" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup),               "Reimposta" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "Panoramica città" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "Ultimo aggiornamento" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup),  "Info" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "Link" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "Correggi poca posta locale" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "Quando è attivo e lo spazio di stoccaggio è troppo basso, appare posta extra.\n " +
                    "Non genera furgoni aggiuntivi — è come magia, ma funziona davvero." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "Soglia posta locale" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "Se la posta locale scende sotto questa percentuale scelta da te,\n " +
                    "l’ufficio postale richiama altra posta locale.\n" +
                    "È una percentuale della capacità massima di stoccaggio." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "Quantità posta locale" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "Percentuale da aggiungere quando si ricarica la posta locale (ricarica magica).\n" +
                    "Se il massimo vanilla = 10.000 e imposti 20%, vengono aggiunti 2.000."},

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)), "Correggi overflow posta" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "Quando c’è troppa posta, gli edifici eseguono una piccola pulizia **magica**.\n " +
                    "La posta in eccesso viene considerata consegnata e rimossa.\n " +
                    "Questo evita che gli edifici restino bloccati come \"pieni\" per sempre.\n " +
                    "Disattiva per mantenere il comportamento vanilla puro." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "Soglia overflow ufficio postale" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "Quando la posta totale in un ufficio postale raggiunge questa percentuale,\n" +
                    "il mod elimina abbastanza posta immagazzinata per riportarla a questo livello." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "Soglia overflow smistamento" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                   "Quando la posta totale in un centro di smistamento raggiunge questa percentuale,\n" +
                   "il mod elimina abbastanza posta immagazzinata per riportarla a questo livello." },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "Modifica capacità" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "Attiva per modificare le capacità di furgoni e camion. Se disattivo,\n" +
                    "tutti gli slider di capacità sotto sono nascosti e\n" +
                    "il gioco usa i valori vanilla anche se i cursori mostrano numeri diversi." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "Carico posta furgoni" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "Controlla quanta posta può trasportare ogni furgone postale.\n" +
                    "<100% = carico vanilla.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "Dimensione flotta furgoni" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "Controlla quanti furgoni può possedere e inviare ogni edificio postale.\n" +
                    "<100% = flotta vanilla.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "Dimensione flotta camion" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "Controlla quanti camion postali può possedere e inviare ogni centro di smistamento\n " +
                    " (e qualsiasi edificio con camion postali).\n " +
                    "<100% = flotta vanilla.>" },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "Velocità di smistamento" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "Moltiplicatore per i centri di **smistamento**. Si applica alla velocità base di smistamento dell’edificio.\n " +
                    "<100% = vanilla>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "Capacità di stoccaggio smistamento" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "Controlla la **capacità di stoccaggio posta**.\n " +
                    "<100% = vanilla>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "Correggi poca posta non smistata" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "Quando è attivo, un po’ di posta non smistata appare in modo magico se le scorte sono troppo basse.\n " +
                    "Così i centri di smistamento restano attivi senza aspettare nuove consegne.\n" +
                    "È una soluzione temporanea per un bug attuale in cui i centri di smistamento ricevono poca posta\n " +
                    "se è presente un porto cargo." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "Soglia posta non smistata" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "Se la posta non smistata scende sotto questa percentuale della capacità,\n " +
                    "viene richiamata magicamente posta non smistata extra.\n" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "Quantità posta non smistata" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "Posta aggiuntiva da inserire quando si ricarica la posta non smistata (ricarica magica).\n" +
                    "Valore espresso come percentuale della capacità massima di stoccaggio." },

                // ---- RESET BUTTONS ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "Valori predefiniti del gioco" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)),
                    "Ripristina tutte le impostazioni al comportamento predefinito originale del gioco (vanilla)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "Consigliato" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "**Avvio rapido** – applica tutte le impostazioni postali consigliate.\n" +
                    "Easy-mode: un clic e fatto!" },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), "" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)),
                    "Riepilogo di uffici postali, furgoni, centri di smistamento e camion postali\n " +
                    "elaborati nell’ultimo aggiornamento di gioco (~ogni 45 minuti in-game)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "Posta cittadina" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "Mostra il flusso recente di posta in tutta la città.\n\n" +
                     "**Accumulata** = quanta posta hanno generato i cittadini.\n" +
                     "**Elaborata**   = quanta posta è stata effettivamente gestita dalla rete.\n\n" +
                     "- Se \"Elaborata\" è spesso maggiore di \"Accumulata\", la tua rete postale ha capacità sufficiente.\n " +
                     "- Se \"Accumulata\" rimane sopra \"Elaborata\" per molto tempo, la città genera più posta\n " +
                     "di quanta ne possa gestire; aggiungi altri edifici, più veicoli o modifica le impostazioni." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "Attività" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)),
                    "Conta quante ricariche di posta e pulizie di overflow sono state fatte nell’ultimo aggiornamento." },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)),
                    "Nome visualizzato di questa mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Versione" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)),
                    "Versione corrente della mod." },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)),
                    "Apri la pagina **Paradox** di **Magic Mail** e delle altre mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)),
                    "Apri la chat di feedback su **Discord** nel browser." },
            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
