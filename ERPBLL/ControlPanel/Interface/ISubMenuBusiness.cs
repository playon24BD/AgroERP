using ERPBO.ControlPanel.DomainModels;
using ERPBO.ControlPanel.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.ControlPanel.Interface
{
   public interface ISubMenuBusiness
    {
        IEnumerable<SubMenu> GetAllSubMenu();
        SubMenu GetSubMenuOneById(long id);
        bool SaveSubMenu(SubMenuDTO subMenuDTO, long userId);
    }
}
