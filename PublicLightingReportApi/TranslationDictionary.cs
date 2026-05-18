using System;
using System.Collections.Generic;

namespace PublicLightingReportApi
{
    static class TranslationDictionary
    {
        // Static dictionary with English-Arabic translations (case-insensitive)
        private static readonly Dictionary<string, string> Translations = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "CustomerCode", "كود المشترك" },
        { "Sector_code", "كود القطاع" },
        { "Sector_name", "اسم القطاع" },
        { "Department_code", "كود الادارة" },
        { "Department_name", "اسم الادارة" },
        { "District_code", "كود الحى" },
        { "District_name", "اسم الحى" },
        { "Region_code", "المنطقة" },
        { "Day_code", "اليومية" },
        { "Main_code", "الحساب" },
        { "Branch_code", "الفرعى" },
        { "Customer_name", "اسم المشترك" },
        { "Customer_address", "عنوان المشترك" },
        { "IsEnd", "منتهى" },
        { "CustomerType_name", "نوع المشترك" },
        { "Meter_number", "رقم العداد" },
        { "First_Esdar_date", "تاريخ اول اصدار" },
        { "Installation_date", "تاريخ تركيب العداد" },
        { "ReadFactor","معامل القراءة" },
        { "MeterPrvID","كود القراءة السابقة" },
        { "MeterCurID","كود القراءة الحالية" },
        { "CurRdg","القراءة الحالية" },
        { "PrvRdg","القراءة السابقة" },
        { "Consum","الاستهلاك" },
        { "ConsumPrv","الاستهلاك السابق" },
        { "ConsumAvg","متوسط الاستهلاك" },
        { "ReadingSource","مصدر القراءة" },
        {"Wizara_code","كود الحى" },
        {"Wizara_name","اسم الحى" },
        {"FirstCurrentReadingKW","اول قراءة حالية" },
        {"FirstPreviousReadingKW","اول قراءة سابقة" },
        {"FirstConsumptionKW","اول استهلاك" },
        {"ConsumptionAVG","متوسط استهلاك" },
        {"RejectedReason","سبب الرفض" }

               
    



    };

        // Method to get the Arabic translation or return the original key if not found
        public static string Ara(string key)
        {
            return Translations.TryGetValue(key, out string value) ? value : key;
        }
    }

}