using System;
using System.Collections.Generic;

namespace Shared
{
    public class QueryDataParser
    {
        protected readonly QueryData queryData;
        protected List<string> querystrings = new List<string>();
        public QueryDataParser(QueryData queryData)
        {
            this.queryData = queryData;
        }

        public string Parse()
        {
            if (!string.IsNullOrWhiteSpace(queryData.SectorCode))
            {
                querystrings.Add($"[Sector_code] =  {queryData.SectorCode.Trim()} ");
            }

            if (!string.IsNullOrWhiteSpace(queryData.DepartmentCode))
            {
                querystrings.Add($"[Department_code] =  {queryData.DepartmentCode.Trim()} ");
            }

            if (!string.IsNullOrWhiteSpace(queryData.RegionCode))
            {
                querystrings.Add($"[Region_code] = {queryData.RegionCode.Trim()} ");
            }

            if (!string.IsNullOrWhiteSpace(queryData.DayCode))
            {
                querystrings.Add($"[Day_code] = {queryData.DayCode.Trim()}  ");
            }

            if (!string.IsNullOrWhiteSpace(queryData.MainCode))
            {
                querystrings.Add($"[Main_code] =  {queryData.MainCode.Trim()} ");
            }

            if (!string.IsNullOrWhiteSpace(queryData.FirstEsdarDate))
            {
                querystrings.Add($"[First_esdar_date] =  N'{queryData.FirstEsdarDate.Trim()}' ");
            }

          

            if (!string.IsNullOrWhiteSpace(queryData.MeterNo))
            {
                querystrings.Add($"[Meter_number] =  {queryData.MeterNo.Trim()}  ");
            }

            

            if (!string.IsNullOrWhiteSpace(queryData.DataItemId))
            {
                querystrings.Add($"[DataItemId] in ( {queryData.DataItemId} ) ");
            }

           

            if (!string.IsNullOrWhiteSpace(queryData.MeterReadingCaseId))
            {
                querystrings.Add($" MeterReadingCases.Id in ( {queryData.MeterReadingCaseId} ) ");
            }

            

            if (!string.IsNullOrWhiteSpace(queryData.DateFieldName))
            {
                querystrings.Add($" {queryData.DateFieldName}  BETWEEN  '{queryData.DateFrom}' AND '{queryData.DateTo}'  ");
            }

           
            if (!string.IsNullOrWhiteSpace(queryData.Installation_date))
            {
                querystrings.Add($" cast([Installation_date]  as date)   ='{queryData.Installation_date}'   ");
            }
           
            // Join the query conditions with 'AND' and return the resulting string

            if (querystrings.Count < 1)
            {
                return " ";
            }
            else
            {
                return " WHERE " + string.Join(" AND" + Environment.NewLine, querystrings);
            }
           
        }

        public string FormDateFromString(string tableName,string FieldName ,string FieldValue)
        {
            string WhereStr = "";
            try
            {
                DateTime dateTime = Convert.ToDateTime(FieldValue);
                WhereStr = $" cast( {tableName }.{FieldName} as date)='{dateTime.ToShortDateString()}'";
            }
            catch
            {
                WhereStr = "";
            }
            return WhereStr ;
        }
    }
}
