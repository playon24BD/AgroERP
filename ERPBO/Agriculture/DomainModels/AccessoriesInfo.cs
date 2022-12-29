using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblAccessoriesInfo")]
    public class AccessoriesInfo
    {
        [Key]
        public long AccessoriesId { get; set; }

        public string AccessoriesName { get; set; }

        public DateTime? UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long EntryUserId { get; set; }
        public string Status { get; set; }


    }
}
