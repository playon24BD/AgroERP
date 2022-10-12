using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.ViewModels
{
    public class UserMainMenuViewModel
    {
        public long MainmenuId { get; set; }
        public string MainmenuName { get; set; }
        public List<UserSubmenuViewModel> UserSubmenus { get; set; }
    }

    public class UserSubmenuViewModel
    {
        public long SubmenuId { get; set; }
        public string SubmenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public bool IsParent { get; set; }
        public List<UserSubSubmenuViewModel> UserSubSubmenus { get; set; }
    }

    public class UserSubSubmenuViewModel
    {
        public long SubsubmenuId { get; set; }
        public string SubsubmenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }


}
