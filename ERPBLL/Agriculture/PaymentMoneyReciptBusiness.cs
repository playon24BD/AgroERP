using ERPBLL.Agriculture.Interface;
using ERPBLL.ControlPanel.Interface;
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
    public class PaymentMoneyReciptBusiness : IPaymentMoneyRecipt
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly PaymentMoneyReciptRepository _paymentMoneyReciptRepository;
        private readonly SalesPaymentRegisterRepository _salesPaymentRegisterRepository;
        private readonly AgroProductSalesInfoRepository _agroProductSalesInfoRepository;
        private readonly IAgroProductSalesInfoBusiness _agroProductSalesInfoBusiness;
        private readonly ICommissionOnProductOnSalesBusiness _commissionOnProductOnSalesBusiness;

        public PaymentMoneyReciptBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IAgroProductSalesInfoBusiness agroProductSalesInfoBusiness, ICommissionOnProductOnSalesBusiness commissionOnProductOnSalesBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._agroProductSalesInfoRepository = new AgroProductSalesInfoRepository(this._agricultureUnitOfWork);
            this._agroProductSalesInfoBusiness = agroProductSalesInfoBusiness;
            this._paymentMoneyReciptRepository = new PaymentMoneyReciptRepository(this._agricultureUnitOfWork);
            this._salesPaymentRegisterRepository = new SalesPaymentRegisterRepository(this._agricultureUnitOfWork);
            this._commissionOnProductOnSalesBusiness = commissionOnProductOnSalesBusiness;
        }

        public IEnumerable<PaymentMoneyRecipt> GetAllPaymentMoneyRecipt()
        {
            return _paymentMoneyReciptRepository.GetAll().ToList();
        }

        public bool SavePaymentMOneyReciept(PaymentMoneyReciptDTO infoDTO, List<SalesPaymentRegisterDTO> detailsDTO, long userId, long OrgId)
        {
            bool isSucccess = false;
            double total = 0;
            foreach (var bom in detailsDTO)
            {
                double subtotal = bom.PaymentAmount;
                total += subtotal;
            }
            PaymentMoneyRecipt paymentMoneyRecipt = new PaymentMoneyRecipt();

            List<SalesPaymentRegister> salesPaymentRegisters = new List<SalesPaymentRegister>();


            paymentMoneyRecipt.PaymentMoneyReciptId = infoDTO.PaymentMoneyReciptId;
            paymentMoneyRecipt.MoneyReciptNo = infoDTO.MoneyReciptNo;
            paymentMoneyRecipt.StockiestId = infoDTO.StockiestId;
            paymentMoneyRecipt.TotalAmount = total;
            paymentMoneyRecipt.EntryDate = DateTime.Now;
            paymentMoneyRecipt.BankName = infoDTO.BankName;
            paymentMoneyRecipt.BranchName = infoDTO.BranchName;
            paymentMoneyRecipt.PaymentMode = infoDTO.PaymentMode;
            paymentMoneyRecipt.AccounrNumber = infoDTO.AccounrNumber;
            paymentMoneyRecipt.EntryUserId = userId;
            _paymentMoneyReciptRepository.Insert(paymentMoneyRecipt);
            isSucccess = _paymentMoneyReciptRepository.Save();

            foreach (var item in detailsDTO)
            {
                if (item.PaymentAmount > 0)
                {
                    if (item.CommisionPercent > 0)
                    {
                        SalesPaymentRegister salesPaymentRegister = new SalesPaymentRegister()
                        {
                            PaymentAmount = item.PaymentAmount,
                            ProductSalesInfoId = item.ProductSalesInfoId,
                            PaymentMode = infoDTO.PaymentMode,
                            AccounrNumber = infoDTO.AccounrNumber,
                            PaymentMoneyReciptId = paymentMoneyRecipt.PaymentMoneyReciptId,
                            EntryUserId = userId,
                            PaymentDate = DateTime.Now,
                            CommisionPercent = item.CommisionPercent,
                            CommisionAmount = (item.PaymentAmount * item.CommisionPercent)/100,

                        };
                        _salesPaymentRegisterRepository.Insert(salesPaymentRegister);

                        var salesPayment = _agroProductSalesInfoBusiness.CheckBYProductSalesInfoId(item.ProductSalesInfoId);
                        salesPayment.PaidAmount += item.PaymentAmount;
                        salesPayment.DueAmount -= item.PaymentAmount;
                        _agroProductSalesInfoRepository.Update(salesPayment);

                    }
                    else
                    {
                        SalesPaymentRegister salesPaymentRegister = new SalesPaymentRegister()
                        {
                            PaymentAmount = item.PaymentAmount,
                            ProductSalesInfoId = item.ProductSalesInfoId,
                            PaymentMode = infoDTO.PaymentMode,
                            AccounrNumber = infoDTO.AccounrNumber,
                            PaymentMoneyReciptId = paymentMoneyRecipt.PaymentMoneyReciptId,
                            EntryUserId = userId,
                            PaymentDate = DateTime.Now,
                            CommisionPercent = 0,
                            CommisionAmount = 0,
                       

                        };
                        _salesPaymentRegisterRepository.Insert(salesPaymentRegister);

                        var salesPayment = _agroProductSalesInfoBusiness.CheckBYProductSalesInfoId(item.ProductSalesInfoId);
                        salesPayment.PaidAmount += item.PaymentAmount;
                        salesPayment.DueAmount -= item.PaymentAmount;
                        _agroProductSalesInfoRepository.Update(salesPayment);

                    }

                }
            }
            isSucccess = _salesPaymentRegisterRepository.Save();




            return isSucccess;
        }
    }
}
