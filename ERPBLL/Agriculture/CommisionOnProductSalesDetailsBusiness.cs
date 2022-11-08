using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using ERPDAL.AgricultureDAL;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class CommisionOnProductSalesDetailsBusiness : ICommisionOnProductSalesDetailsBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly CommissionOnProductOnSalesDetailsBusinessRepository _commissionSalesDetailsRepository;

        public CommisionOnProductSalesDetailsBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._commissionSalesDetailsRepository = new CommissionOnProductOnSalesDetailsBusinessRepository(this._agricultureUnitOfWork);

        }
        public IEnumerable<CommisionOnProductSalesDetails> GetCommisionOnProductSalesDetails(long orgId)
        {
            return _commissionSalesDetailsRepository.GetAll(c => c.OrganizationId == orgId).ToList();
        }

        public CommisionOnProductSalesDetails GetCommisionOnProductSalesDetailsbyId(long commisionOnProductSalesDetailsId, long orgId)
        {
            return _commissionSalesDetailsRepository.GetOneByOrg(d => d.CommissionOnProductSalesDetailsId == commisionOnProductSalesDetailsId && d.OrganizationId == orgId);
        }

        public bool SaveCommisionOnProductSalesDetails(List<CommisionOnProductSalesDetailsDTO> onProductSalesDetailsDTO, long id, string flag, long userId, long orgId)
        {
            bool isSuccess = false;
            List<CommisionOnProductSalesDetails> productSalesDetails = new List<CommisionOnProductSalesDetails>();
            if (onProductSalesDetailsDTO.Count() > 0)
            {
                foreach (var item in onProductSalesDetailsDTO)
                {
                    CommisionOnProductSalesDetails commisionOnProductSalesDetails = new CommisionOnProductSalesDetails()
                    {
                        FinishGoodProductId = item.FinishGoodProductId,
                        CommissionOnProductOnSalesId = item.CommissionOnProductOnSalesId,
                        PaymentMode = item.PaymentMode,
                        Credit = item.Credit,
                        Cash = item.Cash,
                        TotalCommission = item.TotalCommission,
                        Status = item.Status,
                        Remarks = "Insert",
                        EntryUserId = userId,
                        EntryDate = DateTime.Now,
                    };
                }
                _commissionSalesDetailsRepository.InsertAll(productSalesDetails);
                isSuccess = _commissionSalesDetailsRepository.Save();
            }
            return isSuccess;
        }
    }
}
