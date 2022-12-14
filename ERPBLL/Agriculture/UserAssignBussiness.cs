using ERPBLL.Agriculture.Interface;
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
    public class UserAssignBussiness : IUserAssignBussiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly UserAssignRepository _userAssignRepository;


        public UserAssignBussiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._userAssignRepository = new UserAssignRepository(this._agricultureUnitOfWork);

        }

        public IEnumerable<UserAssign> GetAllUserAssignInfo(long orgId)
        {
            return _userAssignRepository.GetAll(u=>u.OrganizationId==orgId);
        }

        public UserAssign GetUserAssignById(long userAssignId , long orgId)
        {
            return _userAssignRepository.GetOneByOrg(u => u.UserAssignId == userAssignId && u.OrganizationId == orgId);
        }

        //public UserAssign GetUserAssignInfoById(long userAssignId, long orgId)
        //{
        //    return _userAssignRepository.GetOneByOrg(r => r.UserAssignId == userAssignId && r.OrganizationId == orgId);
        //}

        public bool SaveUserAssignInformation(UserAssignDTO userAssignDTO, long userId, long orgId)
        {
            bool isSuccess = false;

            return isSuccess;
        }

        public bool SaveUserAssignInformation(List<UserAssignDTO> userAssignDTO, long userId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
