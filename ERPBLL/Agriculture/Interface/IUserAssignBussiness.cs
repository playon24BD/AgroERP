using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
   public interface IUserAssignBussiness
    {
        //UserAssign GetUserAssignInfoById(long userAssignId, long orgId);
        IEnumerable<UserAssign> GetAllUserAssignInfo(long orgId);
        UserAssign GetUserAssignById(long userAssignId,long orgId);
        bool SaveUserAssignInformation(UserAssignDTO userAssignDTO ,long userId,long orgId);
        bool SaveUserAssignInformation(List<UserAssignDTO> userAssignDTO ,long userId,long orgId);
    }
}
