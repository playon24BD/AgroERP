using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ReportModels
{
   public class ReturnRawMaterialDataReport
    {
        public DateTime? EntryDate { get; set; }
        public string RawMaterialName { get; set; }
        public string UnitName { get; set; }
        public string ReturnType { get; set; }
        public string Status { get; set; }
        public double Quantity { get; set; }
    }
}
