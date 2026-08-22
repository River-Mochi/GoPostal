// <copyright file="LocaleZH_CN.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// LocaleZH_CN.cs
// Simplified Chinese locale zh-HANS

namespace MagicMail
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// Simplified Chinese localization source for Magic Mail [MM].</summary>
    public sealed class LocaleZH_CN : IDictionarySource
    {
        private readonly Setting m_Setting;

        /// <summary>
        /// Constructs the Simplified Chinese locale generator.</summary>
        /// <param name="setting">Settings object used for locale IDs.</param>
        public LocaleZH_CN(Setting setting)
        {
            m_Setting = setting;
        }

        /// <summary>
        /// Generates all Simplified Chinese localization entries for this mod.</summary>
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
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab), "状态" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab), "关于" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup), "邮政配送辅助" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup), "邮政面包车和卡车" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "邮件分拣中心" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup), "重置" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "城市扫描" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "最近更新" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup), "信息" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "链接" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "修复本地邮件不足" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "启用后，本地邮件过少时会自动补上一小部分。\n" +
                    "不会额外生成邮政车，就像一点小魔法……但确实有效 :)"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "本地邮件阈值" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "当本地邮件低于你设置的这个百分比时，\n" +
                    "邮局会补充更多本地邮件。\n" +
                    "该比例按建筑的最大存储容量计算。\n" +
                    "例如：<最大存储 = 100,000>、<阈值 = 5%> 时，\n" +
                    "本地邮件 < <5,000> 就会进行补充。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "本地邮件补充量" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "补充本地邮件时增加的百分比（魔法补货）。\n" +
                    "如果原版最大值 = <100,000>，这里设为 <10%>，\n" +
                    "需要时会增加 <10,000>。"
                },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)), "修复邮件溢出" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "邮件太多时，设施会进行一次小小的“魔法清理”。\n" +
                    "多余的存储邮件会视为已投递并移除。\n" +
                    "这样可以避免设施长期卡在满仓状态。\n" +
                    "关闭此项即可保持纯原版行为。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "邮局溢出阈值" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "当邮局内的邮件总量达到这个百分比时，模组会\n" +
                    "删除足够的邮件，让存量回到这个水平。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "分拣中心溢出阈值" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "当分拣中心内的邮件总量达到这个百分比时，模组会\n" +
                    "删除足够的邮件，让存量回到这个水平。"
                },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "修改运力" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "启用后可修改面包车和卡车的容量。关闭时，\n" +
                    "下方所有容量滑块都会隐藏，并且\n" +
                    "无论滑块之前设成多少，都会使用游戏原版数值。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "邮政面包车载量" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "控制每辆邮政面包车可以携带多少邮件。\n" +
                    "<100% = 原版载量>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "邮政面包车数量" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "控制每个邮政建筑可拥有和派出的邮政面包车数量。\n" +
                    "<100% = 原版车队规模>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "邮政卡车数量" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "控制每个分拣中心（以及任何拥有邮政卡车的设施）\n" +
                    "可拥有和派出的邮政卡车数量。\n" +
                    "<100% = 原版车队规模>"
                },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "分拣速度" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "**分拣**设施倍率，作用于设施的基础分拣速度。\n" +
                    "<100% = 原版>。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "分拣存储容量" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "控制**邮件存储容量**。\n" +
                    "<100% = 原版>。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "修复未分拣邮件不足" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "启用后，未分拣邮件库存过低时会自动补上一小部分。\n" +
                    "这样可让分拣设施继续工作。\n" +
                    "这是针对当前一个问题的临时修复：存在货运港口时，分拣设施可能收不到足够邮件。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "未分拣邮件阈值" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "当未分拣邮件低于总存储容量的这个较低百分比时，\n" +
                    "会额外补充一些未分拣邮件。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "未分拣邮件补充量" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "补充未分拣邮件时额外增加的数量（魔法补货）。\n" +
                    "数量按最大存储容量的百分比计算。\n" +
                    "如果原版 <最大 = 250,000> 且这里设为 <10%>，则增加 <25,000>。"
                },

                // ---- RESET BUTTONS ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "游戏默认值" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)), "将所有设置恢复为游戏原本的默认行为（原版）。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "推荐" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "**快速开始** – 一次应用所有推荐的邮政设置。\n" +
                    "简单模式：点一下就完成！"
                },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), string.Empty },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)), "上一次后台扫描中处理到的邮局、邮政面包车、分拣设施和邮政卡车摘要。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "每月邮件" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "显示最近的全市邮件流量。\n" +
                    "\n" +
                    "**累计** = 市民产生了多少邮件。\n" +
                    "**处理** = 邮政网络实际处理了多少邮件。\n" +
                    "\n" +
                    "- 如果“处理”经常高于“累计”，说明邮政网络容量足够。\n" +
                    "- 如果“累计”长期高于“处理”，\n" +
                    "说明城市产生的邮件超过了网络的处理能力。\n" +
                    "增加设施或邮政面包车，或者调整设置。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "活动" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)), "上次更新中执行的邮件补充和溢出清理次数。" },

                // ---- Status text templates (for MagicMailSystem) ----
                { "MM_STATUS_NO_FACILITIES", "尚未处理任何邮政设施。打开一座城市并让模拟运行一会儿。" },

                { "MM_STATUS_NO_ACTIVITY", "尚未记录任何活动。" },

                { "MM_STATUS_SUMMARY", "{0} 个邮局 | {1} 辆邮政面包车 | {2} 个分拣设施 | {3} 辆邮政卡车" },

                { "MM_STATUS_ACTIVITY", "{0} 次本地邮件补充 | {1} 次未分拣邮件补充 | {2} 次溢出清理" },

                { "MM_STATUS_CITY_MAIL_NOT_READY", "城市邮件统计尚不可用。打开一座城市并让模拟运行一会儿。" },

                { "MM_STATUS_CITY_MAIL", "{0} 累计 | {1} 处理" },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "模组" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "此模组的显示名称。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "版本" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "当前模组版本。" },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)), "打开 **Magic Mail** 和其他模组的 **Paradox** 页面。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "在浏览器中打开 **Discord** 反馈聊天。" },

            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
