using ERPBLL.Agriculture.Interface;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class RawMaterialSupplierBusiness:IRawMaterialSupplier
    {
        private readonly IAgricultureUnitOfWork _AgricultureUnitOfWork;
        private readonly RawMaterialSupplierRepository _rawMaterialSupplierRepository;

        public RawMaterialSupplierBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._AgricultureUnitOfWork = agricultureUnitOfWork;
            this._rawMaterialSupplierRepository = new RawMaterialSupplierRepository(this._AgricultureUnitOfWork);
        }
    }
}
