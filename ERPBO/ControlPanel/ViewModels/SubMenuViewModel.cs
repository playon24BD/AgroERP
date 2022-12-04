using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.ViewModels
{
   public class SubMenuViewModel
    {
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
        [Range(1, long.MaxValue)]
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [Range(1, long.MaxValue)]
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }

        //custom
        [Range(1, long.MaxValue)]
        public long MMId { get; set; }
        [StringLength(100)]
        public string MenuName { get; set; }
        public string ParentSubmenuName { get; set; }
        public string IsViewableStatus { get; set; }
        public string IsActAsParentStatus { get; set; }
        public string EntryUser { get; set; }
        public string UpdateUser { get; set; }
    }
}
