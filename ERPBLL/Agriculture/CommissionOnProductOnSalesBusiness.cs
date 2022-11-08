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
        private readonly ICommisionOnProductSalesDetailsBusiness _commisionOnProductSalesDetailsBusiness;

        public CommissionOnProductOnSalesBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, ICommisionOnProductSalesDetailsBusiness commisionOnProductSalesDetailsBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._commissionOnProductOnSalesRepository = new CommissionOnProductOnSalesBusinessRepository(this._agricultureUnitOfWork);
            this._commisionOnProductSalesDetailsBusiness = commisionOnProductSalesDetailsBusiness;
        }

        public CommissionOnProductOnSales GetCommissionOnProductById(long commissionOnProductSalesId, long orgId)
        {
            return _commissionOnProductOnSalesRepository.GetOneByOrg(c => c.CommissionOnProductOnSalesId == commissionOnProductSalesId && c.OrganizationId == orgId);
        }

        public IEnumerable<CommissionOnProductOnSales> GetCommissionOnProductOnSales(long orgId)
        {
            return _commissionOnProductOnSalesRepository.GetAll(a => a.OrganizationId == orgId).ToList(); ;
        }

        public bool SaveCommissionOnProductOnSales(AgroProductSalesInfo agroProductSalesInfo, long userId, long orgId)
        {
            bool isSuccess = false;
            CommissionOnProductOnSalesDTO commissionOnProductOnSalesDTO = new CommissionOnProductOnSalesDTO();
            if (commissionOnProductOnSalesDTO.CommissionOnProductOnSalesId == 0)
            {
                CommissionOnProductOnSales commissionOnProductOnSales = new CommissionOnProductOnSales()
                {
                    ProductSalesInfoId = agroProductSalesInfo.ProductSalesInfoId,
                    InvoiceNo = agroProductSalesInfo.InvoiceNo,
                    PaymentMode = agroProductSalesInfo.PaymentMode,
                    Status = "Insert",

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
            if (isSuccess)
            {
                //_commisionOnProductSalesDetailsBusiness.SaveCommisionOnProductSalesDetails();

            }
            return isSuccess;

        }
    }
}
