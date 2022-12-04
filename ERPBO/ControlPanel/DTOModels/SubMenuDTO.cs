using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.DTOModels
{
   public class SubMenuDTO
    {
        public long SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string IconClass { get; set; }
        public bool IsViewable { get; set; }
        public bool IsActAsParent { get; set; }
        public long? ParentSubMenuId { get; set; }
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }

        //custom
        public long MMId { get; set; }
        public string MenuName { get; set; }
        public string ParentSubmenuName { get; set; }
        public string IsViewableStatus { get; set; }
        public string IsActAsParentStatus { get; set; }
        public string EntryUser { get; set; }
        public string UpdateUser { get; set; }
    }
}
