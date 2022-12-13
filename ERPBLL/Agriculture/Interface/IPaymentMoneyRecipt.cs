
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


        IEnumerable<MoneyReciptRepotData> GetMoneyReciptReport(string moneyReceipt);
        IEnumerable<PaymentMoneyReciptDTO> GetLastMoneyRecipt(long orgId);

    }
}
