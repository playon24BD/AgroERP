using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Common
{
    public class DayAndModelWiseProductionChart
    {
        public long ModelId { get; set; }
        public string ModelName { get; set; }
        public string Date { get; set; }
        public int Quantity { get; set; }
    }
}
