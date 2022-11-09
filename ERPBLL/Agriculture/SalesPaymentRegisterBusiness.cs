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
    public class SalesPaymentRegisterBusiness : ISalesPaymentRegister
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly SalesPaymentRegisterRepository _salesPaymentRegisterRepository;
        private readonly AgroProductSalesInfoRepository _agroProductSalesInfoRepository;

        private readonly IAgroProductSalesInfoBusiness _agroProductSalesInfoBusiness;
        private readonly ICommissionOnProductOnSalesBusiness _commissionOnProductOnSalesBusiness;


        public SalesPaymentRegisterBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IAgroProductSalesInfoBusiness agroProductSalesInfoBusiness, ICommissionOnProductOnSalesBusiness commissionOnProductOnSalesBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._salesPaymentRegisterRepository = new SalesPaymentRegisterRepository(this._agricultureUnitOfWork);
            this._agroProductSalesInfoRepository = new AgroProductSalesInfoRepository(this._agricultureUnitOfWork);

            this._agroProductSalesInfoBusiness = agroProductSalesInfoBusiness;
            this._commissionOnProductOnSalesBusiness = commissionOnProductOnSalesBusiness;
        }

        public IEnumerable<SalesPaymentRegister> GetPaymentDetailsByInvoiceId(long infoId)
        {
            return _salesPaymentRegisterRepository.GetAll(i => i.ProductSalesInfoId == infoId ).ToList();

        }

        public bool SaveSalesPayment(SalesPaymentRegisterDTO info, long userId)
        {
            bool IsSuccess = false;

            if (info.PaymentRegisterID == 0)
            {
                SalesPaymentRegister model = new SalesPaymentRegister
                {

                    PaymentAmount = info.PaymentAmount,
                    PaymentDate = DateTime.Now,
                    Remarks = info.Remarks,
                    ProductSalesInfoId = info.ProductSalesInfoId,
                    EntryUserId = userId,
                    AccounrNumber = info.AccounrNumber,
                    PaymentMode = info.PaymentMode
                    

                };
                _salesPaymentRegisterRepository.Insert(model);
                IsSuccess = _salesPaymentRegisterRepository.Save();
                //
                if (IsSuccess)
                {
                    var salesPayment = _agroProductSalesInfoBusiness.CheckBYProductSalesInfoId(info.ProductSalesInfoId);
                    salesPayment.PaidAmount += model.PaymentAmount;
                    salesPayment.DueAmount -= model.PaymentAmount;

                    _agroProductSalesInfoRepository.Update(salesPayment);
                    IsSuccess = _agroProductSalesInfoRepository.Save();
                    var dateDif = (DateTime.Now.Date - salesPayment.InvoiceDate.Value.Date).Days;
                    if (dateDif < 4 && salesPayment.DueAmount==0)
                    {

                        _commissionOnProductOnSalesBusiness.UpdateCommissionOnProductOnSales(salesPayment, userId, salesPayment.OrganizationId);

                    }
                }




            }
         



            return IsSuccess;
        }
    }
}
