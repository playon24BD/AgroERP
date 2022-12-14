using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class ZoneDetailViewModel
    {
        public long ZoneDetailId { get; set; }
        public long DivisionId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string Status { get; set; }
        public long OrganizationId { get; set; }
        public string Remarks { get; set; }

    }
}
