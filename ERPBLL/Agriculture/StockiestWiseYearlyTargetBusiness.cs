using ERPBLL.Agriculture.Interface;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class StockiestWiseYearlyTargetBusiness: IStockiestWiseYearlyTarget
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly StockiestWiseYearlyTargetRepository _stockiestWiseYearlyTargetRepository;

        public StockiestWiseYearlyTargetBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._stockiestWiseYearlyTargetRepository = new StockiestWiseYearlyTargetRepository(this._agricultureUnitOfWork);

        }
    }
}
