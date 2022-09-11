using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPBO.ControlPanel.ViewModels
{
    public class VmUserModule
    {
        public long ModuleId { get; set; }
        public string ModuleName { get; set; }
        public List<VmUserMenu> Menus { get; set; }
    }
    public class VmUserMenu
    {
        public long MenuId { get; set; }
        public string MenuName { get; set; }
        public List<VmUserSubmenu> SubMenus { get; set; }
    }

    public class VmUserSubmenu
    {
        public long SubmenuId { get; set; }
        public string SubMenuName { get; set; }
        public long ParentSubMenuId { get; set; }
        public long TaskId { get; set; }
        public bool Add { get; set; }
        public bool Detail { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Approval { get; set; }
        public bool Report { get; set; }
    }
}