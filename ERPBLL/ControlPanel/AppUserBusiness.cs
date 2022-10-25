using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBLL.ControlPanel.Interface;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Common;
using ERPBO.ControlPanel.DomainModels;
using ERPBO.ControlPanel.DTOModels;
using ERPBO.ControlPanel.ViewModels;
using ERPDAL.ControlPanelDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.ControlPanel
{
    public class AppUserBusiness : IAppUserBusiness
    {
        private readonly IControlPanelUnitOfWork _controlPanelUnitOfWork; // database
        private readonly AppUserRepository appUserRepository; // repo
        private readonly IDivisionUserBusiness _divisionUserBusiness; //
        private readonly IZoneUserBusiness _zoneUserBusiness; //
        private readonly IDistributionUserBusiness _distributionUserBusiness; //
        public AppUserBusiness(IControlPanelUnitOfWork controlPanelUnitOfWork,IDivisionUserBusiness divisionUserBusiness,IDistributionUserBusiness distributionUserBusiness,IZoneUserBusiness zoneUserBusiness)
        {
            this._controlPanelUnitOfWork = controlPanelUnitOfWork;
            appUserRepository = new AppUserRepository(this._controlPanelUnitOfWork);
            this._divisionUserBusiness = divisionUserBusiness;
            this._distributionUserBusiness = distributionUserBusiness;
            this._zoneUserBusiness = zoneUserBusiness;
        }

        public bool ChangePassword(ChangePasswordDTO dto, long userId, long orgId)
        {
            var user =GetAppUserOneById(userId, orgId);
            if(user != null && dto !=null)
            {
                user.Password = dto.NewPassword;
                user.ConfirmPassword = dto.NewPassword;
                user.UpUserId = userId;
                user.UpdateDate = DateTime.Now;
                appUserRepository.Update(user);
            }
            return appUserRepository.Save();
        }

        public IEnumerable<AppUser> GetAllAppUserByOrgId(long orgId)
        {
            return appUserRepository.GetAll(user => user.OrganizationId == orgId).ToList();
        }

        public IEnumerable<AppUser> GetAllAppUsers()
        {
            return appUserRepository.GetAll().ToList();
        }

        public AppUser GetId(long userId,long orgId)
        {
            //string userid = userId.ToString();
            return appUserRepository.GetOneByOrg(a => a.UserId == userId && a.OrganizationId == orgId);
        }
        //public AppUser GetIds(string userId, long orgId)
        //{
        //    return appUserRepository.GetOneByOrg(a => a.UserId == userId && a.OrganizationId == orgId);
        //}
        public AppUserDTO GetAppUserInfoById(long id, long orgId,string flag)
        {
            if(flag == "System")
            {
                return _controlPanelUnitOfWork.Db.Database.SqlQuery<AppUserDTO>(string.Format(@"Select app.EmployeeId,app.UserId,app.FullName,app.MobileNo,app.[Address],app.Email,app.Desigation,app.UserName, 
app.[Password],app.ConfirmPassword,app.OrganizationId,app.RoleId,app.BranchId,app.IsActive,app.IsRoleActive,app.DivisionId As Division,app.ZoneId As Zone
From tblApplicationUsers app 
Inner Join tblBranch b on app.BranchId = b.BranchId and app.OrganizationId = b.OrganizationId
Inner Join tblRoles r on app.RoleId = r.RoleId and app.OrganizationId = r.OrganizationId
Where app.UserId={0}", id)).FirstOrDefault();
            }
            else
	        {
                return _controlPanelUnitOfWork.Db.Database.SqlQuery<AppUserDTO>(string.Format(@"Select app.EmployeeId,app.UserId,app.FullName,app.MobileNo,app.[Address],app.Email,app.Desigation,app.UserName, 
app.[Password],app.ConfirmPassword,app.OrganizationId,app.RoleId,app.BranchId,app.IsActive,app.IsRoleActive,app.DivisionId As Division
From tblApplicationUsers app 
Inner Join tblBranch b on app.BranchId = b.BranchId and app.OrganizationId = b.OrganizationId
Inner Join tblRoles r on app.RoleId = r.RoleId and app.OrganizationId = r.OrganizationId
Where app.UserId={0} and app.OrganizationId={1}", id,orgId)).FirstOrDefault();
            }
        }

        public AppUser GetAppUserOneById(long id, long orgId)
        {
            return appUserRepository.GetOneByOrg(user => user.UserId == id && user.OrganizationId == orgId);
        }

        public UserDetaildDTO GetUserDetail(long userId, long orgId)
        {
            return this._controlPanelUnitOfWork.Db.Database.SqlQuery<UserDetaildDTO>(string.Format(@"Select a.EmployeeId,o.OrganizationName,a.UserName,a.FullName,ISNULL(a.Email,'N/A') 'Email',ISNULL(a.Desigation,'N/A') 'Designation',r.RoleName, 
(Case When a.IsRoleActive = 0 then 'Inactive' else 'Active' End)'RoleStatus',
(Case When a.IsActive = 0 then 'Inactive' else 'Active' End)'UserStatus',
ISNULL(a.Address,'N/A') 'Address'
From tblApplicationUsers a
Inner Join tblOrganizations o on a.OrganizationId = o.OrganizationId
Left Join tblRoles r on a.RoleId = r.RoleId
Where a.OrganizationId = {1} and a.UserId = {0}", userId,orgId)).FirstOrDefault();
        }

        public async Task<UserInformation> GetUserInformation(UserLogInViewModel loginModel)
        {
            UserInformation user = new UserInformation();
            if(loginModel.UserName !="" && loginModel.Password != "")
            {
                if(Utility.ParamChecker(loginModel.UserName) !="" && Utility.ParamChecker(loginModel.Password) != "")
                {
                    //user = await _controlPanelUnitOfWork.Db.Database.SqlQuery<UserInformation>(string.Format(@"Select U.UserId,U.UserName,U.Desigation 'Designation',U.EmployeeId,U.Email,U.[Address],U.MobileNo,U.FullName,U.IsActive,O.OrganizationId,O.OrganizationName,O.OrgLogoPath,
                    //O.ReportLogoPath, 
                    //R.RoleName,R.RoleId,O.IsActive 'IsOrgActive',U.IsRoleActive,U.BranchId,brn.BranchName,O.AppType From tblApplicationUsers U
                    //Inner Join tblOrganizations O on U.OrganizationId = O.OrganizationId
                    //Inner Join tblBranch brn on U.BranchId = brn.BranchId
                    //Inner Join tblRoles R On U.RoleId = R.RoleId And R.OrganizationId = U.OrganizationId
                    //Where U.UserName = '{0}' And U.[Password] = '{1}'", loginModel.UserName, loginModel.Password)).FirstOrDefaultAsync();
                    user = await _controlPanelUnitOfWork.Db.Database.SqlQuery<UserInformation>(string.Format(@"EXEC	[dbo].[spUserAuthInformation] '{0}','{1}'", loginModel.UserName, loginModel.Password)).FirstOrDefaultAsync();
                }
            }
            return user;
        }

        public bool IsDuplicateEmployeeId(string employeeId, long id, long orgId)
        {
            return appUserRepository.GetOneByOrg(user => user.EmployeeId == employeeId && user.UserId != id && user.OrganizationId == orgId) != null ? true : false;
        }

        public bool SaveAppUser(AppUserDTO appUserDTO, long userId, long orgId)
        {
            AppUser appUser = new AppUser();
            //var roleId = _controlPanelUnitOfWork.Db.Database.SqlQuery<>
            if (appUserDTO.UserId == 0)
            {
                appUser.EmployeeId = appUserDTO.EmployeeId;
                appUser.FullName = appUserDTO.FullName;
                appUser.MobileNo = appUserDTO.MobileNo;
                appUser.Address = appUserDTO.Address;
                appUser.Email = appUserDTO.Email;
                appUser.Desigation = appUserDTO.Desigation;
                appUser.UserName = appUserDTO.UserName;
                appUser.Password = appUserDTO.Password;
                appUser.ConfirmPassword = appUserDTO.Password;
                appUser.IsActive = appUserDTO.IsActive;
                appUser.IsRoleActive = appUserDTO.IsRoleActive;
                appUser.EUserId = userId;
                appUser.EntryDate = DateTime.Now;
                appUser.OrganizationId = appUserDTO.OrganizationId;
                appUser.BranchId = appUserDTO.BranchId;
                appUser.RoleId = appUserDTO.RoleId;
                appUserRepository.Insert(appUser);

            }
            else
            {
                appUser = GetAppUserOneById(appUserDTO.UserId, orgId);
                appUser.EmployeeId = appUserDTO.EmployeeId;
                appUser.FullName = appUserDTO.FullName;
                appUser.MobileNo = appUserDTO.MobileNo;
                appUser.Address = appUserDTO.Address;
                appUser.Email = appUserDTO.Email;
                appUser.Desigation = appUserDTO.Desigation;
                appUser.UserName = appUserDTO.UserName;
                appUser.Password = appUserDTO.Password;
                appUser.ConfirmPassword = appUserDTO.Password;
                appUser.IsActive = appUserDTO.IsActive;
                appUser.IsRoleActive = appUserDTO.IsRoleActive;
                appUser.UpUserId = userId;
                appUser.UpdateDate = DateTime.Now;
                appUser.OrganizationId = appUserDTO.OrganizationId;
                appUser.BranchId = appUserDTO.BranchId;
                appUser.RoleId = appUserDTO.RoleId;
                appUserRepository.Update(appUser);
            }
            return appUserRepository.Save();
        }

        public ExecutionStateWithText SaveAppUser2(AppUserDTO appUserDTO, long userId, long orgId)
        {
            ExecutionStateWithText execution = new ExecutionStateWithText();
            string srUserId = "0";
            var division = string.Empty;
            var district = string.Empty;
            var zone = string.Empty;
            string action = string.Empty;


            if (appUserDTO.ZoneId.Count() > 0)
            {
                foreach (var zon in appUserDTO.ZoneId)
                {
                    zone += zon + ",";
                }
                zone = zone.Substring(0, zone.Length - 1);

            }
            AppUser appUser = new AppUser();
            List<DistributionUserDTO> distributionUserDTO = new List<DistributionUserDTO>();
            if (appUserDTO.UserId == 0)
            {


                if (appUserDTO.DivisionId.Count() > 0)
                {
                    foreach (var div in appUserDTO.DivisionId)
                    {
                        division += div + ",";
                    }
                    division = division.Substring(0, division.Length - 1);
     
                }


  

                appUser.EmployeeId = appUserDTO.EmployeeId;
                appUser.FullName = appUserDTO.FullName;
                appUser.MobileNo = appUserDTO.MobileNo;
                appUser.Address = appUserDTO.Address;
                appUser.Email = appUserDTO.Email;
                appUser.Desigation = appUserDTO.Desigation;
                appUser.UserName = appUserDTO.UserName;
                appUser.Password = appUserDTO.Password;
                appUser.ConfirmPassword = appUserDTO.ConfirmPassword;
                appUser.IsActive = appUserDTO.IsActive;
                appUser.IsRoleActive = appUserDTO.IsRoleActive;
                appUser.EUserId = userId;
                appUser.EntryDate = DateTime.Now;
                appUser.OrganizationId = appUserDTO.OrganizationId;
                appUser.BranchId = appUserDTO.BranchId;
                appUser.RoleId = appUserDTO.RoleId;
              
                appUser.ZoneId = zone;
                appUser.DivisionId = division;
                
                appUser.RegionId = appUserDTO.RegionId;
                appUser.AreaId = appUserDTO.AreaId;
                appUser.TerritoryId = appUserDTO.TerritoryId;
                appUser.StockiestId = appUserDTO.StockiestId;

                appUserRepository.Insert(appUser);

                execution.isSuccess = appUserRepository.Save();


                execution.text = appUser.UserId.ToString();

                if (execution.isSuccess==true)
                {

                    if (division.Count() > 0)
                    {
                        var divisions = division.Split(',');
                        foreach (var id in divisions)
                        {
                            DistributionUserDTO du = new DistributionUserDTO()
                            {
                                UserId = appUser.UserId,
                                DivisionId =Int64.Parse(id),
                                DistributionType="Division",
                                OrganizationId=orgId,
                                EntryDate=DateTime.Now,
                                EntryUserId =userId,
                                Status="INSERT",
                            };

                            distributionUserDTO.Add(du);
                        }
                        
                        
                    }



                    if (appUserDTO.ZoneId.Count() > 0)
                    {
                        _zoneUserBusiness.SaveZoneUser(appUserDTO.ZoneId, appUser.UserId, userId, orgId,action="Insert");
                    }

                    if (appUserDTO.DivisionId.Count() > 0)
                    {
                        _divisionUserBusiness.SaveDivisionsUser( appUserDTO.DivisionId, appUser.UserId, userId, orgId);
                    }


                    if (zone.Count() > 0)
                    {
                        var zones = zone.Split(',');
                        foreach (var id in zones)
                        {
                            DistributionUserDTO du = new DistributionUserDTO()
                            {
                                UserId = appUser.UserId,
                                ZoneId = Int64.Parse(id),
                                DistributionType = "Zone",
                                OrganizationId = orgId,
                                EntryDate = DateTime.Now,
                                EntryUserId = userId,
                                Status = "INSERT",
                            };

                            distributionUserDTO.Add(du);
                        }


                    }

                    if (distributionUserDTO.Count()>0)
                    {
                        _distributionUserBusiness.SaveDistributionUser(distributionUserDTO,userId,orgId);
                    }


                }


            }
            else
            {



                if (appUserDTO.DivisionId.Count() > 0)
                {
                    foreach (var div in appUserDTO.DivisionId)
                    {
                        division += div + ",";
                    }
                    division = division.Substring(0, division.Length - 1);
        
                }
                var  preUserId=   this.GetAppUserOneById(appUserDTO.UserId,orgId);

                if (preUserId !=null)
                {

                    if (zone.Count() > 0)
                    {
     
                        string[] splitInDB = preUserId.ZoneId.Split(',');
                        string[] splitInDTO = zone.Split(',');

                        var eq = (from c in splitInDTO
                                  where splitInDB == null || splitInDB.Any(x => x == c)
                                  select c).ToList();

                        splitInDB = splitInDB.Except(eq).ToArray();
                        splitInDTO = splitInDTO.Except(eq).ToArray();

                        foreach (var item in splitInDB)
                        {
                            DistributionUserDTO du = new DistributionUserDTO()
                            {
                                UserId = appUserDTO.UserId,
                                ZoneId = Int64.Parse(item),
                                DistributionType = "Zone",
                                OrganizationId = orgId,
                                EntryDate = DateTime.Now,
                                EntryUserId = userId,
                                Status = "DELETE",
                            };

                            distributionUserDTO.Add(du);

                        }
                        foreach (var item in splitInDTO)
                        {
                            DistributionUserDTO du = new DistributionUserDTO()
                            {
                                UserId = appUserDTO.UserId,
                                ZoneId = Int64.Parse(item),
                                DistributionType = "Zone",
                                OrganizationId = orgId,
                                EntryDate = DateTime.Now,
                                EntryUserId = userId,
                                Status = "INSERT",
                            };

                            distributionUserDTO.Add(du);

                        }
                        if (distributionUserDTO.Count() > 0)
                        {
                            _distributionUserBusiness.SaveDistributionUser(distributionUserDTO, userId, orgId);
                        }

                    }
                    if (division.Count() > 0)
                    {




                        string[] splitInDB = preUserId.DivisionId.Split(',');
                        string[] splitInDTO = division.Split(',');
                        var eq = (from c in splitInDTO
                                  where splitInDB == null || splitInDB.Any(x => x == c)
                                  select c).ToList();

                        splitInDB = splitInDB.Except(eq).ToArray();
                        splitInDTO = splitInDTO.Except(eq).ToArray();

                        foreach (var item in splitInDB)
                        {
                            DistributionUserDTO du = new DistributionUserDTO()
                            {
                                UserId = appUserDTO.UserId,
                                DivisionId = Int64.Parse(item),
                                DistributionType = "Division",
                                OrganizationId = orgId,
                                EntryDate = DateTime.Now,
                                EntryUserId = userId,
                                Status = "DELETE",
                            };

                            distributionUserDTO.Add(du);

                        }
                        foreach (var item in splitInDTO)
                        {
                            DistributionUserDTO du = new DistributionUserDTO()
                            {
                                UserId = appUserDTO.UserId,
                                DivisionId = Int64.Parse(item),
                                DistributionType = "Division",
                                OrganizationId = orgId,
                                EntryDate = DateTime.Now,
                                EntryUserId = userId,
                                Status = "INSERT",
                            };

                            distributionUserDTO.Add(du);

                        }
                        if (distributionUserDTO.Count() > 0)
                        {
                            _distributionUserBusiness.SaveDistributionUser(distributionUserDTO, userId, orgId);
                        }

                    }
                }

                appUser = GetAppUserOneById(appUserDTO.UserId, appUserDTO.OrganizationId);
                appUser.EmployeeId = appUserDTO.EmployeeId;
                appUser.FullName = appUserDTO.FullName;
                appUser.MobileNo = appUserDTO.MobileNo;
                appUser.Address = appUserDTO.Address;
                appUser.Email = appUserDTO.Email;
                appUser.Desigation = appUserDTO.Desigation;
                appUser.UserName = appUserDTO.UserName;
                appUser.Password = appUserDTO.Password;
                appUser.ConfirmPassword = appUserDTO.Password;
                appUser.IsActive = appUserDTO.IsActive;
                appUser.IsRoleActive = appUserDTO.IsRoleActive;
                appUser.UpUserId = userId;
                appUser.UpdateDate = DateTime.Now;
                appUser.OrganizationId = appUserDTO.OrganizationId;
                appUser.BranchId = appUserDTO.BranchId;
                appUser.RoleId = appUserDTO.RoleId;
                appUser.ZoneId = zone;
                appUser.DivisionId = division;
                appUser.RegionId = appUserDTO.RegionId;
                appUser.AreaId = appUserDTO.AreaId;
                appUser.TerritoryId = appUserDTO.TerritoryId;
                appUser.StockiestId = appUserDTO.StockiestId;
                appUserRepository.Update(appUser);

                execution.isSuccess = appUserRepository.Save();

                execution.text = appUser.UserId.ToString();

                if (execution.isSuccess)
                {
                    if (appUser.ZoneId.Count()>0)
                    {
                        _zoneUserBusiness.SaveZoneUser(appUserDTO.ZoneId, appUser.UserId, userId, orgId, action = "Update");
                    }
                    if (appUserDTO.DivisionId.Count()>0)
                    {
                        _divisionUserBusiness.UpdateDivisions(appUserDTO.DivisionId, appUser.UserId, userId, orgId);

                    }
            
                }

           
                
                    
                
            }


 
            return execution;
        }

        public bool SaveSRAppUser(AppUserDTO appUserDTO, long userId, long orgId,string role, out string srUserId)
        {
            bool IsSuccess = false;
            AppUser appUser = new AppUser();
            var roleId = _controlPanelUnitOfWork.Db.Database.SqlQuery<long>(string.Format(@"Select RoleId From tblRoles Where OrganizationId = {0} and RoleName='{1}'", orgId, role)).SingleOrDefault();

            srUserId = string.Empty;
            if (appUserDTO.UserId == 0)
            {
                appUser.EmployeeId = appUserDTO.EmployeeId;
                appUser.FullName = appUserDTO.FullName;
                appUser.MobileNo = appUserDTO.MobileNo;
                appUser.Address = appUserDTO.Address;
                appUser.Email = appUserDTO.Email;
                appUser.Desigation = appUserDTO.Desigation;
                appUser.UserName = appUserDTO.UserName;
                appUser.Password = appUserDTO.Password;
                appUser.ConfirmPassword = appUserDTO.Password;
                appUser.IsActive = appUserDTO.IsActive;
                appUser.IsRoleActive = appUserDTO.IsRoleActive;
                appUser.EUserId = userId;
                appUser.EntryDate = DateTime.Now;
                appUser.OrganizationId = appUserDTO.OrganizationId;
                appUser.BranchId = appUserDTO.BranchId;
                appUser.RoleId = roleId;
                appUserRepository.Insert(appUser);
            }
            IsSuccess =appUserRepository.Save();
            srUserId = appUser.UserId.ToString();
            return IsSuccess;
        }
    }
}
