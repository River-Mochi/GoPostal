// <copyright file="LocaleVI.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// LocaleVI.cs
// Vietnamese locale vi-VN

namespace MagicMail
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// Vietnamese localization source for Magic Mail [MM].</summary>
    public sealed class LocaleVI : IDictionarySource
    {
        private readonly Setting m_Setting;

        /// <summary>
        /// Constructs the Vietnamese locale generator.</summary>
        /// <param name="setting">Settings object used for locale IDs.</param>
        public LocaleVI(Setting setting)
        {
            m_Setting = setting;
        }

        /// <summary>
        /// Generates all Vietnamese localization entries for this mod.</summary>
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod title
                { m_Setting.GetSettingsLocaleID(), "Magic Mail + Postal Dispatch" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "Thao tác" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab), "Trạng thái" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab), "Giới thiệu" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup), "Hỗ trợ phát thư" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup), "Xe van & xe tải bưu điện" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "Cơ sở phân loại" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup), "Đặt lại" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "Quét thành phố" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "Cập nhật gần nhất" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup), "Thông tin" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "Liên kết" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "Sửa thiếu thư nội địa" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "Khi bật, một ít thư sẽ được thêm nếu lượng thư nội địa xuống quá thấp.\n" +
                    "Không tạo thêm xe van; hơi giống phép thuật... nhưng là thật :)"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "Ngưỡng thư nội địa" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "Nếu thư nội địa xuống dưới tỷ lệ bạn chọn,\n" +
                    "bưu điện sẽ lấy thêm thư nội địa.\n" +
                    "Đây là tỷ lệ phần trăm của sức chứa tối đa của tòa nhà.\n" +
                    "Ví dụ: <sức chứa tối đa = 100.000> và <ngưỡng = 5%>,\n" +
                    "khi thư nội địa < <5.000>, hệ thống sẽ lấy thêm thư."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "Lượng thư nội địa lấy thêm" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "Tỷ lệ được thêm khi lấy thư nội địa (bù thư bằng phép thuật).\n" +
                    "Nếu mức tối đa vanilla = <100.000> và đặt <10%>,\n" +
                    "thì <10.000> sẽ được thêm khi cần."
                },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)), "Sửa tràn kho thư" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "Khi có quá nhiều thư, cơ sở sẽ dọn bớt một chút bằng 'phép thuật'.\n" +
                    "Phần thư dư được xem như đã giao và bị xóa.\n" +
                    "Việc này giúp cơ sở không bị kẹt ở trạng thái đầy mãi.\n" +
                    "Tắt nếu bạn muốn giữ nguyên hành vi vanilla."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "Ngưỡng tràn của bưu điện" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "Khi tổng thư trong bưu điện đạt tỷ lệ này, mod sẽ\n" +
                    "xóa vừa đủ thư để đưa mức lưu trữ về ngưỡng này."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "Ngưỡng tràn của cơ sở phân loại" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "Khi tổng thư trong cơ sở phân loại đạt tỷ lệ này, mod sẽ\n" +
                    "xóa vừa đủ thư để đưa mức lưu trữ về ngưỡng này."
                },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "Thay đổi sức chứa" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "Bật để thay đổi sức chứa của xe van và xe tải. Khi tắt,\n" +
                    "tất cả thanh chỉnh sức chứa bên dưới sẽ bị ẩn và\n" +
                    "giá trị vanilla của game được dùng dù bạn để thanh chỉnh ở mức khác."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "Tải thư của xe van" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "Điều chỉnh lượng thư mỗi xe van bưu điện có thể chở.\n" +
                    "<100% = tải vanilla.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "Số xe van bưu điện" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "Điều chỉnh số xe van mà mỗi tòa nhà bưu điện có thể sở hữu và điều đi.\n" +
                    "<100% = số xe vanilla.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "Số xe tải bưu điện" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "Điều chỉnh số xe tải bưu điện mà mỗi cơ sở phân loại (và cơ sở nào có xe tải bưu điện)\n" +
                    "có thể sở hữu và điều đi.\n" +
                    "<100% = số xe vanilla.>"
                },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "Tốc độ phân loại" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "Hệ số cho cơ sở **phân loại**. Áp dụng lên tốc độ phân loại cơ bản.\n" +
                    "<100% = vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "Sức chứa kho phân loại" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "Điều chỉnh **kho chứa thư**.\n" +
                    "<100% = vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "Sửa thiếu thư chưa phân loại" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "Khi bật, một ít thư chưa phân loại sẽ xuất hiện nếu lượng trong kho xuống quá thấp.\n" +
                    "Việc này giúp cơ sở phân loại tiếp tục hoạt động.\n" +
                    "Đây là cách khắc phục tạm thời cho lỗi hiện tại khiến cơ sở phân loại không nhận đủ thư khi có cảng hàng hóa."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "Ngưỡng thư chưa phân loại" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "Nếu thư chưa phân loại xuống dưới tỷ lệ nhỏ này của tổng sức chứa,\n" +
                    "hệ thống sẽ lấy thêm một ít thư chưa phân loại."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "Lượng thư chưa phân loại lấy thêm" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "Lượng thư thêm khi lấy thư chưa phân loại (bù thư bằng phép thuật).\n" +
                    "Lượng này là tỷ lệ phần trăm của sức chứa tối đa.\n" +
                    "Nếu vanilla <tối đa = 250.000> và đặt <10%>, thì <25.000> sẽ được thêm."
                },

                // ---- RESET BUTTONS ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "Mặc định của game" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)), "Khôi phục toàn bộ cài đặt về hành vi mặc định gốc của game (vanilla)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "Khuyến nghị" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "**Bắt đầu nhanh** – áp dụng toàn bộ cài đặt bưu điện được khuyến nghị.\n" +
                    "Chế độ dễ: 1 cú nhấp là xong!"
                },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), string.Empty },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)), "Tóm tắt bưu điện, xe van, cơ sở phân loại và xe tải được xử lý trong lần quét nền gần nhất." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "Thư hàng tháng" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "Hiển thị luồng thư gần đây trên toàn thành phố.\n" +
                    "\n" +
                    "**Tích lũy** = lượng thư cư dân tạo ra.\n" +
                    "**Đã xử lý** = lượng thư mạng lưới thực sự xử lý được.\n" +
                    "\n" +
                    "- Nếu Đã xử lý thường cao hơn Tích lũy, mạng bưu điện có đủ năng lực.\n" +
                    "- Nếu Tích lũy giữ cao hơn Đã xử lý trong thời gian dài,\n" +
                    "thành phố đang tạo nhiều thư hơn mức mạng lưới có thể xử lý.\n" +
                    "Hãy thêm cơ sở hoặc xe van, hoặc chỉnh lại cài đặt."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "Hoạt động" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)), "Số lần bù thư và dọn tràn kho được thực hiện trong lần cập nhật gần nhất." },

                // ---- Status text templates (for MagicMailSystem) ----
                { "MM_STATUS_NO_FACILITIES", "Chưa có cơ sở bưu điện nào được xử lý. Hãy mở thành phố và để mô phỏng chạy một lúc." },

                { "MM_STATUS_NO_ACTIVITY", "Chưa ghi nhận hoạt động nào." },

                { "MM_STATUS_SUMMARY", "{0} bưu điện | {1} xe van | {2} cơ sở phân loại | {3} xe tải" },

                { "MM_STATUS_ACTIVITY", "{0} lần bù thư nội địa | {1} lần bù thư chưa phân loại | {2} lần dọn tràn" },

                { "MM_STATUS_CITY_MAIL_NOT_READY", "Chưa có thống kê thư của thành phố. Hãy mở thành phố và để mô phỏng chạy một lúc." },

                { "MM_STATUS_CITY_MAIL", "{0} tích lũy | {1} đã xử lý" },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "Tên hiển thị của mod này." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Phiên bản" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "Phiên bản hiện tại của mod." },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)), "Mở trang **Paradox** của **Magic Mail** và các mod khác." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Mở trò chuyện phản hồi **Discord** trong trình duyệt." },

            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
