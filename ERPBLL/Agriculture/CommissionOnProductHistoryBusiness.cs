using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DomainModels;
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
            bool IsSuccess = false;
            List<CommisionOnProductHistory> commisionOnProductHistories = new List<CommisionOnProductHistory>();
            if (commisionOnProductHistoryDTOs.Count() > 0)
            {
                foreach (var item in commisionOnProductHistoryDTOs)
                {
                    CommisionOnProductHistory commisionOnProductHistory = new CommisionOnProductHistory
                    {

                        FGRId = item.FGRId,
                        FinishGoodProductId = item.FinishGoodProductId,
                        CalenderYear = item.CalenderYear,
                        Cash = item.Cash,
                        Credit = item.Credit,
                        EntryDate = DateTime.Now,
                        EntryUserId = userId,
                        OrganizationId = orgId,

                    };
                    commisionOnProductHistories.Add(commisionOnProductHistory);
                }

            }
            _commissionOnProductHistoryBusinessRepository.InsertAll(commisionOnProductHistories);
            IsSuccess = _commissionOnProductHistoryBusinessRepository.Save();

            return IsSuccess;
        }

        public bool SaveCommissionOnProductHistoryWhenInsert(List<CommisionOnProduct> commisionOnProductHistoryDTOs, long userId, long orgId)
        {
            bool IsSuccess = false;
            List<CommisionOnProductHistory> commisionOnProductHistories = new List<CommisionOnProductHistory>();
            if (commisionOnProductHistoryDTOs.Count() > 0)
            {
                foreach (var item in commisionOnProductHistoryDTOs)
                {
                    CommisionOnProductHistory commisionOnProductHistory = new CommisionOnProductHistory
                    {

                        FGRId = item.FGRId,
                        FinishGoodProductId = item.FinishGoodProductId,
                        CalenderYear = item.CalenderYear,
                        Cash = item.Cash,
                        Credit = item.Credit,
                        EntryDate = DateTime.Now,
                        EntryUserId = userId,
                        OrganizationId = orgId,

                    };
                    commisionOnProductHistories.Add(commisionOnProductHistory);
                }

            }
            _commissionOnProductHistoryBusinessRepository.InsertAll(commisionOnProductHistories);
            IsSuccess = _commissionOnProductHistoryBusinessRepository.Save();

            return IsSuccess;
        }
    }
}
