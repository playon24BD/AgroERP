using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.ReportModels;
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
        public RawMaterialRequisitionInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IRawMaterialRequisitionDetailsBusiness rawMaterialRequisitionDetailsBusiness, IMRawMaterialIssueStockInfo mRawMaterialIssueStockInfo)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            
            this._rawMaterialRequisitionInfoRepoBusiness = new RawMaterialRequisitionInfoBusinessRepository(this._agricultureUnitOfWork);

            this._rawMaterialRequisitionDetailsBusiness = rawMaterialRequisitionDetailsBusiness;
            this._mRawMaterialIssueStockInfo = mRawMaterialIssueStockInfo;
        }
        public RawMaterialRequisitionInfo GetRawMaterialRequisitionInfobyId(long infoId, long orgId)
        {
            return _rawMaterialRequisitionInfoRepoBusiness.GetOneByOrg(a=>a.RawMaterialRequisitionInfoId==infoId && a.OrganizationId==orgId);
        }

        //public IEnumerable<RawMaterialRequisitionInfo> GetRawMaterialRequisitionInfos(long orgId)
        //{
        //    return _rawMaterialRequisitionInfoRepoBusiness.GetAll(a =>a.OrganizationId == orgId).ToList();
        //}
        public IEnumerable<RawMaterialRequisitionInfoDTO> GetRawMaterialRequisitionInfoReceives(long orgId, string Status, long RawMaterialRequisitionInfoId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<RawMaterialRequisitionInfoDTO>(QueryForGetRawMaterialRequisitionInfoReceives(orgId, Status, RawMaterialRequisitionInfoId)).ToList();
        }

        private string QueryForGetRawMaterialRequisitionInfoReceives(long orgId, string status, long rawMaterialRequisitionInfoId)
        {
            string query = string.Empty;
            string param = string.Empty;
            if (rawMaterialRequisitionInfoId!=0)
            {
                param += string.Format(@"and RawMaterialRequisitionInfoId ={0}", rawMaterialRequisitionInfoId);
            }
            if (status != null && status != "")
            {
                param += string.Format(@"and Status='{0}'", status);
            }

            query = string.Format(@"	SELECT Top 1 * FROM tblRawMaterialRequisitionInfo where 1=1  {0} and OrganizationId=9 ", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<RawMaterialRequisitionInfoDTO> GetRawMaterialRequisitionInfos(long orgId,string Status)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<RawMaterialRequisitionInfoDTO>(QueryForGetRawMaterialRequisitionInfos(orgId, Status)).ToList();
        }

        private string QueryForGetRawMaterialRequisitionInfos(long orgId,string Status)
        {
            string query = string.Empty;
            string param = string.Empty;
            if (Status!=null && Status!="")
            {
                param += string.Format(@"and Status='{0}'", Status);
            }

            query = string.Format(@"	SELECT Top 1 * FROM tblRawMaterialRequisitionInfo where 1=1  {0} and OrganizationId=9 order by RawMaterialRequisitionInfoId DESC", Utility.ParamChecker(param));

            return query;
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

       
            query = string.Format(@"select * FROM [dbo].[tblRawMaterialRequisitionInfo] R  where 1=1  {0} Order By R.RawMaterialRequisitionInfoId DESC",
           Utility.ParamChecker(param));

            var allRequistion = _agricultureUnitOfWork.Db.Database.SqlQuery<RawMaterialRequisitionInfoDTO>(string.Format(query));

            return allRequistion;
        }



        public bool SaveRawMaterialRequisition(RawMaterialRequisitionInfoDTO rawMaterialRequisitionInfoDTO, long userId, long orgId)
        {
            bool isSuccess = false;
            string requistionCode = "REQ-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
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
                    Info.Status = rawMaterialRequisitionInfoDTO.Status;
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

                            if (isSuccess && rawMaterialRequisitionInfoDTO.Status=="Received")
                            {
                                _mRawMaterialIssueStockInfo.SaveRawMaterialIssueStockWithRequistion(rawMaterialRequisitionInfoDTO, rawMaterialRequisitionInfoDTO.rawMaterialRequisitionDetailsDTO, userId, orgId);
                            }
   

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




        public IEnumerable<GetSendAndReceiveReportData> GetSendAndReceiveReport(string RawMaterialRequisitionCode)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<GetSendAndReceiveReportData>(QueryForSendAndReceiveReport(RawMaterialRequisitionCode));
        }
        
        public string QueryForSendAndReceiveReport(string RawMaterialRequisitionCode)
        {
            string param = string.Empty;
            string query = string.Empty;


            if (!string.IsNullOrEmpty(RawMaterialRequisitionCode))
            {
                param += string.Format(@"and ri.RawMaterialRequisitionCode ='{0}'", RawMaterialRequisitionCode);
            }


            query = string.Format(@" 
        select ri.RawMaterialRequisitionCode, convert(date,rd.EntryDate)as EntryDate,au.FullName,rm.RawMaterialName,rd.IssueQuantity,rd.RequisitionQuantity,rd.Status ,ui.UnitName,ri.Remarks from tblRawMaterialRequistionDetails rd
inner join [ControlPanelAgro].[dbo].tblApplicationUsers au on rd.EntryUserId=au.UserId
inner join tblRawMaterialInfo rm on rd.RawMaterialId=rm.RawMaterialId
inner join tblAgroUnitInfo ui on rd.UnitID=ui.UnitId
inner join tblRawMaterialRequisitionInfo ri on ri.RawMaterialRequisitionInfoId=rd.RawMaterialRequisitionInfoId

where 1=1 {0}", Utility.ParamChecker(param));
            return query;
        }

        public IEnumerable<IssueRequisitionReportData> GetIssueRequisitionReportData(string RawMaterialRequisitionCode)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<IssueRequisitionReportData>(QueryForIssueRequisitionReport(RawMaterialRequisitionCode));
        }

        public string QueryForIssueRequisitionReport(string RawMaterialRequisitionCode)
        {
            string param = string.Empty;
            string query = string.Empty;


            if (!string.IsNullOrEmpty(RawMaterialRequisitionCode))
            {
                param += string.Format(@"and ri.RawMaterialRequisitionCode ='{0}'", RawMaterialRequisitionCode);
            }


            query = string.Format(@" 
        select ri.RawMaterialRequisitionCode, convert(date,rd.EntryDate)as EntryDate,au.FullName,rm.RawMaterialName,rd.IssueQuantity,rd.RequisitionQuantity,rd.Status ,ui.UnitName,ri.Remarks from tblRawMaterialRequistionDetails rd
inner join [ControlPanelAgro].[dbo].tblApplicationUsers au on rd.EntryUserId=au.UserId
inner join tblRawMaterialInfo rm on rd.RawMaterialId=rm.RawMaterialId
inner join tblAgroUnitInfo ui on rd.UnitID=ui.UnitId
inner join tblRawMaterialRequisitionInfo ri on ri.RawMaterialRequisitionInfoId=rd.RawMaterialRequisitionInfoId

where 1=1 {0}", Utility.ParamChecker(param));
            return query;
        }
    }
}
