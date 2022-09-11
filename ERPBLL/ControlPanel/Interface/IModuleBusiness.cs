using System;
using ERPBO.ControlPanel.DomainModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPBO.ControlPanel.DTOModels;

namespace ERPBLL.ControlPanel.Interface
{
   public interface IModuleBusiness
    {
        IEnumerable<Module> GetAllModules();
        Module GetModuleOneById(long id);
        bool SaveModule(ModuleDTO moduleDTO, long userId);
    }
}
