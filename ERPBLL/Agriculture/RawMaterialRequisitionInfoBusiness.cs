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
        private readonly IRawMaterialRequisitionDetailsBusiness _rawMaterialRequisitionDetailsBusiness;
        private readonly RawMaterialRequisitionInfoBusinessRepository _rawMaterialRequisitionInfoRepoBusiness;
        public RawMaterialRequisitionInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IRawMaterialRequisitionDetailsBusiness rawMaterialRequisitionDetailsBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            
            this._rawMaterialRequisitionInfoRepoBusiness = new RawMaterialRequisitionInfoBusinessRepository(this._agricultureUnitOfWork);

            this._rawMaterialRequisitionDetailsBusiness = rawMaterialRequisitionDetailsBusiness;
        }
        public RawMaterialRequisitionInfo GetRawMaterialRequisitionInfobyId(long infoId, long orgId)
        {
            return _rawMaterialRequisitionInfoRepoBusiness.GetOneByOrg(a=>a.RawMaterialRequisitionInfoId==infoId && a.OrganizationId==orgId);
        }

        public IEnumerable<RawMaterialRequisitionInfo> GetRawMaterialRequisitionInfos(long orgId)
        {
            return _rawMaterialRequisitionInfoRepoBusiness.GetAll(a =>a.OrganizationId == orgId).ToList();
        }

        public IEnumerable<RawMaterialRequisitionInfoDTO> GetAllRawMaterialRequisitionInfos(string RequisitonCode, string fdate,string tdate,long orgId)
        {

            string query = string.Empty;
            string param = string.Empty;

            var allRequistion = _agricultureUnitOfWork.Db.Database.SqlQuery<RawMaterialRequisitionInfoDTO>(string.Format(""));

            return allRequistion;
        }



        public bool SaveRawMaterialRequisition(RawMaterialRequisitionInfoDTO rawMaterialRequisitionInfoDTO, long userId, long orgId)
        {
            bool isSuccess = false;
            string requistionCode = "Req-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss"); ;
            if (rawMaterialRequisitionInfoDTO.RawMaterialRequisitionInfoId==0)
            {
                RawMaterialRequisitionInfo rawMaterialRequisitionInfo = new RawMaterialRequisitionInfo();
                rawMaterialRequisitionInfo.RawMaterialRequisitionCode = requistionCode;
                rawMaterialRequisitionInfo.Status = "Pending";
                rawMaterialRequisitionInfo.Remarks = "RFP";
                rawMaterialRequisitionInfo.EntryDate = DateTime.Now;
                rawMaterialRequisitionInfo.EntryUserId = userId;
                rawMaterialRequisitionInfo.OrganizationId = orgId;

                _rawMaterialRequisitionInfoRepoBusiness.Insert(rawMaterialRequisitionInfo);
               isSuccess= _rawMaterialRequisitionInfoRepoBusiness.Save();
                if (isSuccess)
                {
                    long requistionInfoId = rawMaterialRequisitionInfo.RawMaterialRequisitionInfoId;
                    if (rawMaterialRequisitionInfoDTO.rawMaterialRequisitionDetailsDTO.Count()>0 && requistionInfoId>0)
                    {
                        //List<RawMaterialRequisitionDetailsDTO> detailsDTOs = new List<RawMaterialRequisitionDetailsDTO>();

                        //foreach (var id in rawMaterialRequisitionInfoDTO.rawMaterialRequisitionDetailsDTO)
                        //{
                        //    id.RawMaterialRequisitionInfoId = requistionInfoId;
                        //}
                        //detailsDTOs.Add();
                        _rawMaterialRequisitionDetailsBusiness.SaveRawMaterialRequisitionDetails(rawMaterialRequisitionInfoDTO.rawMaterialRequisitionDetailsDTO,requistionInfoId,userId,orgId);

                    }



                }


            }


            return isSuccess;
        }
    }
}
