using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Common
{
    public class DaysAndLineWiseProductionChart
    {
        public long LineId { get; set; }
        public string LineNumber { get; set; }
        public string Date { get; set; }
        public int Quantity { get; set; }
    }
}
