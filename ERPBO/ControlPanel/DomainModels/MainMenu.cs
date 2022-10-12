using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.DomainModels
{
    [Table("tblMainMenus")]
   public class MainMenu
    {
        [Key]
        public long MMId { get; set; }
        public string MenuName { get; set; }
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        [ForeignKey("Module")]
        public long MId { get; set; }
        public Module Module { get; set; }
        public ICollection<SubMenu> SubMenus { get; set; }
    }
}
