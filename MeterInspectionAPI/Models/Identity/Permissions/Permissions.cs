using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MeterInspectionAPI.Models.Identity.Permissions
{

    public static class Permissions
    {
        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";

            /// public const string ViewAR = "عرض المستخدمين";
            //public const string CreateAR = "انشاء مستخدم جديد";
            //public const string EditAR = "تعديل المستخدمين";
            //public const string DeleteAR = "مسح مستخدم";
        }

        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
        }
        public static class CustBasicDataDTO
        {
            //public const string GetAll = "Permissions.CustBasicDataDTO.View";
            //public const string GetById = "Permissions.CustBasicDataDTO.View";
            public const string View = "Permissions.CustBasicDataDTO.View";
            public const string Add = "Permissions.CustBasicDataDTO.Add";
            public const string EsdarDoneApiResponse = "Permissions.CustBasicDataDTO.EsdarDoneApiResponse";
            public const string Update = "Permissions.CustBasicDataDTO.Update";
            public const string Delete = "Permissions.CustBasicDataDTO.Delete";

        }

        public static class CustBasicDataMeters
        {
            public const string Add = "Permissions.CustBasicDataMeters.Add";
            public const string View = "Permissions.CustBasicDataMeters.View";
            public const string Update = "Permissions.CustBasicDataMeters.Update";
            public const string Delete = "Permissions.CustBasicDataMeters.Delete";

        }
        public static class DvResult
        {
            public const string View = "Permissions.DvResult.View";
        }
        public static class Esdar
        {
            public const string ExecuteEsdar = "Permissions.Esdar.ExecuteEsdar";
            public const string CancelEsdar = "Permissions.Esdar.CancelEsdar";
            public const string ExecuteEndEsdarCycle = "Permissions.Esdar.ExecuteEndEsdarCycle";
            public const string ExecuteCancelEndEsdarCycle = "Permissions.Esdar.ExecuteCancelEndEsdarCycle";
        }

        public static class UploadFiles
        {
            public const string BulkInsertHesFile = "Permissions.UploadFiles.BulkInsertHesFile";
            public const string BulkInsertNewCustXls = "Permissions.UploadFiles.BulkInsertNewCustXls";
            public const string UploadHesFiles = "Permissions.UploadFiles.UploadHesFiles";
            public const string UploadNewCustExcel = "Permissions.UploadFiles.UploadNewCustExcel";
        }
        public static class HES
        {
            public const string View = "Permissions.HES.View";
            public const string SaveOnDemandHesMessage = "Permissions.HES.SaveOnDemandHesMessage";
            public const string SaveDailyMessage = "Permissions.HES.SaveDailyMessage";
            public const string SaveMonthlyMessage = "Permissions.HES.SaveDailyMessage";
            

        }
        public static class LK_Wizara
        {
            public const string Add = "Permissions.LK_Wizara.Add";
            public const string View = "Permissions.LK_Wizara.View";
            public const string Update = "Permissions.LK_Wizara.Update";
            public const string Delete = "Permissions.LK_Wizara.Delete";

        }
        public static class LK_District
        {
            public const string Add = "Permissions.LK_District.Add";
            public const string View = "Permissions.LK_District.View";
            public const string Update = "Permissions.LK_District.Update";
            public const string Delete = "Permissions.LK_District.Delete";

        }
        public static class Reports
        {
            public const string GetRptReadingList = "Permissions.Reports.GetRptReadingList";
            public const string GetEsdarMenuDtl = "Permissions.Reports.GetEsdarMenuDtl";
            public const string GetEsdarMenuTotal = "Permissions.Reports.GetEsdarMenuTotal";
            public const string GetCustBasicData = "Permissions.Reports.GetCustBasicData";
            public const string GetCustBasicData_New = "Permissions.Reports.GetCustBasicData_New";
            public const string GetCustBasicData_End = "Permissions.Reports.GetCustBasicData_End";
            public const string GetRptMissingMeters = "Permissions.Reports.GetRptMissingMeters";
            public const string GetRejectedMeters = "Permissions.Reports.GetRejectedMeters";
            public const string GetCustCard = "Permissions.Reports.GetCustCard";
            public const string GetDailyMetersRead = "Permissions.Reports.GetDailyMetersRead";
            public const string GetNewCustBasicExcel = "Permissions.Reports.GetNewCustBasicExcel";

        }

        public static IEnumerable<string> GetAll()
        {
            return typeof(Permissions)
                .GetNestedTypes(BindingFlags.Public)
                .SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.Static))
                .Where(f => f.FieldType == typeof(string))
                .Select(f => f.GetValue(null)!.ToString()!);
        }

    }
}
