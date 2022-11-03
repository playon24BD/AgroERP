using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DTOModels;
using ERPDAL.AgricultureDAL;

namespace ERPBLL.Agriculture
{
    public class CommissionOnProductHistoryBusiness : ICommissionOnProductHistoryBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly CommissionOnProductHistoryBusinessRepository _commissionOnProductHistoryBusinessRepository;
       
        public CommissionOnProductHistoryBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._commissionOnProductHistoryBusinessRepository = new CommissionOnProductHistoryBusinessRepository(this._agricultureUnitOfWork);


        }

        public bool SaveCommissionOnProductHistory(List<CommisionOnProductHistoryDTO> commisionOnProductHistoryDTOs, long userId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
