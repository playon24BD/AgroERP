using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.ViewModels
{
    public class OrgAuthMenusViewModels
    {
        [Range(1,long.MaxValue)]
        public long OrgId { get; set; }
        public List<MenusDataViewModels> Menus { get; set; }
    }

    public class MenusDataViewModels
    {
        [Range(1, long.MaxValue)]
        public long MainMenuId { get; set; }
        [Range(1, long.MaxValue)]
        public long ModuleId { get; set; }
    }
}
