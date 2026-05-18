using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    //public class QueryDataParserPaged
    //{
    //    private readonly QueryDataPaged queryData;
    //    private List<string> querystrings = new List<string>();
    //    public QueryDataParserPaged(QueryDataPaged queryData)
    //    {
    //        this.queryData = queryData;
    //    }

    //    public string Parse()
    //    {
    //        if (!string.IsNullOrWhiteSpace(queryData.SectorCode))
    //        {
    //            querystrings.Add($"[Sector_code] =  {queryData.SectorCode.Trim()} ");
    //        }

    //        if (!string.IsNullOrWhiteSpace(queryData.DepartmentCode))
    //        {
    //            querystrings.Add($"[Department_code] =  {queryData.DepartmentCode.Trim()} ");
    //        }

    //        if (!string.IsNullOrWhiteSpace(queryData.RegionCode))
    //        {
    //            querystrings.Add($"[Region_code] = {queryData.RegionCode.Trim()} ");
    //        }

    //        if (!string.IsNullOrWhiteSpace(queryData.DayCode))
    //        {
    //            querystrings.Add($"[Day_code] = {queryData.DayCode.Trim()}  ");
    //        }

    //        if (!string.IsNullOrWhiteSpace(queryData.MainCode))
    //        {
    //            querystrings.Add($"[Main_code] =  {queryData.MainCode.Trim()} ");
    //        }

    //        if (!string.IsNullOrWhiteSpace(queryData.FirstEsdarDate))
    //        {
    //            querystrings.Add($"[First_esdar_date] =  N'{queryData.FirstEsdarDate.Trim()}' ");
    //        }

    //        if (!string.IsNullOrWhiteSpace(queryData.BranchCode))
    //        {
    //            querystrings.Add($"[Branch_code] =  {queryData.BranchCode.Trim()}  ");
    //        }

    //        if (!string.IsNullOrWhiteSpace(queryData.CustName))
    //        {
    //            querystrings.Add($"[Customer_name]  like N'%{queryData.CustName.Trim()}%'   ");
    //        }

    //        if (!string.IsNullOrWhiteSpace(queryData.CustAddress))
    //        {
    //            querystrings.Add($"[Customer_address] like N'%{queryData.CustAddress.Trim()}%' ");
    //        }

    //        if (!string.IsNullOrWhiteSpace(queryData.MeterNo))
    //        {
    //            querystrings.Add($"[Meter_number] =  {queryData.MeterNo.Trim()}  ");
    //        }

    //        if (!string.IsNullOrWhiteSpace(queryData.CustomerCode))
    //        {
    //            querystrings.Add($"[CustomerCode] =  '{queryData.CustomerCode.Trim()}'  ");
    //        }

    //        if (!string.IsNullOrWhiteSpace(queryData.DataItemId))
    //        {
    //            querystrings.Add($"[DataItemId] in ( {queryData.DataItemId} ) ");
    //        }

    //        // Join the query conditions with 'AND' and return the resulting string

    //        if (querystrings.Count < 1)
    //        {
    //            return " ";
    //        }
    //        else
    //        {
    //            return " WHERE " + string.Join(" AND" + Environment.NewLine, querystrings);
    //        }

    //    }
    //}
}
