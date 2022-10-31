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
            var requisitionDetails = _rawMaterialRequisitionDetailsBusinessRepository.GetAll(a=>a.OrganizationId==orgId);
            return requisitionDetails.ToList();
        }

        public RawMaterialRequisitionDetails GetRawMaterialRequisitionDetailsbyId(long DetailsId, long orgId)
        {
            var SingleRequisitonDetails = _rawMaterialRequisitionDetailsBusinessRepository.GetOneByOrg(d=>d.RawMaterialRequisitionDetailsId==DetailsId && d.OrganizationId==orgId);
            return SingleRequisitonDetails;
        }

        public IEnumerable<RawMaterialRequisitionDetails> GetRawMaterialRequisitionDetailsbyInfo(long InfoId, long orgId)
        {
            var requisitionDetailsbyInfo = _rawMaterialRequisitionDetailsBusinessRepository.GetAll(a=>a.RawMaterialRequisitionInfoId==InfoId && a.OrganizationId == orgId);
            return requisitionDetailsbyInfo.ToList() ;
        }

        public bool SaveRawMaterialRequisitionDetails(List<RawMaterialRequisitionDetailsDTO> rawMaterialRequisitionDetailsDTO,long infoId, long userId, long orgId)
        {
            bool IsSuccess = false;
            if (rawMaterialRequisitionDetailsDTO.Count()>0 && infoId>0)
            {
              
              List< RawMaterialRequisitionDetails> requisitionDetailList = new List<RawMaterialRequisitionDetails>();
                foreach (var requisitionMaterial in rawMaterialRequisitionDetailsDTO)
                {
                    RawMaterialRequisitionDetails requisitionDetails = new RawMaterialRequisitionDetails
                    {
                        RawMaterialRequisitionInfoId = infoId,
                        RawMaterialId = requisitionMaterial.RawMaterialId,
                        UnitID= requisitionMaterial.UnitID,
                        RequisitionQuantity = requisitionMaterial.RequisitionQuantity,
                        IssueQuantity = 0,
                        Remarks = requisitionMaterial.Remarks,
                        Status = "Pending",
                        EntryDate = DateTime.Now,
                        EntryUserId = userId,
                        OrganizationId = orgId

                    };


                    requisitionDetailList.Add(requisitionDetails);
                }
                if (requisitionDetailList.Count()>0)
                {
                    _rawMaterialRequisitionDetailsBusinessRepository.InsertAll(requisitionDetailList);
                  IsSuccess=  _rawMaterialRequisitionDetailsBusinessRepository.Save();
                }

            }


            return IsSuccess;
        }
    }
}
