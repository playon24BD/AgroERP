using ERPBLL.ControlPanel.Interface;
using ERPDAL.ControlPanelDAL;
using System;
using System.Collections.Generic;
using ERPBO.ControlPanel.DomainModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPBO.ControlPanel.DTOModels;

namespace ERPBLL.ControlPanel
{
   public class ModuleBusiness: IModuleBusiness
    {
        private readonly IControlPanelUnitOfWork _controlPanelUnitOfWork; // database
        private readonly ModuleRepository _moduleRepository; // repo
        public ModuleBusiness(IControlPanelUnitOfWork controlPanelUnitOfWork)
        {
            this._controlPanelUnitOfWork = controlPanelUnitOfWork;
            _moduleRepository = new ModuleRepository(this._controlPanelUnitOfWork);
        }

        public IEnumerable<Module> GetAllModules()
        {
            return _moduleRepository.GetAll();
        }

        public Module GetModuleOneById(long id)
        {
            return _moduleRepository.GetAll(module => module.MId == id).FirstOrDefault();
        }

        public bool SaveModule(ModuleDTO moduleDTO, long userId)
        {
            Module module = new Module();
            if (moduleDTO.MId == 0)
            {
                module.ModuleName = moduleDTO.ModuleName;
                module.IconName = moduleDTO.IconName;
                module.IconColor = moduleDTO.IconColor;
                module.EUserId = userId;
                module.EntryDate = DateTime.Now;
                _moduleRepository.Insert(module);
            }
            else
            {
                module = GetModuleOneById(moduleDTO.MId);
                module.ModuleName = moduleDTO.ModuleName;
                module.IconName = moduleDTO.IconName;
                module.IconColor = moduleDTO.IconColor;
                module.UpUserId = userId;
                module.UpdateDate = DateTime.Now;
                _moduleRepository.Update(module);
            }
            return _moduleRepository.Save();
        }
    }
}
