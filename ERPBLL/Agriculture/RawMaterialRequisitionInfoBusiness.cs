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
    public class RawMaterialRequisitionInfoBusiness : IRawMaterialRequisitionInfoBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly IRawMaterialRequisitionDetailsBusiness _rawMaterialRequisitionDetailsBusiness;
        private readonly RawMaterialRequisitionInfoBusinessRepository _rawMaterialRequisitionInfoRepoBusiness;
        private readonly IMRawMaterialIssueStockInfo _mRawMaterialIssueStockInfo;
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

        public IEnumerable<RawMaterialRequisitionInfoDTO> GetAllRawMaterialRequisitionInfos(string RequisitonCode, string status, string fdate,string tdate,long orgId)
        {

            string query = string.Empty;
            string param = string.Empty;

            if (orgId > 0)
            {
                param += string.Format(@"and OrganizationId={0}", orgId);
            }
            if (RequisitonCode != null && RequisitonCode != "")
            {
                param += string.Format(@" and RawMaterialRequisitionCode ='{0}'", RequisitonCode);
            }

            if (status != null && status != "")
            {
                param += string.Format(@" and Status ='{0}'", status);
            }
            if (!string.IsNullOrEmpty(fdate) && fdate.Trim() != "" && !string.IsNullOrEmpty(tdate) && tdate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fdate).ToString("yyyy-MM-dd");
                string tDate = Convert.ToDateTime(tdate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(EntryDate as date) between '{0}' and '{1}'", fDate, tDate);
            }
            else if (!string.IsNullOrEmpty(fdate) && fdate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fdate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(EntryDate as date)='{0}'", fDate);
            }
            else if (!string.IsNullOrEmpty(tdate) && tdate.Trim() != "")
            {
                string tDate = Convert.ToDateTime(tdate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(EntryDate as date)='{0}'", tDate);
            }

       
            query = string.Format(@"select * FROM [dbo].[tblRawMaterialRequisitionInfo]  where 1=1  {0}",
           Utility.ParamChecker(param));

            var allRequistion = _agricultureUnitOfWork.Db.Database.SqlQuery<RawMaterialRequisitionInfoDTO>(string.Format(query));

            return allRequistion;
        }



        public bool SaveRawMaterialRequisition(RawMaterialRequisitionInfoDTO rawMaterialRequisitionInfoDTO, long userId, long orgId)
        {
            bool isSuccess = false;
            string requistionCode = "Req-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
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
                    isSuccess=    _rawMaterialRequisitionDetailsBusiness.SaveRawMaterialRequisitionDetails(rawMaterialRequisitionInfoDTO.rawMaterialRequisitionDetailsDTO,requistionInfoId,userId,orgId);

                    }



                }


            }
            if (rawMaterialRequisitionInfoDTO.RawMaterialRequisitionInfoId>0)
            {

                var Info = this.GetRawMaterialRequisitionInfobyId(rawMaterialRequisitionInfoDTO.RawMaterialRequisitionInfoId,orgId);
                if (rawMaterialRequisitionInfoDTO.flag== "RejectOrReceived")
                {
                    Info.Remarks = "RFP";
                    Info.Status = rawMaterialRequisitionInfoDTO.Status;
                    Info.UpdateDate = DateTime.Now;
                    Info.Type = "Requistion";
                    Info.UpdateUserId = userId;
                    _rawMaterialRequisitionInfoRepoBusiness.Update(Info);
                    isSuccess = _rawMaterialRequisitionInfoRepoBusiness.Save();

                }
                else
                {
                    Info.Remarks = "SFW";
                    Info.Status = "Send";
                    Info.UpdateDate = DateTime.Now;
                    Info.Type = "Requistion";
                    Info.UpdateUserId = userId;
                    _rawMaterialRequisitionInfoRepoBusiness.Update(Info);
                    isSuccess = _rawMaterialRequisitionInfoRepoBusiness.Save();

                }
   




                if (isSuccess)
                {
                   
                    if (rawMaterialRequisitionInfoDTO.rawMaterialRequisitionDetailsDTO.Count() > 0 && rawMaterialRequisitionInfoDTO.RawMaterialRequisitionInfoId > 0)
                    {
                        if (rawMaterialRequisitionInfoDTO.flag == "RejectOrReceived")
                        {
                            isSuccess = _rawMaterialRequisitionDetailsBusiness.UpdateRawMaterialRequisitionDetailsbyProduction(rawMaterialRequisitionInfoDTO.rawMaterialRequisitionDetailsDTO, rawMaterialRequisitionInfoDTO.RawMaterialRequisitionInfoId, userId, orgId);
                            


                        }
                        else
                        {
                            isSuccess = _rawMaterialRequisitionDetailsBusiness.UpdateRawMaterialRequisitionDetails(rawMaterialRequisitionInfoDTO.rawMaterialRequisitionDetailsDTO, rawMaterialRequisitionInfoDTO.RawMaterialRequisitionInfoId, userId, orgId);

                        }
     


                    }



                }


            }



            return isSuccess;
        }
    }
}
