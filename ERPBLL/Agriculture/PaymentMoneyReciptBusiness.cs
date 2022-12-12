using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBLL.ControlPanel.Interface;

using ERPBO.Agriculture.DomainModels;

using ERPBO.Agriculture.ReportModels;

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

        public PaymentMoneyReciptBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;

            this._paymentMoneyReciptRepository = new PaymentMoneyReciptRepository(this._agricultureUnitOfWork);

        }


        public IEnumerable<PaymentMoneyRecipt> GetAllPaymentMoneyRecipt()
        {
            return _paymentMoneyReciptRepository.GetAll().ToList();

        }

        public IEnumerable<MoneyReciptRepotData> GetMoneyReciptReport(string moneyReceipt)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<MoneyReciptRepotData>(QueryForMoneyReciptRepot(moneyReceipt)).ToList();
        }

        private string QueryForMoneyReciptRepot(string moneyReceipt)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (!string.IsNullOrEmpty(moneyReceipt))
            {
                param += string.Format(@"and mr.MoneyReciptNo ='{0}'", moneyReceipt);
            }
            query = string.Format(@"


select mr.MoneyReciptNo,info.InvoiceNo,s.StockiestName,mr.EntryDate,mr.BankName,mr.BranchName,ph.PaymentDate,mr.TotalAmount,ph.PaymentAmount,
            dbo.fnIntegerToWords(mr.TotalAmount)+' '+'Taka Only ..........' AS TotalAmountText from tblPaymentMoneyRecipt mr

inner join tblProductSalesPaymentHistory ph on mr.PaymentMoneyReciptId=ph.PaymentMoneyReciptId
inner join tblStockiestInfo s on mr.StockiestId=s.StockiestId
inner join tblProductSalesInfo info on ph.ProductSalesInfoId=info.ProductSalesInfoId




Where 1=1 {0} and mr.MoneyReciptNo='MR-1234'", Utility.ParamChecker(param));

            return query;



        }
    }
}

