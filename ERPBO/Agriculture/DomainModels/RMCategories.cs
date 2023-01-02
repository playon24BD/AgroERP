using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblRMCategories")]
    public class RMCategories
    {
        [Key]
        public long RMCategorieId { get; set; }
        public string RMCategorieName { get; set; }
        public string Flag { get; set; }

    }
}
