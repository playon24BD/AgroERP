using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IUserInfo
    {
        IEnumerable<UserInfo> GetAllUserInfo(long OrgId);

        UserInfo GetUserInfoById(long userId, long orgId);
        IEnumerable<UserInfoDTO> GetUserInfos(long? userId,long orgId);
        
        bool SaveUserInfoList(List<UserInfoDTO> infoDTO, long userId, long orgId);
    }
}
