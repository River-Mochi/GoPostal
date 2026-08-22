// <copyright file="LocaleKO.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// LocaleKO.cs
// Korean locale ko-KR

namespace MagicMail
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// Korean localization source for Magic Mail [MM].</summary>
    public sealed class LocaleKO : IDictionarySource
    {
        private readonly Setting m_Setting;

        /// <summary>
        /// Constructs the Korean locale generator.</summary>
        /// <param name="setting">Settings object used for locale IDs.</param>
        public LocaleKO(Setting setting)
        {
            m_Setting = setting;
        }

        /// <summary>
        /// Generates all Korean localization entries for this mod.</summary>
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod title
                { m_Setting.GetSettingsLocaleID(), "Magic Mail + Postal Dispatch" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "작업" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab), "상태" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab), "정보" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup), "우편 배송 도우미" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup), "우편 밴 & 트럭" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "우편 분류 시설" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup), "초기화" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "도시 스캔" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "최근 업데이트" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup), "정보" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "링크" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "로컬 우편 부족 보완" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "활성화하면 로컬 우편이 너무 적을 때 소량의 우편이 자동으로 추가됩니다.\n" +
                    "밴을 더 생성하는 건 아니고, 약간 마법처럼... 하지만 진짜예요 :)"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "로컬 우편 기준" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "로컬 우편이 선택한 비율 아래로 내려가면\n" +
                    "우체국이 로컬 우편을 더 가져옵니다.\n" +
                    "건물 최대 저장량을 기준으로 한 비율입니다.\n" +
                    "예: <최대 저장량 = 100,000>, <기준 = 5%>일 때\n" +
                    "로컬 우편이 <5,000> 미만이면 우편을 더 가져옵니다."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "로컬 우편 보충량" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "로컬 우편을 가져올 때 추가할 비율입니다(마법 보충).\n" +
                    "바닐라 최대치가 <100,000>이고 <10%>로 설정했다면\n" +
                    "필요할 때 <10,000>이 추가됩니다."
                },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)), "우편 넘침 수정" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "우편이 너무 많으면 시설에서 소량을 마법처럼 정리합니다.\n" +
                    "초과 저장 우편은 배달된 것으로 처리하고 제거합니다.\n" +
                    "시설이 계속 가득 찬 채 멈추는 것을 막아줍니다.\n" +
                    "완전한 바닐라 동작을 원하면 끄세요."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "우체국 넘침 기준" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "우체국의 전체 우편이 이 비율에 도달하면 모드가\n" +
                    "저장된 우편을 이 수준까지 줄입니다."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "분류 시설 넘침 기준" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "분류 시설의 전체 우편이 이 비율에 도달하면 모드가\n" +
                    "저장된 우편을 이 수준까지 줄입니다."
                },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "용량 변경" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "밴과 트럭 용량을 바꾸려면 켜세요. 끄면\n" +
                    "아래 용량 슬라이더가 모두 숨겨지고\n" +
                    "슬라이더 값과 상관없이 바닐라 게임 값이 사용됩니다."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "우편 밴 적재량" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "우편 밴 한 대가 실을 수 있는 우편량을 조절합니다.\n" +
                    "<100% = 바닐라 적재량>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "우편 밴 보유 대수" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "각 우편 건물이 보유하고 출동시킬 수 있는 우편 밴 수를 조절합니다.\n" +
                    "<100% = 바닐라 보유 대수>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "우편 트럭 보유 대수" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "각 분류 시설(및 우편 트럭이 있는 시설)이 보유하고\n" +
                    "출동시킬 수 있는 우편 트럭 수를 조절합니다.\n" +
                    "<100% = 바닐라 보유 대수>"
                },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "분류 속도" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "**분류** 시설의 기본 분류 속도에 적용되는 배율입니다.\n" +
                    "<100% = 바닐라>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "분류 시설 저장 용량" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "**우편 저장 용량**을 조절합니다.\n" +
                    "<100% = 바닐라>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "미분류 우편 부족 보완" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "활성화하면 미분류 우편이 너무 적을 때 소량이 자동으로 추가됩니다.\n" +
                    "분류 시설이 계속 일할 수 있게 해 줍니다.\n" +
                    "화물 항구가 있으면 분류 시설에 우편이 충분히 들어오지 않는 현재 버그를 위한 임시 해결책입니다."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "미분류 우편 기준" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "미분류 우편이 전체 저장 용량의 이 낮은 비율 아래로 내려가면\n" +
                    "미분류 우편을 조금 더 가져옵니다."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "미분류 우편 보충량" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "미분류 우편을 가져올 때 추가하는 양입니다(마법 보충).\n" +
                    "최대 저장 용량에 대한 비율입니다.\n" +
                    "바닐라 <최대 = 250,000>이고 <10%>라면 <25,000>이 추가됩니다."
                },

                // ---- RESET BUTTONS ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "게임 기본값" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)), "모든 설정을 게임 원래 기본 동작(바닐라)으로 되돌립니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "추천" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "**빠른 시작** – 추천 우편 설정을 한 번에 적용합니다.\n" +
                    "쉬운 모드: 한 번 클릭하면 끝!"
                },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), string.Empty },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)), "마지막 백그라운드 스캔에서 확인한 우체국, 우편 밴, 분류 시설, 우편 트럭 요약입니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "월간 우편" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "최근 도시 전체의 우편 흐름을 보여 줍니다.\n" +
                    "\n" +
                    "**누적** = 시민이 만든 우편량.\n" +
                    "**처리** = 우편망이 실제로 처리한 우편량.\n" +
                    "\n" +
                    "- 처리가 누적보다 자주 높으면 우편망 처리 능력이 충분합니다.\n" +
                    "- 누적이 오랫동안 처리보다 높게 유지되면\n" +
                    "도시에서 처리할 수 있는 양보다 우편을 더 많이 만들고 있다는 뜻입니다.\n" +
                    "시설이나 밴을 더 추가하거나 설정을 조정하세요."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "활동" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)), "마지막 업데이트에서 실행된 우편 보충과 넘침 정리 횟수입니다." },

                // ---- Status text templates (for MagicMailSystem) ----
                { "MM_STATUS_NO_FACILITIES", "아직 처리된 우편 시설이 없습니다. 도시를 열고 시뮬레이션을 잠시 실행하세요." },

                { "MM_STATUS_NO_ACTIVITY", "아직 기록된 활동이 없습니다." },

                { "MM_STATUS_SUMMARY", "우체국 {0} | 우편 밴 {1} | 분류 시설 {2} | 우편 트럭 {3}" },

                { "MM_STATUS_ACTIVITY", "로컬 우편 보충 {0} | 미분류 우편 보충 {1} | 넘침 정리 {2}" },

                { "MM_STATUS_CITY_MAIL_NOT_READY", "도시 우편 통계가 아직 준비되지 않았습니다. 도시를 열고 시뮬레이션을 잠시 실행하세요." },

                { "MM_STATUS_CITY_MAIL", "누적 {0} | 처리 {1}" },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "모드" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "이 모드의 표시 이름입니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "버전" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "현재 모드 버전입니다." },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)), "**Magic Mail**과 다른 모드의 **Paradox** 페이지를 엽니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "브라우저에서 **Discord** 피드백 채팅을 엽니다." },

            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
