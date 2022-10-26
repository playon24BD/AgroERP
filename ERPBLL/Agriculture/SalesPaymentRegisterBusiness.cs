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


        public SalesPaymentRegisterBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IAgroProductSalesInfoBusiness agroProductSalesInfoBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._salesPaymentRegisterRepository = new SalesPaymentRegisterRepository(this._agricultureUnitOfWork);
            this._agroProductSalesInfoRepository = new AgroProductSalesInfoRepository(this._agricultureUnitOfWork);

            this._agroProductSalesInfoBusiness = agroProductSalesInfoBusiness;
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
                    EntryUserId = userId
                    

                };
                _salesPaymentRegisterRepository.Insert(model);
                IsSuccess = _salesPaymentRegisterRepository.Save();


                var salesPayment = _agroProductSalesInfoBusiness.CheckBYProductSalesInfoId(info.ProductSalesInfoId);
                salesPayment.PaidAmount += model.PaymentAmount;
                salesPayment.DueAmount -= model.PaymentAmount;
                _agroProductSalesInfoRepository.Update(salesPayment);
                IsSuccess = _agroProductSalesInfoRepository.Save();
                //var IssueRawMaterialStock = RawMaterialStockInfoCheckValues(item.RawMaterialId, orgId);

                //IssueRawMaterialStock.Quantity += item.Quantity;
                //_rawMaterialIssueStockInfoRepository.Update(IssueRawMaterialStock);

                //var wrkmng = _context.NewWorks.Where(x => x.WorkId == newWork.WorkId).FirstOrDefault();

                //var SalesID = info.ProductSalesInfoId;
                //var mainsalesid = _agroProductSalesInfoBusiness.GetAgroProductionInfoById().where


            }
         

            //_rawMaterialTrackInfoRepository.InsertAll(modeltrk);
            //IsSuccess = _rawMaterialTrackInfoRepository.Save();

            return IsSuccess;
        }
    }
}
