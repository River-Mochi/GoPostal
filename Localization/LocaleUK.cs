// <copyright file="LocaleUK.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// LocaleUK.cs
// Ukrainian locale uk-UA

namespace MagicMail
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// Ukrainian localization source for Magic Mail [MM].</summary>
    public sealed class LocaleUK : IDictionarySource
    {
        private readonly Setting m_Setting;

        /// <summary>
        /// Constructs the Ukrainian locale generator.</summary>
        /// <param name="setting">Settings object used for locale IDs.</param>
        public LocaleUK(Setting setting)
        {
            m_Setting = setting;
        }

        /// <summary>
        /// Generates all Ukrainian localization entries for this mod.</summary>
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod title
                { m_Setting.GetSettingsLocaleID(), "Magic Mail + Postal Dispatch" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "Дії" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab),  "Стан" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab),   "Про мод" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup),          "Допомога поштовій доставці" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup),             "Поштові фургони й вантажівки" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "Сортувальний центр" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup),               "Скидання" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup),  "Сканування міста" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "Останнє оновлення" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup),  "Інформація" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "Посилання" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "Виправити нестачу місцевої пошти" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "Якщо ввімкнено, невелика кількість пошти з’являється, коли її стає надто мало.\n " +
                    "Додаткові фургони не створюються; це ніби магія... але справжня :)"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "Поріг місцевої пошти" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "Якщо місцева пошта опускається нижче вибраного вами відсотка,\n " +
                    "поштове відділення починає отримувати більше місцевої пошти.\n" +
                    "Це відсоток від максимальної місткості сховища будівлі.\n" +
                    "Напр., якщо <макс. сховище = 100,000> і <поріг = 5%>,\n" +
                    "коли місцева пошта < <5,000>, додається більше пошти."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "Обсяг отримання місцевої пошти" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "Відсоток, що додається під час отримання місцевої пошти (магічне поповнення).\n" +
                    "Якщо максимум vanilla = <100,000>, а тут встановлено <10%>,\n" +
                    "то за потреби додається <10,000>."
                },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)), "Виправити переповнення пошти" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "Коли пошти забагато, об’єкти виконують невелике магічне очищення.\n " +
                    "Надлишкова збережена пошта вважається доставленою та видаляється.\n " +
                    "Це не дає об’єктам назавжди застрягати переповненими.\n " +
                    "Вимкніть, щоб зберегти чисту поведінку vanilla."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "Поріг переповнення поштового відділення" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "Коли загальна кількість пошти у відділенні досягає цього відсотка, мод\n" +
                    "видаляє достатньо збереженої пошти, щоб повернути її до цього рівня."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "Поріг переповнення сортувального центру" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "Коли загальна кількість пошти в сортувальному центрі досягає цього відсотка, мод\n" +
                    "видаляє достатньо збереженої пошти, щоб повернути її до цього рівня."
                },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "Змінити місткість" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "Увімкніть, щоб змінювати місткість фургонів і вантажівок. Коли вимкнено,\n" +
                    "усі повзунки місткості нижче приховані, а\n" +
                    "значення vanilla (гри) використовуються, навіть якщо повзунки залишилися на інших значеннях."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "Завантаження поштового фургона" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "Визначає, скільки пошти може перевозити кожен поштовий фургон.\n" +
                    "<100% = вантажопідйомність vanilla.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "Розмір парку поштових фургонів" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "Визначає, скільки поштових фургонів може мати й відправляти кожна поштова будівля.\n" +
                    "<100% = розмір парку vanilla.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "Розмір парку поштових вантажівок" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "Визначає, скільки поштових вантажівок може мати й відправляти кожен сортувальний центр\n " +
                    "(і будь-який об’єкт із поштовими вантажівками).\n " +
                    "<100% = розмір парку vanilla.>"
                },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "Швидкість сортування" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "Множник для **сортувальних** центрів. Застосовується до базової швидкості сортування.\n " +
                    "<100% = vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "Місткість сховища сортування" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "Керує **сховищем пошти**.\n " +
                    "<100% = vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "Виправити нестачу несортованої пошти" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "Коли ввімкнено, трохи несортованої пошти магічно з’являється, якщо запас стає надто малим.\n " +
                    "Це підтримує роботу сортувальних будівель.\n" +
                    "Це тимчасове виправлення поточної помилки, через яку сортувальні центри не отримують достатньо пошти за наявності вантажного порту."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "Поріг несортованої пошти" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "Якщо несортована пошта опускається нижче цього малого відсотка загальної місткості сховища,\n" +
                    "отримується трохи додаткової несортованої пошти.\n"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "Обсяг отримання несортованої пошти" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "Додаткова пошта, що додається під час отримання несортованої пошти (магічне поповнення).\n" +
                    "Кількість є відсотком від максимальної місткості сховища.\n" +
                    "Якщо vanilla <макс = 250,000> і тут встановлено <10%>, тоді додається <25,000>."
                },

                // ---- RESET BUTTONS ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "Стандартні налаштування гри" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)),
                    "Відновлює всі налаштування до оригінальної стандартної поведінки гри (vanilla)."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "Рекомендовано" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "**Швидкий старт** – застосовує всі рекомендовані поштові налаштування.\n" +
                    "Простий режим: 1 клік — і готово!"
                },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), string.Empty },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)),
                    "Підсумок поштових відділень, фургонів, сортувальних центрів і поштових вантажівок, оброблених під час останнього фонового сканування."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "Пошта за місяць" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "Показує недавній потік пошти по всьому місту.\n\n" +
                    "**Накопичено** = скільки пошти створили мешканці.\n" +
                    "**Оброблено**  = скільки пошти фактично опрацювала мережа.\n\n" +
                    "- Якщо Оброблено часто більше за Накопичено, поштова мережа має достатню пропускну здатність.\n " +
                    "- Якщо Накопичено довго залишається вище за Оброблено,\n" +
                    "місто створює більше пошти, ніж мережа може опрацювати.\n" +
                    "Додайте більше об’єктів, фургонів або змініть налаштування."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "Активність" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)),
                    "Кількість поповнень пошти й очищень переповнення, виконаних під час останнього оновлення."
                },

                // ---- Status text templates (for MagicMailSystem) ----
                { "MM_STATUS_NO_FACILITIES",
                  "Поштові об’єкти ще не оброблялися. Відкрийте місто й дайте симуляції попрацювати." },

                { "MM_STATUS_NO_ACTIVITY",
                  "Активність ще не зафіксована." },

                {
                    "MM_STATUS_SUMMARY",
                    "{0} поштових відділень | {1} поштових фургонів | {2} сортувальних будівель | {3} поштових вантажівок"
                },

                {
                    "MM_STATUS_ACTIVITY",
                    "{0} поповнень місцевої пошти | {1} поповнень несортованої пошти | {2} очищень переповнення"
                },

                { "MM_STATUS_CITY_MAIL_NOT_READY",
                  "Статистика міської пошти ще недоступна. Відкрийте місто й дайте симуляції попрацювати." },

                {
                    "MM_STATUS_CITY_MAIL",
                    "{0} накопичено | {1} оброблено"
                },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Мод" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)),
                    "Відображувана назва цього мода."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Версія" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)),
                    "Поточна версія мода."
                },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)),
                    "Відкрити вебсторінку **Paradox** для **Magic Mail** та інших модів."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)),
                    "Відкрити чат відгуків **Discord** у браузері."
                },
            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
