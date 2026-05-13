using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterInspectionDB.Model
{
    public class CompanySectorDept
    {
        public int Id { get; set; }
        public string CompanySectorDeptCode { get; set; }
        public string CompanySectorDeptName { get; set; }
        public int CompanySectorDept_Level { get; set; }
        public int? ParentId { get; set; } // nullable علشان الجذر
    }
}
