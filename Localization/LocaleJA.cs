// <copyright file="LocaleJA.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// LocaleJA.cs
// Japanese locale ja-JP

namespace MagicMail
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// Japanese localization source for Magic Mail [MM].</summary>
    public sealed class LocaleJA : IDictionarySource
    {
        private readonly Setting m_Setting;

        /// <summary>
        /// Constructs the Japanese locale generator.</summary>
        /// <param name="setting">Settings object used for locale IDs.</param>
        public LocaleJA(Setting setting)
        {
            m_Setting = setting;
        }

        /// <summary>
        /// Generates all Japanese localization entries for this mod.</summary>
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
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab), "状態" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab), "概要" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup), "郵便配送サポート" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup), "郵便バン＆トラック" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "仕分け施設" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup), "リセット" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "都市スキャン" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "最新の更新" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup), "情報" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "リンク" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "ローカル郵便の不足を修正" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "有効にすると、ローカル郵便が少なくなりすぎたときに少量の郵便を追加します。\n" +
                    "追加のバンは出ません。ちょっとした魔法…でも本物です :)"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "ローカル郵便のしきい値" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "ローカル郵便が選んだ割合より少なくなると、\n" +
                    "郵便局がローカル郵便を追加で取り寄せます。\n" +
                    "建物の最大保管量に対する割合です。\n" +
                    "例：<最大保管 = 100,000>、<しきい値 = 5%> の場合、\n" +
                    "ローカル郵便が <5,000> 未満になると追加で取り寄せます。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "ローカル郵便の補充量" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "ローカル郵便を取り寄せるときに追加する割合です（魔法の補充）。\n" +
                    "バニラの最大値が <100,000> で <10%> に設定した場合、\n" +
                    "必要なときに <10,000> が追加されます。"
                },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)), "郵便のあふれを修正" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "郵便が多すぎると、施設がちょっとした魔法の掃除をします。\n" +
                    "余分な郵便は配達済みとして扱われ、削除されます。\n" +
                    "施設が満杯のまま動かなくなるのを防ぎます。\n" +
                    "完全なバニラ挙動にしたい場合はオフにしてください。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "郵便局のあふれしきい値" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "郵便局の総郵便量がこの割合に達すると、\n" +
                    "この水準まで戻るように余分な郵便を削除します。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "仕分け施設のあふれしきい値" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "仕分け施設の総郵便量がこの割合に達すると、\n" +
                    "この水準まで戻るように余分な郵便を削除します。"
                },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "容量を変更" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "バンとトラックの容量を変更します。オフの場合、\n" +
                    "下の容量スライダーはすべて非表示になり、\n" +
                    "スライダーの値に関係なくゲームのバニラ値が使われます。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "郵便バンの積載量" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "各郵便バンが運べる郵便量を調整します。\n" +
                    "<100% = バニラの積載量>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "郵便バンの台数" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "各郵便施設が保有・出動できる郵便バンの台数を調整します。\n" +
                    "<100% = バニラの台数>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "郵便トラックの台数" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "各仕分け施設（および郵便トラックを持つ施設）が\n" +
                    "保有・出動できる郵便トラックの台数を調整します。\n" +
                    "<100% = バニラの台数>"
                },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "仕分け速度" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "**仕分け**施設の基本仕分け速度に掛ける倍率です。\n" +
                    "<100% = バニラ>。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "仕分け保管容量" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "**郵便の保管容量**を調整します。\n" +
                    "<100% = バニラ>。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "未仕分け郵便の不足を修正" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "有効にすると、未仕分け郵便が少なすぎるときに少量を自動で追加します。\n" +
                    "これで仕分け施設が動き続けやすくなります。\n" +
                    "カーゴ港があると仕分け施設に十分な郵便が届かない現在の不具合への一時的な対策です。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "未仕分け郵便のしきい値" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "未仕分け郵便が総保管容量のこの低い割合を下回ると、\n" +
                    "追加の未仕分け郵便を取り寄せます。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "未仕分け郵便の補充量" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "未仕分け郵便を取り寄せるときの追加量です（魔法の補充）。\n" +
                    "最大保管容量に対する割合です。\n" +
                    "バニラ <最大 = 250,000> で <10%> に設定した場合、<25,000> が追加されます。"
                },

                // ---- RESET BUTTONS ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "ゲームのデフォルト" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)), "すべての設定をゲーム本来のデフォルト動作（バニラ）に戻します。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "おすすめ" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "**かんたんスタート** – おすすめの郵便設定をまとめて適用します。\n" +
                    "ワンクリックで完了！"
                },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), string.Empty },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)), "直近のバックグラウンドスキャンで処理された郵便局、郵便バン、仕分け施設、郵便トラックの概要です。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "月間郵便" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "最近の都市全体の郵便フローを表示します。\n" +
                    "\n" +
                    "**累積** = 市民が生成した郵便量。\n" +
                    "**処理済み** = 郵便ネットワークが実際に処理した量。\n" +
                    "\n" +
                    "- 処理済みが累積より多いことが多ければ、郵便ネットワークの能力は十分です。\n" +
                    "- 累積が長時間ずっと処理済みを上回る場合、\n" +
                    "都市がネットワークの処理能力以上の郵便を生成しています。\n" +
                    "施設や郵便バンを増やすか、設定を調整してください。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "アクティビティ" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)), "直近の更新で行った郵便補充と、あふれクリーンアップの回数です。" },

                // ---- Status text templates (for MagicMailSystem) ----
                { "MM_STATUS_NO_FACILITIES", "まだ郵便施設が処理されていません。都市を開き、シミュレーションをしばらく動かしてください。" },

                { "MM_STATUS_NO_ACTIVITY", "まだアクティビティは記録されていません。" },

                { "MM_STATUS_SUMMARY", "{0} 件の郵便局 | {1} 台の郵便バン | {2} 件の仕分け施設 | {3} 台の郵便トラック" },

                { "MM_STATUS_ACTIVITY", "{0} 回のローカル郵便補充 | {1} 回の未仕分け郵便補充 | {2} 回のあふれクリーンアップ" },

                { "MM_STATUS_CITY_MAIL_NOT_READY", "都市の郵便統計はまだ利用できません。都市を開き、シミュレーションをしばらく動かしてください。" },

                { "MM_STATUS_CITY_MAIL", "{0} 累積 | {1} 処理済み" },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "このModの表示名です。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "バージョン" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "現在のModバージョンです。" },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)), "**Magic Mail** やその他のModの **Paradox** ページを開きます。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "ブラウザで **Discord** のフィードバックチャットを開きます。" },

            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
