using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.ViewModels
{
    public class ModuleUIViewModel
    {
        public long MId { get; set; }
        public string ModuleName { get; set; }
        public List<MainMenuUIVIewModel> MainMenus { get; set; }
    }

    public class MainMenuUIVIewModel
    {
        public long MMId { get; set; }
        public string MainmenuName { get; set; }
    }
}
