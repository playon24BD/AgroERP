using ERPBLL.Agriculture.Interface;
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
    public class RawMaterialRequisitionDetailsBusiness: IRawMaterialRequisitionDetailsBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly RawMaterialRequisitionDetailsBusinessRepository _rawMaterialRequisitionDetailsBusinessRepository;
        public RawMaterialRequisitionDetailsBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._rawMaterialRequisitionDetailsBusinessRepository = new RawMaterialRequisitionDetailsBusinessRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<RawMaterialRequisitionDetails> GetRawMaterialRequisitionDetails(long orgId)
        {
            throw new NotImplementedException();
        }

        public RawMaterialRequisitionDetails GetRawMaterialRequisitionDetailsbyId(long DetailsId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool SaveRawMaterialRequisitionDetails(List<RawMaterialRequisitionDetailsDTO> rawMaterialRequisitionDetailsDTO, long userId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
