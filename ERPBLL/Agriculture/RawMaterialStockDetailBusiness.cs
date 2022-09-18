using ERPBLL.Agriculture.Interface;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class RawMaterialStockDetailBusiness:IRawMaterialStockDetail
    {
        private readonly RawMaterialStockDetailRepository _rawMaterialStockDetailRepository;
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;

        public RawMaterialStockDetailBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._rawMaterialStockDetailRepository = new RawMaterialStockDetailRepository(this._agricultureUnitOfWork);
        }
    }
}
