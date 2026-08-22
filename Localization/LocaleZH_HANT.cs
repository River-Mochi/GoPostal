// <copyright file="LocaleZH_HANT.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// LocaleZH_HANT.cs
// Traditional Chinese locale zh-HANT

namespace MagicMail
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// Traditional Chinese localization source for Magic Mail [MM].</summary>
    public sealed class LocaleZH_HANT : IDictionarySource
    {
        private readonly Setting m_Setting;

        /// <summary>
        /// Constructs the Traditional Chinese locale generator.</summary>
        /// <param name="setting">Settings object used for locale IDs.</param>
        public LocaleZH_HANT(Setting setting)
        {
            m_Setting = setting;
        }

        /// <summary>
        /// Generates all Traditional Chinese localization entries for this mod.</summary>
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod title
                { m_Setting.GetSettingsLocaleID(), "Magic Mail + Postal Dispatch" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "操作" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab), "狀態" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab), "關於" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup), "郵政配送輔助" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup), "郵政廂型車和卡車" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "郵件分揀中心" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup), "重設" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "城市掃描" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "最近更新" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup), "資訊" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "連結" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "修正本地郵件不足" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "啟用後，本地郵件太少時會自動補上一小部分。\n" +
                    "不會額外產生郵政車，就像一點小魔法……但真的有效 :)"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "本地郵件門檻" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "當本地郵件低於你設定的這個百分比時，\n" +
                    "郵局會補充更多本地郵件。\n" +
                    "此比例依建築的最大儲存容量計算。\n" +
                    "例如：<最大儲存 = 100,000>、<門檻 = 5%> 時，\n" +
                    "本地郵件 < <5,000> 就會補充更多郵件。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "本地郵件補充量" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "補充本地郵件時增加的百分比（魔法補貨）。\n" +
                    "如果原版最大值 = <100,000>，這裡設為 <10%>，\n" +
                    "需要時會增加 <10,000>。"
                },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)), "修正郵件溢出" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "郵件太多時，設施會做一次小小的「魔法清理」。\n" +
                    "多餘的儲存郵件會視為已送達並移除。\n" +
                    "這可避免設施長時間卡在滿倉狀態。\n" +
                    "關閉此項即可保持純原版行為。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "郵局溢出門檻" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "當郵局內的郵件總量達到這個百分比時，模組會\n" +
                    "刪除足夠的郵件，讓存量回到這個水平。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "分揀中心溢出門檻" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "當分揀中心內的郵件總量達到這個百分比時，模組會\n" +
                    "刪除足夠的郵件，讓存量回到這個水平。"
                },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "修改運力" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "啟用後可修改廂型車和卡車的容量。關閉時，\n" +
                    "下方所有容量滑桿都會隱藏，而且\n" +
                    "不論滑桿之前設成多少，都會使用遊戲原版數值。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "郵政廂型車載量" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "控制每輛郵政廂型車可攜帶多少郵件。\n" +
                    "<100% = 原版載量>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "郵政廂型車數量" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "控制每個郵政建築可擁有和派出的郵政廂型車數量。\n" +
                    "<100% = 原版車隊規模>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "郵政卡車數量" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "控制每個分揀中心（以及任何擁有郵政卡車的設施）\n" +
                    "可擁有和派出的郵政卡車數量。\n" +
                    "<100% = 原版車隊規模>"
                },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "分揀速度" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "**分揀**設施倍率，套用至設施的基礎分揀速度。\n" +
                    "<100% = 原版>。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "分揀儲存容量" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "控制**郵件儲存容量**。\n" +
                    "<100% = 原版>。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "修正未分揀郵件不足" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "啟用後，未分揀郵件庫存太低時會自動補上一小部分。\n" +
                    "這可讓分揀設施繼續運作。\n" +
                    "這是目前問題的暫時修正：存在貨運港口時，分揀設施可能收不到足夠郵件。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "未分揀郵件門檻" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "當未分揀郵件低於總儲存容量的這個低百分比時，\n" +
                    "會額外補充一些未分揀郵件。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "未分揀郵件補充量" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "補充未分揀郵件時額外增加的數量（魔法補貨）。\n" +
                    "數量依最大儲存容量的百分比計算。\n" +
                    "如果原版 <最大 = 250,000> 且這裡設為 <10%>，則增加 <25,000>。"
                },

                // ---- RESET BUTTONS ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "遊戲預設值" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)), "將所有設定還原為遊戲原本的預設行為（原版）。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "推薦" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "**快速開始** – 一次套用所有推薦的郵政設定。\n" +
                    "簡單模式：點一下就完成！"
                },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), string.Empty },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)), "上一次背景掃描中處理到的郵局、郵政廂型車、分揀設施和郵政卡車摘要。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "每月郵件" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "顯示最近的全市郵件流量。\n" +
                    "\n" +
                    "**累積** = 市民產生了多少郵件。\n" +
                    "**處理** = 郵政網路實際處理了多少郵件。\n" +
                    "\n" +
                    "- 如果「處理」經常高於「累積」，表示郵政網路容量足夠。\n" +
                    "- 如果「累積」長時間高於「處理」，\n" +
                    "表示城市產生的郵件超過網路的處理能力。\n" +
                    "增加設施或郵政廂型車，或調整設定。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "活動" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)), "上次更新中執行的郵件補充和溢出清理次數。" },

                // ---- Status text templates (for MagicMailSystem) ----
                { "MM_STATUS_NO_FACILITIES", "尚未處理任何郵政設施。開啟一座城市並讓模擬運行一會兒。" },

                { "MM_STATUS_NO_ACTIVITY", "尚未記錄任何活動。" },

                { "MM_STATUS_SUMMARY", "{0} 個郵局 | {1} 輛郵政廂型車 | {2} 個分揀設施 | {3} 輛郵政卡車" },

                { "MM_STATUS_ACTIVITY", "{0} 次本地郵件補充 | {1} 次未分揀郵件補充 | {2} 次溢出清理" },

                { "MM_STATUS_CITY_MAIL_NOT_READY", "城市郵件統計尚不可用。開啟一座城市並讓模擬運行一會兒。" },

                { "MM_STATUS_CITY_MAIL", "{0} 累積 | {1} 處理" },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "模組" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "此模組的顯示名稱。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "版本" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "目前的模組版本。" },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)), "開啟 **Magic Mail** 和其他模組的 **Paradox** 頁面。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "在瀏覽器中開啟 **Discord** 意見回饋聊天。" },

            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
