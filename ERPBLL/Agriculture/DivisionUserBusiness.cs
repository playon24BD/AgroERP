using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class DivisionUserBusiness : IDivisionUserBusiness
    {
        private readonly IAgricultureUnitOfWork _AgricultureUnitOfWork;
        private readonly DivisionUserBusinessRepository divisionUserBusinessRepository;

        public DivisionUserBusiness(IAgricultureUnitOfWork AgricultureUnitOfWork)
        {
            this._AgricultureUnitOfWork = AgricultureUnitOfWork;
            this.divisionUserBusinessRepository = new DivisionUserBusinessRepository(this._AgricultureUnitOfWork);
        }
        public DivisionUser GetAllDivisionsByUserIdAndDivisionId(long userId, long divisionId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool SaveDivisionsUser(List<long> divisions, long userId, long suserId, long orgId)
        {
            bool isSuccess = false;
            List<DivisionUser> divisionsUser = new List<DivisionUser>();
            //foreach (var item in divisions)
            //{
            //    DivisionUser du = new DivisionUser()
            //    {
            //        DivisionId = item,
            //        EntryDate = DateTime.Now,
            //        EntryUserId = suserId,
            //        UserId = userId,
            //         = orgId,
            //    };
            //    rSMDivisions.Add(rSM);
            //}
            //if (rSMDivisions.Count() > 0)
            //{
            //    _rSMDivisionsRepository.InsertAll(rSMDivisions);
            //    isSuccess = _rSMDivisionsRepository.Save();
            //}

            return isSuccess;
        }

        public bool UpdateDivisions(List<long> divisions, long userId, long suserId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
