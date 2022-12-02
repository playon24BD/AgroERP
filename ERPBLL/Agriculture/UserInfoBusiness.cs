using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class UserInfoBusiness:IUserInfo
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly UserInfoRepository _userInfoRepository;

        public UserInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._userInfoRepository = new UserInfoRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<UserInfo> GetAllUserInfo(long OrgId)
        {
            return _userInfoRepository.GetAll(o => o.OrganizationId == OrgId).ToList();
        }

        public UserInfo GetUserInfoById(long userId, long orgId)
        {
            return _userInfoRepository.GetOneByOrg(r => r.UserId == userId && r.OrganizationId == orgId);
        }

        public IEnumerable<UserInfoDTO> GetUserInfos(long? userId, string departmentName, string designation, long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<UserInfoDTO>(QueryForUserInfoss(userId,departmentName,designation, orgId)).ToList();
        }

        private string QueryForUserInfoss(long? userId, string departmentName, string designation, long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            //param += string.Format(@"OrganizationId={0}", orgId);
            if (userId != null && userId > 0)
            {
                param += string.Format(@" and UserId={0}", userId);
            }
            if (departmentName != "")
            {
                param += string.Format(@" and DepartmentName like'%{0}%'", departmentName);
            }
            if (designation != "")
            {
                param += string.Format(@"and Designation like'%{0}%'", designation);
            }

            query = string.Format(@"
           select UserId, UserName,DepartmentName,Designation,Status from tblUserInfo
            where 1=1  {0}",
            Utility.ParamChecker(param));
            return query;
        }

        public bool SaveUserInfoList(List<UserInfoDTO> infoDTO, long userId, long orgId)
        {
            bool isSuccess = false;

            List<UserInfo> userInfos = new List<UserInfo>();

            foreach (var item in infoDTO)
            {
                UserInfo userInfo = new UserInfo()
                {
                    OrganizationId = orgId,
                    Status = "Active",
                    UserName = item.UserName,
                    DepartmentName = item.DepartmentName,
                    Designation = item.Designation,
                    MobileNumber = item.MobileNumber,
                    Address = item.Address,
                    Email = item.Email,
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                };
                _userInfoRepository.Insert(userInfo);
            }

            isSuccess = _userInfoRepository.Save();
            return isSuccess;
        }

        public bool UpdateUserInfoList(UserInfoDTO updateDTOs, long userId, long orgId)
        {
            bool isSuccess = false;

            UserInfo info = new UserInfo();
            info = GetUserInfoById(updateDTOs.UserId, orgId);
            info.UserName = updateDTOs.UserName;
            info.DepartmentName = updateDTOs.DepartmentName;
            info.Designation = updateDTOs.Designation;
            info.Status = updateDTOs.Status;
            info.UpdateDate = DateTime.Now;
            info.UpdateUserId = userId;
            _userInfoRepository.Update(info);

            isSuccess = _userInfoRepository.Save();
            return isSuccess;
        }
    }
}
