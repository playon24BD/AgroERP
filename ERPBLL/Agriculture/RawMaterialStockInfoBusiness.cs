using ERPBLL.Agriculture.Interface;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class RawMaterialStockInfoBusiness:IRawMaterialStockInfo
    {
        private readonly RawMaterialStockInfoRepository _rawMaterialStockInfoRepository;
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;

        public RawMaterialStockInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._rawMaterialStockInfoRepository = new RawMaterialStockInfoRepository(this._agricultureUnitOfWork);
        }
    }
}
