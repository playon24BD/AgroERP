using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
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



        public IEnumerable<PRawMaterialStockIDetails> GetRawMatwrialPurchaseDetailsByInfoId(long infoId)
        {
            return _pRawMaterialStockIDetailsRepository.GetAll(i => i.PRawMaterialStockId == infoId).ToList();
           
        }
    }
}
