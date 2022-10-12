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
   public class SubMenuBusiness: ISubMenuBusiness
    {
        private readonly IControlPanelUnitOfWork _controlPanelUnitOfWork; // database
        private readonly SubMenuRepository _subMenuRepository; // repo
        public SubMenuBusiness(IControlPanelUnitOfWork controlPanelUnitOfWork)
        {
            this._controlPanelUnitOfWork = controlPanelUnitOfWork;
            _subMenuRepository = new SubMenuRepository(this._controlPanelUnitOfWork);
        }

        public IEnumerable<SubMenu> GetAllSubMenu()
        {
            return _subMenuRepository.GetAll();
        }

        public SubMenu GetSubMenuOneById(long id)
        {
            return _subMenuRepository.GetAll(sub => sub.SubMenuId == id).FirstOrDefault();
        }

        public bool SaveSubMenu(SubMenuDTO subMenuDTO, long userId)
        {
            SubMenu sub = new SubMenu();
            if (subMenuDTO.SubMenuId == 0)
            {
                sub.SubMenuName = subMenuDTO.SubMenuName;
                sub.ControllerName = subMenuDTO.ControllerName;
                sub.ActionName = subMenuDTO.ActionName;
                sub.IconClass = subMenuDTO.IconClass;
                sub.IsViewable = subMenuDTO.IsViewable;
                sub.IsActAsParent = subMenuDTO.IsActAsParent;
                sub.ParentSubMenuId = subMenuDTO.ParentSubMenuId;
                sub.MMId = subMenuDTO.MMId;
                sub.EUserId = userId;
                sub.EntryDate = DateTime.Now;
                _subMenuRepository.Insert(sub);
            }
            else
            {
                sub = GetSubMenuOneById(subMenuDTO.SubMenuId);
                sub.SubMenuName = subMenuDTO.SubMenuName;
                sub.ControllerName = subMenuDTO.ControllerName;
                sub.ActionName = subMenuDTO.ActionName;
                sub.IconClass = subMenuDTO.IconClass;
                sub.IsViewable = subMenuDTO.IsViewable;
                sub.IsActAsParent = subMenuDTO.IsActAsParent;
                sub.ParentSubMenuId = subMenuDTO.ParentSubMenuId;
                sub.MMId = subMenuDTO.MMId;
                sub.UpUserId = userId;
                sub.UpdateDate = DateTime.Now;
                _subMenuRepository.Update(sub);
            }
            return _subMenuRepository.Save();
        }
    }
}
