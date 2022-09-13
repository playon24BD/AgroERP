using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class FinishGoodProductViewModel
    {
        [Key]
        public long FinishGoodProductId { get; set; }
        public string FinishGoodProductName { get; set; }
        public long OrganizationId { get; set; }
        public long RoleId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long EntryUserId { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public string OrganizationName { get; set; }
    }
}
