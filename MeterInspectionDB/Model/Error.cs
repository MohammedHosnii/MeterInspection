using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterInspectionDB.Model
{
    public class Error
    {
        public int Id { get; set; }
        public int ErrorNumber { get; set; }
        public string ErrorName { get; set; }
    }
}
