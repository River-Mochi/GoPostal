// <copyright file="LocaleTR.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// LocaleTR.cs
// Turkish locale tr-TR

namespace MagicMail
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// Turkish localization source for Magic Mail [MM].</summary>
    public sealed class LocaleTR : IDictionarySource
    {
        private readonly Setting m_Setting;

        /// <summary>
        /// Constructs the Turkish locale generator.</summary>
        /// <param name="setting">Settings object used for locale IDs.</param>
        public LocaleTR(Setting setting)
        {
            m_Setting = setting;
        }

        /// <summary>
        /// Generates all Turkish localization entries for this mod.</summary>
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod title
                { m_Setting.GetSettingsLocaleID(), "Magic Mail + Postal Dispatch" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "Eylemler" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab),  "Durum" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab),   "Hakkında" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup),          "Posta dağıtım yardımı" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup),             "Posta minibüsleri ve kamyonları" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "Ayırma tesisi" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup),               "Sıfırla" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup),  "Şehir taraması" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "Son güncelleme" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup),  "Bilgi" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "Bağlantılar" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "Düşük yerel postayı düzelt" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "Etkinleştirilirse, posta miktarı çok düşerse az miktarda posta ortaya çıkar.\n " +
                    "Ekstra minibüs oluşturmaz; biraz sihir gibi... ama gerçek :)"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "Yerel posta eşiği" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "Yerel posta seçtiğiniz bu yüzdesinin altına düşerse,\n " +
                    "postane daha fazla yerel posta çekmeye başlar.\n" +
                    "Bu, binanın maksimum depolama kapasitesinin yüzdesidir.\n" +
                    "Örn. <maks depolama = 100,000> ve <eşik = 5%> ise,\n" +
                    "yerel posta < <5,000> olduğunda daha fazla posta alınır."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "Yerel posta alma miktarı" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "Yerel posta alınırken eklenecek yüzde (sihirli takviye).\n" +
                    "Vanilla maksimum <100,000> ve bu değer <10%> ise\n" +
                    "gerektiğinde <10,000> eklenir."
                },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)), "Posta taşmasını düzelt" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "Çok fazla posta olduğunda tesisler küçük bir sihirli temizlik yapar.\n " +
                    "Fazla depolanmış posta teslim edilmiş sayılır ve kaldırılır.\n " +
                    "Bu düzeltme tesislerin sonsuza kadar dolu kalmasını önler.\n " +
                    "Tam vanilla davranışını korumak için bunu kapatın."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "Postane taşma eşiği" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "Bir postanedeki toplam posta bu yüzdeye ulaştığında mod,\n" +
                    "depolanan postayı bu seviyeye düşürecek kadar siler."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "Ayırma tesisi taşma eşiği" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "Bir ayırma tesisindeki toplam posta bu yüzdeye ulaştığında mod,\n" +
                    "depolanan postayı bu seviyeye düşürecek kadar siler."
                },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "Kapasiteleri değiştir" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "Minibüs ve kamyon kapasitelerini değiştirmek için etkinleştirin. Kapalıyken,\n" +
                    "aşağıdaki tüm kapasite kaydırıcıları gizlenir ve\n" +
                    "kaydırıcıları farklı değerlerde bıraksanız bile vanilla (oyun) değerleri kullanılır."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "Posta minibüsü yükü" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "Her posta minibüsünün ne kadar posta taşıyabileceğini kontrol eder.\n" +
                    "<100% = vanilla yük kapasitesi.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "Posta minibüsü filosu" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "Her posta binasının sahip olabileceği ve gönderebileceği posta minibüsü sayısını kontrol eder.\n" +
                    "<100% = vanilla filo büyüklüğü.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "Posta kamyonu filosu" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "Her ayırma tesisinin (ve posta kamyonu olan herhangi bir tesisin) sahip olabileceği\n " +
                    "ve gönderebileceği posta kamyonu sayısını kontrol eder.\n " +
                    "<100% = vanilla filo büyüklüğü.>"
                },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "Ayırma hızı" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "**Ayırma** tesisleri için çarpan. Tesisin temel ayırma hızına uygulanır.\n " +
                    "<100% = vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "Ayırma depolama kapasitesi" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "**Posta depolamasını** kontrol eder.\n " +
                    "<100% = vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "Düşük ayrılmamış postayı düzelt" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "Etkinleştirildiğinde, depodaki miktar çok düşerse biraz ayrılmamış posta sihirli şekilde ortaya çıkar.\n " +
                    "Bu, ayırma binalarının aktif kalmasını sağlar.\n" +
                    "Kargo limanı varken ayırma tesislerinin yeterli posta alamadığı mevcut bir hata için geçici çözümdür."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "Ayrılmamış posta eşiği" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "Ayrılmamış posta toplam depolama kapasitesinin bu düşük yüzdesinin altına düşerse,\n" +
                    "bir miktar ek ayrılmamış posta alınır.\n"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "Ayrılmamış posta alma miktarı" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "Ayrılmamış posta alınırken eklenecek miktar (sihirli takviye).\n" +
                    "Miktar, maksimum depolama kapasitesinin yüzdesidir.\n" +
                    "Vanilla <maks = 250,000> ve bu değer <10%> ise <25,000> eklenir."
                },

                // ---- RESET BUTTONS ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "Oyun varsayılanları" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)),
                    "Tüm ayarları oyunun orijinal varsayılan davranışına (vanilla) geri döndürür."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "Önerilen" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "**Hızlı Başlangıç** – önerilen tüm posta ayarlarını uygular.\n" +
                    "Kolay mod: 1 tıkla ve bitti!"
                },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), string.Empty },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)),
                    "Son arka plan taramasında işlenen postanelerin, posta minibüslerinin, ayırma tesislerinin ve posta kamyonlarının özeti."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "Aylık posta" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "Şehir genelindeki son posta akışını gösterir.\n\n" +
                    "**Birikmiş** = vatandaşların ürettiği posta miktarı.\n" +
                    "**İşlenmiş**  = posta ağının gerçekten işlediği miktar.\n\n" +
                    "- İşlenmiş miktar sık sık Birikmiş miktardan yüksekse posta ağınızın kapasitesi yeterlidir.\n " +
                    "- Birikmiş miktar uzun süre İşlenmiş miktarın üzerinde kalırsa,\n" +
                    "şehir işleyebileceğinden daha fazla posta üretiyor demektir.\n" +
                    "Daha fazla tesis veya minibüs ekleyin ya da ayarlarınızı değiştirin."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "Etkinlik" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)),
                    "Son güncellemede yapılan posta takviyelerinin ve taşma temizliklerinin sayısı."
                },

                // ---- Status text templates (for MagicMailSystem) ----
                { "MM_STATUS_NO_FACILITIES",
                  "Henüz hiçbir posta tesisi işlenmedi. Bir şehir açın ve simülasyonu çalıştırın." },

                { "MM_STATUS_NO_ACTIVITY",
                  "Henüz hiçbir etkinlik kaydedilmedi." },

                {
                    "MM_STATUS_SUMMARY",
                    "{0} postane | {1} posta minibüsü | {2} ayırma binası | {3} posta kamyonu"
                },

                {
                    "MM_STATUS_ACTIVITY",
                    "{0} yerel posta takviyesi | {1} ayrılmamış posta takviyesi | {2} taşma temizliği"
                },

                { "MM_STATUS_CITY_MAIL_NOT_READY",
                  "Şehir posta istatistikleri henüz hazır değil. Bir şehir açın ve simülasyonu çalıştırın." },

                {
                    "MM_STATUS_CITY_MAIL",
                    "{0} birikmiş | {1} işlenmiş"
                },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)),
                    "Bu modun görüntülenen adı."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Sürüm" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)),
                    "Geçerli mod sürümü."
                },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)),
                    "**Magic Mail** ve diğer modlar için **Paradox** web sayfasını açar."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)),
                    "**Discord** geri bildirim sohbetini tarayıcıda açar."
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
