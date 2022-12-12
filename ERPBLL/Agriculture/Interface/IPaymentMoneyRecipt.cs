
﻿using ERPBO.Agriculture.DomainModels;

﻿using ERPBO.Agriculture.ReportModels;

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


        IEnumerable<MoneyReciptRepotData> GetMoneyReciptReport(string moneyReceipt);

    }
}
