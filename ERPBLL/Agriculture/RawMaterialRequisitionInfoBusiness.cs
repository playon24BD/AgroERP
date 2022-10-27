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
    public class RawMaterialRequisitionInfoBusiness : IRawMaterialRequisitionInfoBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly RawMaterialRequisitionInfoBusinessRepository _rawMaterialRequisitionInfoRepoBusiness;
        public RawMaterialRequisitionInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._rawMaterialRequisitionInfoRepoBusiness = new RawMaterialRequisitionInfoBusinessRepository(this._agricultureUnitOfWork);
        }
        public RawMaterialRequisitionInfo GetRawMaterialRequisitionInfobyId(long infoId, long orgId)
        {
            return _rawMaterialRequisitionInfoRepoBusiness.GetOneByOrg(a=>a.RawMaterialRequisitionInfoId==infoId && a.OrganizationId==orgId);
        }

        public IEnumerable<RawMaterialRequisitionInfo> GetRawMaterialRequisitionInfos(long orgId)
        {
            return _rawMaterialRequisitionInfoRepoBusiness.GetAll(a =>a.OrganizationId == orgId).ToList();
        }

        public bool SaveRawMaterialRequisition(List<RawMaterialRequisitionInfoDTO> rawMaterialRequisitionInfoDTO, long userId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
