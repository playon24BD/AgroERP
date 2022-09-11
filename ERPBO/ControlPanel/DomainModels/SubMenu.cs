using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.DomainModels
{
    [Table("tblSubMenus")]
   public class SubMenu
    {
        [Key]
        public long SubMenuId { get; set; }
        [StringLength(100)]
        public string SubMenuName { get; set; }
        [StringLength(150)]
        public string ControllerName { get; set; }
        [StringLength(150)]
        public string ActionName { get; set; }
        [StringLength(100)]
        public string IconClass { get; set; }
        public bool IsViewable { get; set; }
        public bool IsActAsParent { get; set; }
        public long? ParentSubMenuId { get; set; }
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        [ForeignKey("MainMenu")]
        public long MMId { get; set; }
        public MainMenu MainMenu { get; set; }
    }
}
