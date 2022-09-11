using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.DTOModels
{
    public class UserMainMenuDTO
    {
        public long MainmenuId { get; set; }
        public string MainmenuName { get; set; }
        public List<UserSubmenuDTO> UserSubmenus { get; set; }
    }

    public class UserSubmenuDTO
    {
        public long SubmenuId { get; set; }
        public string SubmenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public bool IsParent { get; set; }
        public List<UserSubSubmenuDTO> UserSubSubmenus { get; set; }
    }

    public class UserSubSubmenuDTO
    {
        public long SubsubmenuId { get; set; }
        public string SubsubmenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }


}
