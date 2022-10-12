using ERPBLL.Agriculture.Interface;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class StockiestInfoBusiness:IStockiestInfo
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly StockiestInfoRepository _stockiestInfoRepository;
        public StockiestInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._stockiestInfoRepository = new StockiestInfoRepository(this._agricultureUnitOfWork);
        }
    }
}
