using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ReportModels
{
    public class IssueRequisitionReportData
    {
        public double IssueQuantity { get; set; }
        public double RequisitionQuantity { get; set; }
        public string Remarks { get; set; }
        public DateTime? EntryDate { get; set; }
        public string RawMaterialName { get; set; }
        public string UnitName { get; set; }
        public string Status { get; set; }
        public string RawMaterialRequisitionCode { get; set; }
        public string FullName { get; set; }
    }
}
