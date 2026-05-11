using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterInspectionDB.Model
{
    public class CorrectiveAction
    {
        public int Id { get; set; }
        public int CorrectiveActionCode { get; set; }
        public string CorrectiveActionName { get; set; }
    }
}
