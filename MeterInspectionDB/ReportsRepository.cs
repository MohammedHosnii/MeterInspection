using Dapper;
using Newtonsoft.Json.Linq;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterInspectionDB
{
    public class ReportsRepository
    {
        private readonly IDbConnection _db;
        private readonly string MDb;
        public ReportsRepository(IDbConnection db)
        {
            _db = db;
           
           // MDb = GPI.GetConfig().DbName();
        }
        public ReportInput GetMaintenanceRecord(string MaintenanceRecord)
        {

            ReportInput input;

            try
            {
                if (_db.State != System.Data.ConnectionState.Open)
                {
                    _db.Open();
                }
               
                var sql = $@"SELECT
                    MR.MaintenanceRecordDate,

                    CSD.CompanySectorDeptName AS CompanySectorDept,

                    dbo.getCompanyName(MR.CompanySectorDept_Id) AS CompanyName,

                    LC.LabCenterName AS LabCenter,

                    MR.MeterCount,
                    MR.WorkingMetersCount,
                    MR.RepairedMetersCount,
                    MR.RetiredMetersCount,
                    MR.MaintenanceRecordCode,
                    MR.ISSync,

                    CASE MR.CompanySectorDept_Level
                        WHEN 1 THEN N'شركة'
                        WHEN 2 THEN N'قطاع'
                        WHEN 3 THEN N'هندسة'
                        ELSE N'غير محدد'
                    END AS CompanySectorDept_Level,

                    U.UserName AS [User],

                    MRD.MeterNumber,

                    TR.TestResultName AS TestResult,

                    CA.CorrectiveActionName AS CorrectiveAction,

                    MT.MeterTypeName AS MeterType

                FROM dbo.MaintenanceRecord AS MR

                INNER JOIN dbo.MaintenanceRecord_Detail AS MRD
                    ON MR.Id = MRD.MaintenanceRecord_id

                INNER JOIN dbo.CompanySectorDept AS CSD
                    ON MR.CompanySectorDept_Id = CSD.Id

                INNER JOIN dbo.LabCenter AS LC
                    ON MR.LabCenter_Id = LC.Id

                INNER JOIN dbo.Users AS U
                    ON MR.UserId = U.Id

                INNER JOIN dbo.TestResult AS TR
                    ON MRD.TestResultCode = TR.TestResultCode

                INNER JOIN dbo.CorrectiveAction AS CA
                    ON MRD.CorrectiveActionCode = CA.CorrectiveActionCode

                INNER JOIN dbo.MeterTypes AS MT
                    ON MRD.MeterType_id = MT.Id
                     where MR.MaintenanceRecordCode={MaintenanceRecord}
                  
				    
				    FOR JSON PATH ;"
                ;


                IEnumerable<string> Data = _db.Query<string>(sql);

                int count = calculateCount(Data);

                string DataStr = string.Join("", Data);
                // 

                input = new ReportInput()
                {
                    ReportArgs = $"[]",
                    ReportData = DataStr,
                    ReportName = "MaintenanceRecord"
                };

            }
            catch (System.Exception ex)
            {

                return null;
            }

            return input;
        }

        private static int calculateCount(IEnumerable<string> Data)
        {
            if (Data.Count() == 0)
            {
                return 0;
            }

            JArray jsonArray = JArray.Parse(string.Join("", Data));


            int count = jsonArray.Count;
            return count;
        }

    }
}
