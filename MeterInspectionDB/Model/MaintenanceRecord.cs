using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterInspectionDB.Model
{
    public class MaintenanceRecord
    {
        public int Id { get; set; }
        public DateTime MaintenanceRecordDate { get; set; }
        public int CompanySectorDept_Id { get; set; }
        public int LabCenter_Id { get; set; }
        public int MeterCount { get; set; }
        public int WorkingMetersCount { get; set; }
        public int RepairedMetersCount { get; set; }
        public int RetiredMetersCount { get; set; }
        public long MaintenanceRecordCode { get; set; }
        public bool ISSync { get; set; } = false;
        public int CompanySectorDept_Level { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
        public int? DeletedUserId { get; set; }
    }
}
