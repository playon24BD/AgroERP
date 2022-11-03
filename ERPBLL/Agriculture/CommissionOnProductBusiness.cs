using ERPBLL.Agriculture.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPDAL.AgricultureDAL;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;

namespace ERPBLL.Agriculture
{
    public class CommissionOnProductBusiness: ICommissionOnProductBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly CommissionOnProductBusinessRepository _commissionOnProductBusinessRepository;
        private readonly ICommissionOnProductHistoryBusiness _commissionOnProductHistoryBusiness;
        public CommissionOnProductBusiness(IAgricultureUnitOfWork agricultureUnitOfWork,ICommissionOnProductHistoryBusiness commissionOnProductHistoryBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._commissionOnProductBusinessRepository = new CommissionOnProductBusinessRepository(this._agricultureUnitOfWork);
            this._commissionOnProductHistoryBusiness = commissionOnProductHistoryBusiness;
        }

        public CommisionOnProduct GetCommisionOnProductbyId(long commissionOnProductId, long orgId)
        {
         return   _commissionOnProductBusinessRepository.GetOneByOrg(c=>c.CommissionOnProductId==commissionOnProductId && c.OrganizationId==orgId);
        }

        public IEnumerable<CommisionOnProduct> GetCommisionOnProducts(long orgId)
        {
            return _commissionOnProductBusinessRepository.GetAll(c => c.OrganizationId == orgId).ToList();
        }

        public bool SaveCommisionOnProductby(List<CommisionOnProductDTO> commisionOnProductDTOs, long userId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
