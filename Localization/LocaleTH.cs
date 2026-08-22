// <copyright file="LocaleTH.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// LocaleTH.cs
// Thai locale th-TH

namespace MagicMail
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// Thai localization source for Magic Mail [MM].</summary>
    public sealed class LocaleTH : IDictionarySource
    {
        private readonly Setting m_Setting;

        /// <summary>
        /// Constructs the Thai locale generator.</summary>
        /// <param name="setting">Settings object used for locale IDs.</param>
        public LocaleTH(Setting setting)
        {
            m_Setting = setting;
        }

        /// <summary>
        /// Generates all Thai localization entries for this mod.</summary>
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod title
                { m_Setting.GetSettingsLocaleID(), "Magic Mail + Postal Dispatch" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "การทำงาน" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab), "สถานะ" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab), "เกี่ยวกับ" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup), "ตัวช่วยจัดส่งไปรษณีย์" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup), "รถตู้และรถบรรทุกไปรษณีย์" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "ศูนย์คัดแยก" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup), "รีเซ็ต" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "สแกนเมือง" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "อัปเดตล่าสุด" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup), "ข้อมูล" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "ลิงก์" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "แก้จดหมายในพื้นที่ต่ำ" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "ถ้าเปิดไว้ จะเพิ่มจดหมายเล็กน้อยเมื่อปริมาณจดหมายในพื้นที่เหลือน้อยเกินไป\n" +
                    "ไม่สร้างรถตู้เพิ่ม แค่เหมือนมีเวทมนตร์นิดหน่อย...แต่ใช้ได้จริง :)"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "เกณฑ์จดหมายในพื้นที่" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "ถ้าจดหมายในพื้นที่ต่ำกว่าเปอร์เซ็นต์ที่คุณเลือก\n" +
                    "ไปรษณีย์จะดึงจดหมายในพื้นที่เข้ามาเพิ่ม\n" +
                    "ค่านี้เป็นเปอร์เซ็นต์ของความจุสูงสุดของอาคาร\n" +
                    "เช่น <ความจุสูงสุด = 100,000> และ <เกณฑ์ = 5%>\n" +
                    "เมื่อจดหมายในพื้นที่ < <5,000> จะดึงจดหมายเพิ่ม"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "ปริมาณจดหมายในพื้นที่ที่เติม" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "เปอร์เซ็นต์ที่เติมเมื่อดึงจดหมายในพื้นที่ (เติมแบบเวทมนตร์)\n" +
                    "ถ้าค่าสูงสุดแบบ vanilla = <100,000> และตั้งไว้ที่ <10%>\n" +
                    "จะเพิ่ม <10,000> เมื่อจำเป็น"
                },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)), "แก้จดหมายล้นคลัง" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "เมื่อมีจดหมายมากเกินไป อาคารจะทำความสะอาดแบบเวทมนตร์เล็กน้อย\n" +
                    "จดหมายส่วนเกินจะถือว่าส่งแล้วและถูกลบออก\n" +
                    "ช่วยไม่ให้อาคารค้างเพราะคลังเต็มตลอดเวลา\n" +
                    "ปิดตัวเลือกนี้ถ้าต้องการพฤติกรรม vanilla ล้วน ๆ"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "เกณฑ์ล้นคลังของไปรษณีย์" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "เมื่อจดหมายรวมในไปรษณีย์ถึงเปอร์เซ็นต์นี้ ม็อดจะ\n" +
                    "ลบจดหมายส่วนเกินจนกลับมาที่ระดับนี้"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "เกณฑ์ล้นคลังของศูนย์คัดแยก" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "เมื่อจดหมายรวมในศูนย์คัดแยกถึงเปอร์เซ็นต์นี้ ม็อดจะ\n" +
                    "ลบจดหมายส่วนเกินจนกลับมาที่ระดับนี้"
                },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "เปลี่ยนความจุ" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "เปิดเพื่อปรับความจุของรถตู้และรถบรรทุก ถ้าปิด\n" +
                    "สไลเดอร์ความจุด้านล่างทั้งหมดจะถูกซ่อน และ\n" +
                    "เกมจะใช้ค่า vanilla แม้ว่าสไลเดอร์จะค้างอยู่ที่ค่าอื่น"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "จดหมายต่อรถตู้" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "ควบคุมว่ารถตู้ไปรษณีย์แต่ละคันบรรทุกจดหมายได้เท่าไร\n" +
                    "<100% = น้ำหนักบรรทุก vanilla>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "จำนวนรถตู้ไปรษณีย์" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "ควบคุมจำนวนรถตู้ที่อาคารไปรษณีย์แต่ละแห่งมีและส่งออกวิ่งได้\n" +
                    "<100% = จำนวนรถ vanilla>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "จำนวนรถบรรทุกไปรษณีย์" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "ควบคุมจำนวนรถบรรทุกไปรษณีย์ที่ศูนย์คัดแยก (และอาคารที่มีรถบรรทุกไปรษณีย์)\n" +
                    "มีและส่งออกวิ่งได้\n" +
                    "<100% = จำนวนรถ vanilla>"
                },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "ความเร็วคัดแยก" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "ตัวคูณสำหรับศูนย์ **คัดแยก** ใช้กับอัตราคัดแยกพื้นฐานของอาคาร\n" +
                    "<100% = vanilla>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "ความจุเก็บจดหมาย" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "ควบคุม **พื้นที่เก็บจดหมาย**\n" +
                    "<100% = vanilla>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "แก้จดหมายยังไม่คัดต่ำ" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "ถ้าเปิดไว้ จะเพิ่มจดหมายที่ยังไม่คัดเล็กน้อยเมื่อของในคลังเหลือน้อยเกินไป\n" +
                    "ช่วยให้ศูนย์คัดแยกทำงานต่อได้\n" +
                    "เป็นวิธีแก้ชั่วคราวสำหรับบั๊กปัจจุบันที่ศูนย์คัดแยกได้จดหมายไม่พอเมื่อมีท่าเรือสินค้า"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "เกณฑ์จดหมายยังไม่คัด" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "ถ้าจดหมายยังไม่คัดต่ำกว่าเปอร์เซ็นต์เล็ก ๆ นี้ของความจุรวม\n" +
                    "จะดึงจดหมายยังไม่คัดเข้ามาเพิ่ม"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "ปริมาณจดหมายยังไม่คัดที่เติม" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "ปริมาณจดหมายที่เพิ่มเมื่อดึงจดหมายยังไม่คัด (เติมแบบเวทมนตร์)\n" +
                    "คิดเป็นเปอร์เซ็นต์ของความจุสูงสุด\n" +
                    "ถ้า vanilla <สูงสุด = 250,000> และตั้งไว้ที่ <10%> จะเพิ่ม <25,000>"
                },

                // ---- RESET BUTTONS ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "ค่าเริ่มต้นของเกม" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)), "คืนค่าทั้งหมดกลับเป็นพฤติกรรมมาตรฐานของเกม (vanilla)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "ค่าที่แนะนำ" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "**เริ่มแบบเร็ว** – ใช้การตั้งค่าไปรษณีย์ที่แนะนำทั้งหมด\n" +
                    "โหมดง่าย: คลิกครั้งเดียวแล้วจบ!"
                },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), string.Empty },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)), "สรุปไปรษณีย์ รถตู้ ศูนย์คัดแยก และรถบรรทุกจากการสแกนเบื้องหลังครั้งล่าสุด" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "จดหมายรายเดือน" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "แสดงการไหลของจดหมายทั้งเมืองช่วงล่าสุด\n" +
                    "\n" +
                    "**สะสม** = จดหมายที่ชาวเมืองสร้างขึ้น\n" +
                    "**ประมวลผล** = จดหมายที่เครือข่ายจัดการได้จริง\n" +
                    "\n" +
                    "- ถ้า ประมวลผล สูงกว่า สะสม บ่อย ๆ แสดงว่าเครือข่ายไปรษณีย์มีความจุพอ\n" +
                    "- ถ้า สะสม สูงกว่า ประมวลผล เป็นเวลานาน\n" +
                    "แสดงว่าเมืองสร้างจดหมายมากกว่าที่เครือข่ายจะจัดการได้\n" +
                    "เพิ่มอาคารหรือรถตู้ หรือปรับการตั้งค่า"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "กิจกรรม" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)), "จำนวนครั้งที่เติมจดหมายและทำความสะอาดคลังล้นในการอัปเดตล่าสุด" },

                // ---- Status text templates (for MagicMailSystem) ----
                { "MM_STATUS_NO_FACILITIES", "ยังไม่มีอาคารไปรษณีย์ที่ถูกประมวลผล เปิดเมืองแล้วปล่อยให้ซิมูเลชันทำงานสักพัก" },

                { "MM_STATUS_NO_ACTIVITY", "ยังไม่มีกิจกรรมที่บันทึกไว้" },

                { "MM_STATUS_SUMMARY", "ไปรษณีย์ {0} | รถตู้ {1} | ศูนย์คัดแยก {2} | รถบรรทุก {3}" },

                { "MM_STATUS_ACTIVITY", "เติมจดหมายในพื้นที่ {0} | เติมจดหมายยังไม่คัด {1} | ล้างคลังล้น {2}" },

                { "MM_STATUS_CITY_MAIL_NOT_READY", "สถิติจดหมายของเมืองยังไม่พร้อม เปิดเมืองแล้วปล่อยให้ซิมูเลชันทำงานสักพัก" },

                { "MM_STATUS_CITY_MAIL", "สะสม {0} | ประมวลผล {1}" },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "ม็อด" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "ชื่อที่แสดงของม็อดนี้" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "เวอร์ชัน" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "เวอร์ชันปัจจุบันของม็อด" },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)), "เปิดหน้า **Paradox** ของ **Magic Mail** และม็อดอื่น ๆ" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "เปิดแชตฟีดแบ็ก **Discord** ในเบราว์เซอร์" },

            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
