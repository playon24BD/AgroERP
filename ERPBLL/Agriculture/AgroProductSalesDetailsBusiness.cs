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
    public class AgroProductSalesDetailsBusiness : IAgroProductSalesDetailsBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly AgroProductSalesDetailsRepository _agroProductSalesDetailsRepository;
        public AgroProductSalesDetailsBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._agroProductSalesDetailsRepository = new AgroProductSalesDetailsRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<AgroProductSalesDetails> GetAgroSalesDetailsByInfoId(long infoId, long orgId)
        {
            return _agroProductSalesDetailsRepository.GetAll(i => i.OrganizationId == orgId && i.ProductSalesInfoId == infoId).ToList();
        }
    }
}
