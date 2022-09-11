using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.ViewModels
{
    public class UserCustomMenusViewModels
    {
        public long SubmenuId { get; set; }
        public string SubMenuName { get; set; }
        public long ParentSubMenuId { get; set; }
        public long MainmenuId { get; set; }
        public string MainmenuName { get; set; }
        public long ModuleId { get; set; }
        public string ModuleName { get; set; }
        public long TaskId { get; set; }
        public bool Add { get; set; }
        public bool Detail { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Approval { get; set; }
        public bool Report { get; set; }
        public string ParentSubmenuName { get; set; }
    }
}
