using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class RawMaterialRequisitionDetailsViewModel
    {
        public long RawMaterialRequisitionDetailsId { get; set; }
        public long RawMaterialRequisitionInfoId { get; set; }
        public long RawMaterialId { get; set; }
        public double IssueQuantity { get; set; }
        public double RequisitionQuantity { get; set; }
        public long UnitID { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string flag { get; set; }
        public string Remarks { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public long OrganizationId { get; set; }
        //Custom:
        public string RawMaterialName { get; set; }
        public string UnitName { get; set; }
        public double AvailableQty { get; set; }
    }
}
