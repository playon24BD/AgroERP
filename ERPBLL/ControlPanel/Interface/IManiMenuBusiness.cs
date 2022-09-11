using ERPBO.ControlPanel.DomainModels;
using ERPBO.ControlPanel.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.ControlPanel.Interface
{
   public interface IManiMenuBusiness
    {
        IEnumerable<MainMenu> GetAllMainMenu();
        MainMenu GetMainMenuOneById(long id);
        bool SaveMainMenu(MainMenuDTO maniMenuDTO, long userId);
    }
}
