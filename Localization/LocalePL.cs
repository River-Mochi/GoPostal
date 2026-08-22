// <copyright file="LocalePL.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// LocalePL.cs
// Polish locale pl-PL

namespace MagicMail
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// Polish localization source for Magic Mail [MM].</summary>
    public sealed class LocalePL : IDictionarySource
    {
        private readonly Setting m_Setting;

        /// <summary>
        /// Constructs the Polish locale generator.</summary>
        /// <param name="setting">Settings object used for locale IDs.</param>
        public LocalePL(Setting setting)
        {
            m_Setting = setting;
        }

        /// <summary>
        /// Generates all Polish localization entries for this mod.</summary>
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod title
                { m_Setting.GetSettingsLocaleID(), "Magic Mail + Postal Dispatch" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "Działania" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab), "Status" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab), "O modzie" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup), "Pomoc w obsłudze poczty" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup), "Furgonetki i ciężarówki" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "Sortownia" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup), "Reset" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "Skan miasta" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "Ostatnia aktualizacja" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup), "Informacje" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "Linki" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "Napraw mało poczty lokalnej" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "Po włączeniu pojawi się trochę poczty, gdy jej zapas spadnie za nisko.\n" +
                    "Nie tworzy dodatkowych furgonetek — trochę jak magia... ale działa :)"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "Próg poczty lokalnej" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "Gdy poczta lokalna spadnie poniżej wybranego procentu,\n" +
                    "urząd pocztowy pobierze więcej poczty lokalnej.\n" +
                    "To procent maksymalnej pojemności magazynu budynku.\n" +
                    "Np. przy <maks. magazyn = 100 000> i <próg = 5%>,\n" +
                    "gdy poczta lokalna < <5 000>, zostanie pobrana dodatkowa poczta."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "Ilość pobieranej poczty lokalnej" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "Procent dodawany przy pobieraniu poczty lokalnej (magiczne uzupełnienie).\n" +
                    "Jeśli maksimum vanilla = <100 000>, a ustawisz <10%>,\n" +
                    "w razie potrzeby zostanie dodane <10 000>."
                },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)), "Napraw przepełnienie poczty" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "Gdy poczty jest za dużo, obiekty wykonują małe magiczne sprzątanie.\n" +
                    "Nadmiar zapisanej poczty jest uznawany za dostarczony i usuwany.\n" +
                    "Dzięki temu obiekty nie pozostają na zawsze całkowicie zapełnione.\n" +
                    "Wyłącz, jeśli chcesz zachować czyste zachowanie vanilla."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "Próg przepełnienia urzędu pocztowego" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "Gdy całkowita ilość poczty w urzędzie osiągnie ten procent, mod\n" +
                    "usuwa tyle poczty, aby wrócić do tego poziomu."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "Próg przepełnienia sortowni" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "Gdy całkowita ilość poczty w sortowni osiągnie ten procent, mod\n" +
                    "usuwa tyle poczty, aby wrócić do tego poziomu."
                },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "Zmień pojemności" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "Włącz, aby zmieniać pojemności furgonetek i ciężarówek. Gdy wyłączone,\n" +
                    "wszystkie suwaki pojemności poniżej są ukryte, a\n" +
                    "gra używa wartości vanilla bez względu na pozostawione ustawienia suwaków."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "Ładunek furgonetki pocztowej" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "Określa, ile poczty może przewieźć każdy furgon pocztowy.\n" +
                    "<100% = ładunek vanilla.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "Liczba furgonetek pocztowych" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "Określa, ile furgonetek każdy obiekt pocztowy może posiadać i wysyłać.\n" +
                    "<100% = liczba vanilla.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "Liczba ciężarówek pocztowych" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "Określa, ile ciężarówek pocztowych może posiadać i wysyłać każda sortownia\n" +
                    "(oraz każdy obiekt mający ciężarówki pocztowe).\n" +
                    "<100% = liczba vanilla.>"
                },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "Szybkość sortowania" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "Mnożnik dla **sortowni**. Dotyczy podstawowej szybkości sortowania obiektu.\n" +
                    "<100% = vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "Pojemność magazynu sortowni" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "Kontroluje **magazyn poczty**.\n" +
                    "<100% = vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "Napraw mało niesortowanej poczty" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "Po włączeniu pojawi się trochę niesortowanej poczty, gdy zapasy spadną za nisko.\n" +
                    "Dzięki temu sortownie mogą dalej pracować.\n" +
                    "To tymczasowe obejście obecnego błędu, przez który sortownie nie dostają dość poczty, gdy w mieście jest port cargo."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "Próg niesortowanej poczty" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "Gdy niesortowana poczta spadnie poniżej tego niskiego procentu całkowitej pojemności,\n" +
                    "zostanie pobrana dodatkowa niesortowana poczta."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "Ilość niesortowanej poczty" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "Dodatkowa ilość przy pobieraniu niesortowanej poczty (magiczne uzupełnienie).\n" +
                    "To procent maksymalnej pojemności magazynu.\n" +
                    "Jeśli vanilla <maks. = 250 000> i ustawisz <10%>, zostanie dodane <25 000>."
                },

                // ---- RESET BUTTONS ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "Domyślne gry" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)), "Przywraca wszystkie ustawienia do oryginalnego zachowania gry (vanilla)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "Zalecane" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "**Szybki start** – zastosuj wszystkie zalecane ustawienia poczty.\n" +
                    "Tryb prosty: jeden klik i gotowe!"
                },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), string.Empty },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)), "Podsumowanie urzędów pocztowych, furgonetek, sortowni i ciężarówek z ostatniego skanu w tle." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "Poczta miesięczna" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "Pokazuje ostatni przepływ poczty w całym mieście.\n" +
                    "\n" +
                    "**Nagromadzona** = ile poczty wygenerowali mieszkańcy.\n" +
                    "**Przetworzona** = ile poczty sieć faktycznie obsłużyła.\n" +
                    "\n" +
                    "- Jeśli Przetworzona często jest wyższa niż Nagromadzona, sieć ma wystarczającą wydajność.\n" +
                    "- Jeśli Nagromadzona długo pozostaje wyższa niż Przetworzona,\n" +
                    "miasto generuje więcej poczty, niż sieć może obsłużyć.\n" +
                    "Dodaj obiekty lub furgonetki albo zmień ustawienia."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "Aktywność" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)), "Liczba uzupełnień poczty i czyszczeń przepełnienia wykonanych podczas ostatniej aktualizacji." },

                // ---- Status text templates (for MagicMailSystem) ----
                { "MM_STATUS_NO_FACILITIES", "Nie przetworzono jeszcze żadnych obiektów pocztowych. Otwórz miasto i uruchom symulację na chwilę." },

                { "MM_STATUS_NO_ACTIVITY", "Nie zarejestrowano jeszcze aktywności." },

                { "MM_STATUS_SUMMARY", "{0} urzędów pocztowych | {1} furgonetek | {2} sortowni | {3} ciężarówek" },

                { "MM_STATUS_ACTIVITY", "{0} uzupełnień lokalnej | {1} uzupełnień niesortowanej | {2} czyszczeń przepełnienia" },

                { "MM_STATUS_CITY_MAIL_NOT_READY", "Statystyki poczty miejskiej nie są jeszcze dostępne. Otwórz miasto i uruchom symulację na chwilę." },

                { "MM_STATUS_CITY_MAIL", "{0} nagromadzona | {1} przetworzona" },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "Wyświetlana nazwa tego moda." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Wersja" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "Aktualna wersja moda." },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)), "Otwiera stronę **Paradox** dla **Magic Mail** i innych modów." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Otwiera czat opinii **Discord** w przeglądarce." },

            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
