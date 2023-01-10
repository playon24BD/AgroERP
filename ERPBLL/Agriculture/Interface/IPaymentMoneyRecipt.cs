
﻿using ERPBO.Agriculture.DomainModels;

﻿using ERPBO.Agriculture.ReportModels;

using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IPaymentMoneyRecipt
    {

        IEnumerable<PaymentMoneyRecipt> GetAllPaymentMoneyRecipt();
        bool SavePaymentMOneyReciept(PaymentMoneyReciptDTO infoDTO, List<SalesPaymentRegisterDTO> detailsDTO, long userId, long OrgId);
        bool SavePaymentMOneyRecieptApprove(PaymentMoneyReciptDTO infoDTO, List<SalesPaymentRegisterDTO> detailsDTO, long userId);


        IEnumerable<MoneyReciptRepotData> GetMoneyReciptReport(string moneyReceipt);
        IEnumerable<PaymentMoneyReciptDTO> GetLastMoneyRecipt(long orgId);
        IEnumerable<PaymentMoneyReciptDTO> GetMoneyReceiptList(string moneyReceiptNo, long? stockiestId, string fromDate, string toDate);
        IEnumerable<PaymentMoneyReciptDTO> GetMoneyReceiptListApprove(string moneyReceiptNo, long? stockiestId, string fromDate, string toDate);
        IEnumerable<SalesPaymentRegisterDTO> GetMoneyReceiptById(long id, long orgId); 

    }
}
