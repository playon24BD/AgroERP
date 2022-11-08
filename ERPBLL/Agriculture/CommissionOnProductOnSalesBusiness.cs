using ERPBLL.Agriculture.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERPDAL.AgricultureDAL;
using System.Threading.Tasks;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;

namespace ERPBLL.Agriculture
{
    public class CommissionOnProductOnSalesBusiness : ICommissionOnProductOnSalesBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly CommissionOnProductOnSalesBusinessRepository _commissionOnProductOnSalesRepository;

        public CommissionOnProductOnSalesBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._commissionOnProductOnSalesRepository = new CommissionOnProductOnSalesBusinessRepository(this._agricultureUnitOfWork);
        }

        public CommissionOnProductOnSales GetCommissionOnProductById(long commissionOnProductSalesId, long orgId)
        {
            return _commissionOnProductOnSalesRepository.GetOneByOrg(c => c.CommissionOnProductOnSalesId == commissionOnProductSalesId && c.OrganizationId == orgId);
        }

        public IEnumerable<CommissionOnProductOnSales> GetCommissionOnProductOnSales(long orgId)
        {
            return _commissionOnProductOnSalesRepository.GetAll(a => a.OrganizationId == orgId).ToList(); ;
        }

        public bool SaveCommissionOnProductOnSales(CommissionOnProductOnSalesDTO commissionOnProductOnSalesDTO, long userId, long orgId)
        {
            bool isSuccess = false;
            if (commissionOnProductOnSalesDTO.CommissionOnProductOnSalesId == 0)
            {
                CommissionOnProductOnSales commissionOnProductOnSales = new CommissionOnProductOnSales()
                {
                    ProductSalesInfoId = commissionOnProductOnSalesDTO.ProductSalesInfoId,
                    InvoiceNo = commissionOnProductOnSalesDTO.InvoiceNo,
                    PaymentMode = commissionOnProductOnSalesDTO.PaymentMode,
                    Status = commissionOnProductOnSalesDTO.Status,

                    EntryDate = DateTime.Now,
                    OrganizationId = orgId,
                    EntryUserId = userId,

                };
                _commissionOnProductOnSalesRepository.Insert(commissionOnProductOnSales);
            }
            else
            {
                var commissionOnProductSales = this.GetCommissionOnProductById(commissionOnProductOnSalesDTO.CommissionOnProductOnSalesId, orgId);
                commissionOnProductSales.Status = commissionOnProductOnSalesDTO.Status;
                commissionOnProductSales.PaymentMode = commissionOnProductOnSalesDTO.PaymentMode;
                commissionOnProductSales.UpdateDate = DateTime.Now;
                commissionOnProductSales.UpdateUserId = userId;
                _commissionOnProductOnSalesRepository.Update(commissionOnProductSales);

            }

            isSuccess = _commissionOnProductOnSalesRepository.Save();
            return isSuccess;
        }
    }
}
