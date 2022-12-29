using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class AccessoriesInfoViewModel
    {
        public long AccessoriesId { get; set; }

        public string AccessoriesName { get; set; }

        public DateTime? UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long EntryUserId { get; set; }
        public string Status { get; set; }
    }
}
