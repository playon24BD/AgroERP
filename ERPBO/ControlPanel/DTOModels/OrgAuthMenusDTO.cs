using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.DTOModels
{
    public class OrgAuthMenusDTO
    {
        public long OrgId { get; set; }
        public List<MenusDataDTO> Menus { get; set; }
    }

    public class MenusDataDTO
    {
        public long MainMenuId { get; set; }
        public long ModuleId { get; set; }
    }
}
