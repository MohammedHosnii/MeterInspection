using System;
using System.Collections.Generic;

namespace Shared
{
    public class QueryData
    {
        public string? ReportId { get; set; }
        public string? ButtonId { get; set; }
        public string? SectorCode { get; set; }
        public string? DepartmentCode { get; set; }
        public string? RegionCode { get; set; }
        public string? DayCode { get; set; }
        public string? MainCode { get; set; }
        public string? FirstEsdarDate { get; set; }
    
 
        public string? MeterNo { get; set; }
 
 
        public string? DataItemId { get; set; }
 
        public string? MeterReadingCaseId { get; set; }
  
        public string? DateFieldName { get; set; }
        public string? DateFrom { get; set; }
        public string? DateTo { get; set; }
   
        public string Installation_date { get; set; }
       
     

       

 
        public bool IsFullRef( out string errors)
        {
            try
            {
                List<string> errorList = new List<string>();

                if (string.IsNullOrWhiteSpace(DepartmentCode))
                {
                    errorList.Add("كود الهندسة غير موجود");
                }
                if (string.IsNullOrWhiteSpace(RegionCode))
                {
                    errorList.Add("كود المنطقة غير موجود");
                }
                if (string.IsNullOrWhiteSpace(DayCode))
                {
                    errorList.Add("كود اليومية غير موجود");
                }
                if (string.IsNullOrWhiteSpace(MainCode))
                {
                    errorList.Add("كود الحساب غير موجود");
                }
                 

                if (errorList.Count == 0)
                {
                    errors = string.Empty;
                    return true;
                }
                else
                {
                    errors = string.Join(Environment.NewLine, errorList);
                    return false;
                }


               
            }
            catch (Exception ex)
            {
                errors = "Exception";
                return false;
            }
        }
    }
}
