using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class PRawMaterialStockIDetailsBusiness : IPRawMaterialStockIDetails
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly PRawMaterialStockIDetailsRepository _pRawMaterialStockIDetailsRepository;


        //contractor
        public PRawMaterialStockIDetailsBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._pRawMaterialStockIDetailsRepository = new PRawMaterialStockIDetailsRepository(this._agricultureUnitOfWork);
        }



        public IEnumerable<PRawMaterialStockIDetails> GetAllPRawMaterialStockIDetails(long OrgId)
        {
            throw new NotImplementedException();
        }
    }
}
