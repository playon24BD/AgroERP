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
    public class OrganizationAuthBusiness : IOrganizationAuthBusiness
    {
        private readonly IControlPanelUnitOfWork _controlPaneldb; // database
        private readonly OrganizationAuthorizationRepository _OrganizationAuthorizationRepository; // repo

        public OrganizationAuthBusiness(IControlPanelUnitOfWork controlPaneldb)
        {
            this._controlPaneldb = controlPaneldb;
            this._OrganizationAuthorizationRepository = new OrganizationAuthorizationRepository(this._controlPaneldb);
        }
        public IEnumerable<MainMenuDTO> GetMainMenusForOrgAuth()
        {
            return this._controlPaneldb.Db.Database.SqlQuery<MainMenuDTO>(string.Format(@"Select m.MId,m.ModuleName,mm.MMId,mm.MenuName From tblMainMenus mm
Inner Join tblModules m on mm.MId = m.MId")).ToList();
        }

        public IEnumerable<MainMenuDTO> GetMainMenusForOrgAuthById(long orgId)
        {
            return this._controlPaneldb.Db.Database.SqlQuery<MainMenuDTO>(string.Format(@"Select  m.MId,m.ModuleName,mm.MMId,mm.MenuName From tblOrganizationAuthorization oa
Inner Join tblMainMenus mm on oa.MainmenuId = mm.MMId
Inner Join tblModules m on mm.MId = m.MId 
where oa.OrganizationId={0}
", orgId)).ToList();
        }

        public IEnumerable<OrganizationAuthorization> GetOrganizationAuthorizations(long orgId)
        {
            return _OrganizationAuthorizationRepository.GetAll(o => o.OrganizationId == orgId);
        }

        public bool SaveOrganizationAuthMenus(OrgAuthMenusDTO dto, long userId)
        {
            bool IsSuccess = false;
            var orgAuthInDb = GetOrganizationAuthorizations(dto.OrgId);
            List<OrganizationAuthorization> list = new List<OrganizationAuthorization>();
            if (orgAuthInDb.Count() == 0)
            {
                
                foreach (var item in dto.Menus)
                {
                    OrganizationAuthorization authorization = new OrganizationAuthorization()
                    {
                        OrganizationId = dto.OrgId,
                        ModuleId = item.ModuleId,
                        MainmenuId = item.MainMenuId,
                        EUserId = userId,
                        EntryDate = DateTime.Now
                    };
                    list.Add(authorization);
                }
                _OrganizationAuthorizationRepository.InsertAll(list);
            }
            else
            {
                var menus = dto.Menus.Select(m => m.MainMenuId).ToList();
                _OrganizationAuthorizationRepository.DeleteAll(o => o.OrganizationId == dto.OrgId && !menus.Contains(o.MainmenuId));
                foreach (var item in dto.Menus)
                {
                    var existingAuth = orgAuthInDb.FirstOrDefault(o => o.MainmenuId == item.MainMenuId);
                    if(existingAuth != null)
                    {
                        existingAuth.UpdateDate = DateTime.Now;
                        existingAuth.UpUserId = userId;
                        _OrganizationAuthorizationRepository.Update(existingAuth);
                    }
                    else
                    {
                        OrganizationAuthorization authorization = new OrganizationAuthorization()
                        {
                            ModuleId = item.ModuleId,
                            MainmenuId = item.MainMenuId,
                            OrganizationId = dto.OrgId,
                            EntryDate = DateTime.Now,
                            EUserId = userId
                        };
                        list.Add(authorization);
                    }
                }
                if(list.Count > 0)
                {
                    _OrganizationAuthorizationRepository.InsertAll(list);
                }
            }
            IsSuccess = _OrganizationAuthorizationRepository.Save();
            return IsSuccess;
        }
    }
}
