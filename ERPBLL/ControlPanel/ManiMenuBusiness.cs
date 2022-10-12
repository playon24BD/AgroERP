using ERPBLL.ControlPanel.Interface;
using ERPBO.ControlPanel.DomainModels;
using ERPBO.ControlPanel.DTOModels;
using ERPDAL.ControlPanelDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.ControlPanel
{
   public class ManiMenuBusiness: IManiMenuBusiness
    {
        private readonly IControlPanelUnitOfWork _controlPanelUnitOfWork; // database
        private readonly MainMenuRepository _mainMenuRepository; // repo
        public ManiMenuBusiness(IControlPanelUnitOfWork controlPanelUnitOfWork)
        {
            this._controlPanelUnitOfWork = controlPanelUnitOfWork;
            _mainMenuRepository = new MainMenuRepository(this._controlPanelUnitOfWork);
        }

        public IEnumerable<MainMenu> GetAllMainMenu()
        {
            return _mainMenuRepository.GetAll();
        }

        public MainMenu GetMainMenuOneById(long id)
        {
            return _mainMenuRepository.GetAll(menu => menu.MMId == id).FirstOrDefault();
        }

        public bool SaveMainMenu(MainMenuDTO maniMenuDTO, long userId)
        {
            MainMenu mainMenu = new MainMenu();
            if (maniMenuDTO.MMId == 0)
            {
                mainMenu.MenuName = maniMenuDTO.MenuName;
                mainMenu.MId = maniMenuDTO.MId;
                mainMenu.EUserId = userId;
                mainMenu.EntryDate = DateTime.Now;
                _mainMenuRepository.Insert(mainMenu);
            }
            else
            {
                mainMenu = GetMainMenuOneById(maniMenuDTO.MMId);
                mainMenu.MenuName = maniMenuDTO.MenuName;
                mainMenu.MId = maniMenuDTO.MId;
                mainMenu.UpUserId = userId;
                mainMenu.UpdateDate = DateTime.Now;
                _mainMenuRepository.Update(mainMenu);
            }
            return _mainMenuRepository.Save();
        }
    }
}
