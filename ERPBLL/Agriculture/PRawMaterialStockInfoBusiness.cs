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

    public class PRawMaterialStockInfoBusiness : IPRawMaterialStockInfo
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly PRawMaterialStockInfoRepository  _pRawMaterialStockInfoRepository;


        //contractor
        public PRawMaterialStockInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._pRawMaterialStockInfoRepository = new PRawMaterialStockInfoRepository(this._agricultureUnitOfWork);
        }


        public IEnumerable<PRawMaterialStockInfo> GetAllPRawMaterialStockInfo(long OrgId)
        {
            throw new NotImplementedException();
        }
    }
}
