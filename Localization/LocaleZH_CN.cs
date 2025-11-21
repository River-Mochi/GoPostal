// Localization/LocaleZH_CN.cs
// Simplified Chinese locale zh-HANS

namespace MagicPostMaster
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// Simplified Chinese localization source for Magic PostMaster [MPM].</summary>
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
                { m_Setting.GetSettingsLocaleID(), "Magic PostMaster [MPM]" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "操作" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab),  "状态" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab),   "关于" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup),          "邮局" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup),             "邮车与卡车" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "分拣中心" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup),               "重置" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "城市总览" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "最近一次更新" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup),  "信息" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "链接" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "修复本地邮件不足" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "启用后，如果本地邮件库存过低，会有额外的本地邮件“魔法般”出现在邮局中。\n " +
                    "不会生成额外的邮车——邮件直接出现在建筑里供投递使用。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "本地邮件阈值" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "当本地邮件低于你设置的这个百分比时，邮局会自动拉取更多本地邮件。\n" +
                    "这是相对于最大存储容量的百分比。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "本地邮件获取量" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "进行本地邮件“魔法补充”时要增加的百分比。\n" +
                    "20% = 在原版最大容量 10,000 时增加 2,000。" },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)),
                    "修复邮件溢出" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "启用后，当达到设定的溢出阈值时，邮局和/或分拣中心会进行一次小型“魔法”清理：\n " +
                    "多余的存储邮件会被视为已投递并直接移除。\n " +
                    "这样可以防止建筑长时间卡在“已满”的状态。\n " +
                    "如果想保持原版行为，请关闭此选项。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "邮局溢出阈值" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "当邮局的邮件总量达到存储容量的这个百分比时，会触发溢出处理。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "分拣中心溢出阈值" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "当分拣中心的邮件总量达到总存储容量的这个百分比时，\n " +
                    "会触发“魔法”溢出处理。" },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "修改运力" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "启用后可以修改邮车和卡车的容量。关闭时，下方所有容量滑条都会隐藏，\n " +
                    "即使数值被改动，实际仍使用游戏原版的默认数值。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "邮车邮件载量" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "控制每辆邮车可携带的邮件量。100% = 原版载量。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "邮车车队规模" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "控制每个邮政建筑可以拥有并派出的邮车数量。\n" +
                    "<100% = 原版车队规模。>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "邮政卡车车队规模" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "控制每个分拣中心（以及任何拥有邮政卡车的建筑）\n " +
                    "可以拥有并派出的邮政卡车数量。\n " +
                    "<100% = 原版车队规模。>" },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "分拣速度" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "用于邮政**分拣中心**的分拣速度倍率。作用于建筑的基础分拣率。\n " +
                    "<100% = 原版>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "分拣存储容量" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "控制分拣中心的**邮件存储容量**。\n " +
                    "<100% = 原版>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "修复未分拣邮件不足" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "启用后，如果分拣中心的未分拣邮件存量过低，会有额外的未分拣邮件“魔法般”出现。\n " +
                    "这样可以让分拣过程保持运转，而不用等待更多运送。\n" +
                    "这是对当前 Bug 的临时修复：在有货运港口时，分拣中心可能得不到足够的邮件。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "未分拣邮件阈值" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "当未分拣邮件低于总容量的这个百分比时，建筑会“魔法般”拉取设定数量的未分拣邮件。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "未分拣邮件获取量" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "进行未分拣邮件“魔法补充”时要增加的邮件数量。\n" +
                    "相对于最大存储容量的百分比。" },

                // ---- Reset ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "重置为游戏默认" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)),
                    "将所有邮政设置恢复为游戏原始（原版）行为。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "重置为推荐设置" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "快速开始：应用推荐的邮政设置。" },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), "" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)),
                    "显示在上一次游戏更新中处理的邮局、邮车、分拣中心和邮政卡车数量（约每 45 分钟游戏时间一次）。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "城市邮件" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "显示全城最近的邮件流量。\n\n" +
                     "生成邮件 = 市民在一段时间内产生的邮件总量。\n" +
                     "处理邮件   = 邮政网络实际处理的邮件总量。\n\n" +
                     "- 如果“处理”长期高于“生成”，说明邮政网络有足够的容量，" +
                     "可以视情况减少预算或车辆数量。\n" +
                     "- 如果“生成”长时间高于“处理”，说明城市产生的邮件超过了网络的处理能力，\n " +
                     "需要增加设施、车辆或调整设置。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "活动" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)),
                    "显示在上一次更新中进行的邮件补充和溢出清理次数。" },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "模组" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)),
                    "此模组的显示名称。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "版本" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)),
                    "当前模组版本。" },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)),
                    "打开 **Paradox** 网站上的 **Magic PostMaster** 及其他模组页面。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)),
                    "在浏览器中打开 **Discord** 反馈聊天频道。" },
            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
