using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterInspectionDB.Model
{
    public class MaintenanceRecordDetail
    {
        public int Id { get; set; }
        public int MaintenanceRecord_id { get; set; }
        public long MeterNumber { get; set; }
        public int TestResultCode { get; set; }
        public long CorrectiveActionCode { get; set; }
        public int ErrorNumber { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string? Notes { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public bool ISSync { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedUserId { get; set; }
    }
}
