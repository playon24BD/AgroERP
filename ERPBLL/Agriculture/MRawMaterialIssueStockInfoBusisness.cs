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
    public class MRawMaterialIssueStockInfoBusisness : IMRawMaterialIssueStockInfo
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly MRawMaterialIssueStockInfoRepository _mRawMaterialIssueStockInfoRepository;
        private readonly RawMaterialTrackInfoRepository _rawMaterialTrackInfoRepository;
        private readonly MRawMaterialIssueStockDetailsRepository _mRawMaterialIssueStockDetailsRepository;



        //contractor
        public MRawMaterialIssueStockInfoBusisness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._mRawMaterialIssueStockInfoRepository = new MRawMaterialIssueStockInfoRepository(this._agricultureUnitOfWork);
            this._rawMaterialTrackInfoRepository = new RawMaterialTrackInfoRepository(this._agricultureUnitOfWork);
            this._mRawMaterialIssueStockDetailsRepository = new MRawMaterialIssueStockDetailsRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<MRawMaterialIssueStockInfo> GetAllRawMaterialIssue(long OrgId)
        {
            return _mRawMaterialIssueStockInfoRepository.GetAll(x => x.OrganizationId == OrgId).ToList();
            //return this._agricultureUnitOfWork.Db.Database.SqlQuery<MRawMaterialIssueStockInfoDTO>(QueryForFinishGoodProductInfoss(OrgId)).ToList();
        }

//        private string QueryForFinishGoodProductInfoss(long orgId)
//        {
//            string query = string.Empty;
//            string param = string.Empty;

//           // param += string.Format(@" and infos.OrganizationId={0}", orgId);

//            query = string.Format(@"select infos.FinishGoodProductId, info.FinishGoodProductName,infos.TargetQuantity
//from FinishGoodProductionInfoes infos 
//inner join tblFinishGoodProductInfo info on infos.FinishGoodProductId=info.FinishGoodProductId	
//Where 1=1 {0}", Utility.ParamChecker(param));

//            return query;
//        }

        public MRawMaterialIssueStockInfo GetRawmaterialIssueInfoOneById(long id,  long orgId)
        {
            return _mRawMaterialIssueStockInfoRepository.GetOneByOrg(p => p.RawMaterialIssueStockId == id && p.OrganizationId == orgId);
        }

        public bool SaveRawMaterialIssueStock(MRawMaterialIssueStockInfoDTO info, List<MRawMaterialIssueStockDetailsDTO> details, long userId, long orgId)
        {
            
            bool IsSuccess = false;
            var BatchCode = "BPIS-" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yy") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");


            if (info.RawMaterialIssueStockId == 0)
            {
                MRawMaterialIssueStockInfo model = new MRawMaterialIssueStockInfo
                {
                    ProductBatchCode = BatchCode,
                    type = "NonRequisiton",
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                    OrganizationId = orgId,
                    Status = "InActive",

                };
                _mRawMaterialIssueStockInfoRepository.Insert(model);
                IsSuccess = _mRawMaterialIssueStockInfoRepository.Save();

                var Issueid = GetRawmaterialIssueInfoByProbatch(model.ProductBatchCode).RawMaterialIssueStockId;
                
                List<MRawMaterialIssueStockDetails> modelDetails = new List<MRawMaterialIssueStockDetails>();
                List<RawMaterialTrack> modeltrk = new List<RawMaterialTrack>();//RawMaterialTrackInfo table update

                foreach (var item in details)
                {
                    MRawMaterialIssueStockDetails materialIssueStockDetails = new MRawMaterialIssueStockDetails()
                    {

                        RawMaterialId = item.RawMaterialId,
                        Quantity = item.Quantity,
                        UnitID = item.UnitID,
                        IssueStatus = "PendingStockIn",                  
                        EntryDate = DateTime.Now,

                    };
                    modelDetails.Add(materialIssueStockDetails);


                    //RawMaterialTrackInfo table update
                    RawMaterialTrack rawMaterialTrack = new RawMaterialTrack()
                    {
                        RawMaterialId = item.RawMaterialId,
                        Quantity = item.Quantity,
                        IssueDate = DateTime.Now,
                        EntryDate = DateTime.Now,
                        IssueStatus = "PendingStockOut",
                        type="NonRequisiton",
                        EntryUserId = userId,
                        RawMaterialIssueStockId= Issueid
                    };
                    modeltrk.Add(rawMaterialTrack);

                }

                model.MRawMaterialIssueStockDetails = modelDetails;



                _rawMaterialTrackInfoRepository.InsertAll(modeltrk);
                IsSuccess = _rawMaterialTrackInfoRepository.Save();

            }

            return IsSuccess;
        }

        public bool SaveRawMaterialIssueStockWithRequistion(RawMaterialRequisitionInfoDTO info, List<RawMaterialRequisitionDetailsDTO> details, long userId, long orgId)
        {
            bool IsSuccess = false;
            var BatchCode = "BPIS-" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yy") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");


            if (info.RawMaterialRequisitionInfoId > 0)
            {
                MRawMaterialIssueStockInfo model = new MRawMaterialIssueStockInfo
                {
                    ProductBatchCode = BatchCode,
                    type="Requistion",
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                    OrganizationId = orgId,
                    Status = "Active",

                };
                List<MRawMaterialIssueStockDetails> modelDetails = new List<MRawMaterialIssueStockDetails>();
                List<RawMaterialTrack> modeltrk = new List<RawMaterialTrack>();//RawMaterialTrackInfo table update

                foreach (var item in details)
                {
                    MRawMaterialIssueStockDetails materialIssueStockDetails = new MRawMaterialIssueStockDetails()
                    {

                        RawMaterialId = item.RawMaterialId,
                        Quantity = item.IssueQuantity,
                        UnitID = item.UnitID,
                        IssueStatus = "StockIn",
                        EntryDate = DateTime.Now,

                    };
                    modelDetails.Add(materialIssueStockDetails);


                    //RawMaterialTrackInfo table update
                    RawMaterialTrack rawMaterialTrack = new RawMaterialTrack()
                    {
                        RawMaterialId = item.RawMaterialId,
                        Quantity = item.IssueQuantity,
                        type = "Requistion",
                        IssueDate = DateTime.Now,
                        EntryDate = DateTime.Now,
                        IssueStatus = "StockOut",
                        EntryUserId = userId
                    };
                    modeltrk.Add(rawMaterialTrack);

                }

                model.MRawMaterialIssueStockDetails = modelDetails;

                _mRawMaterialIssueStockInfoRepository.Insert(model);
                IsSuccess = _mRawMaterialIssueStockInfoRepository.Save();

                _rawMaterialTrackInfoRepository.InsertAll(modeltrk);
                IsSuccess = _rawMaterialTrackInfoRepository.Save();

            }

            return IsSuccess;
        }

        public MRawMaterialIssueStockInfo GetRawMaterialIssueStockByMeterialId(long rawMaterialId, long orgId)
        {
            return _mRawMaterialIssueStockInfoRepository.GetOneByOrg(i => i.RawMaterialIssueStockId == rawMaterialId && i.OrganizationId == orgId);
        }

        public IEnumerable<MRawMaterialDataReport> MRawMaterialReport(long? rawMaterialId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<MRawMaterialDataReport>(QueryForMRawMaterialReport(rawMaterialId)).ToList();
        }

        private string QueryForMRawMaterialReport(long? rawMaterialId)
        {
            string query = string.Empty;
            string param = string.Empty;


            if (rawMaterialId != null && rawMaterialId > 0)
            {
                param += string.Format(@" and RM.RawMaterialId={0}", rawMaterialId);
            }

            query = string.Format(@"



SELECT Distinct RM.RawMaterialName,RM.RawMaterialId,un.UnitName,
StockIN=isnull((SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where t.IssueStatus ='StockIn' and t.RawMaterialId=RM.RawMaterialId),0),
StockOut=isnull((SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where  t.IssueStatus ='StockOut'and t.RawMaterialId=RM.RawMaterialId),0),

PendingStock=isnull((SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where  t.IssueStatus ='Pending'and t.RawMaterialId=RM.RawMaterialId),0),

GoodReturn=isnull((SELECT sum(rr.Quantity) FROM  tblReturnRawMaterial rr
where  t.RawMaterialId=rr.RawMaterialId and rr.ReturnType='Good' and rr.Status='Approved' ),0),

BadReturn=isnull((SELECT sum(rr.Quantity) FROM  tblReturnRawMaterial rr
where  t.RawMaterialId=rr.RawMaterialId and rr.ReturnType='Damage'and rr.Status='Approved'),0),

ReturnQty=isnull((SELECT sum(rr.Quantity) FROM  tblReturnRawMaterial rr
where  t.RawMaterialId=rr.RawMaterialId and rr.Status='Approved'),0),

CurrentStock=isnull((SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where t.IssueStatus ='StockIn' and t.RawMaterialId=RM.RawMaterialId ),0)-isnull((SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where  t.IssueStatus ='StockOut'and t.RawMaterialId=RM.RawMaterialId),0)-isnull((SELECT sum(rr.Quantity) FROM  tblReturnRawMaterial rr
where  t.RawMaterialId=rr.RawMaterialId and rr.Status='Approved'),0)-isnull((SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where  t.IssueStatus ='Pending'and t.RawMaterialId=RM.RawMaterialId),0)

FROM  
tblMRawMaterialIssueStockDetails t 
INNER JOIN tblRawMaterialInfo RM on t.RawMaterialId=RM.RawMaterialId
inner join tblAgroUnitInfo un on RM.UnitId = un.UnitId


            Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

       //extra
        public IEnumerable<MRawMaterialIssueStockInfo> GetAllRawMaterialIssueforaccept()
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<MRawMaterialIssueStockInfo>(QueryForissue()).ToList();
        }
        private string QueryForissue()
        {
            string query = string.Empty;
            string param = string.Empty;




            query = string.Format(@"



SELECT Distinct RM.RawMaterialName,RM.RawMaterialId,un.UnitName,
StockIN=isnull((SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where t.IssueStatus ='StockIn' and t.RawMaterialId=RM.RawMaterialId),0),
StockOut=isnull((SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where  t.IssueStatus ='StockOut'and t.RawMaterialId=RM.RawMaterialId),0),

PendingStock=isnull((SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where  t.IssueStatus ='Pending'and t.RawMaterialId=RM.RawMaterialId),0),

GoodReturn=isnull((SELECT sum(rr.Quantity) FROM  tblReturnRawMaterial rr
where  t.RawMaterialId=rr.RawMaterialId and rr.ReturnType='Good' and rr.Status='Approved' ),0),

BadReturn=isnull((SELECT sum(rr.Quantity) FROM  tblReturnRawMaterial rr
where  t.RawMaterialId=rr.RawMaterialId and rr.ReturnType='Damage'and rr.Status='Approved'),0),

ReturnQty=isnull((SELECT sum(rr.Quantity) FROM  tblReturnRawMaterial rr
where  t.RawMaterialId=rr.RawMaterialId and rr.Status='Approved'),0),

CurrentStock=isnull((SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where t.IssueStatus ='StockIn' and t.RawMaterialId=RM.RawMaterialId ),0)-isnull((SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where  t.IssueStatus ='StockOut'and t.RawMaterialId=RM.RawMaterialId),0)-isnull((SELECT sum(rr.Quantity) FROM  tblReturnRawMaterial rr
where  t.RawMaterialId=rr.RawMaterialId and rr.Status='Approved'),0)-isnull((SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where  t.IssueStatus ='Pending'and t.RawMaterialId=RM.RawMaterialId),0)

FROM  
tblMRawMaterialIssueStockDetails t 
INNER JOIN tblRawMaterialInfo RM on t.RawMaterialId=RM.RawMaterialId
inner join tblAgroUnitInfo un on RM.UnitId = un.UnitId


            Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }



        public MRawMaterialIssueStockInfo GetRawmaterialIssueInfoByProbatch(string ProductBatchCode)
        {
            return _mRawMaterialIssueStockInfoRepository.GetOneByOrg(d=>d.ProductBatchCode== ProductBatchCode);
        }

        public bool UpdateRawMaterialIssueStock(MRawMaterialIssueStockInfoDTO info, List<MRawMaterialIssueStockDetailsDTO> details, long userId)
        {
            bool IsSuccess = false;
            if(info.Status == "Approved")
            {


                MRawMaterialIssueStockInfo mRawMaterialIssueStockInfo = new MRawMaterialIssueStockInfo();
                mRawMaterialIssueStockInfo = GetRawmaterialIssueInfoOneById(info.RawMaterialIssueStockId, 9);
                mRawMaterialIssueStockInfo.Status = "Active";
                _mRawMaterialIssueStockInfoRepository.Update(mRawMaterialIssueStockInfo);
                if (_mRawMaterialIssueStockInfoRepository.Save())
                {
                    List<MRawMaterialIssueStockDetails> mRawMaterialIssueStockDetails = new List<MRawMaterialIssueStockDetails>();
                    MRawMaterialIssueStockDetails mRawMaterialIssueStockDetails1 = new MRawMaterialIssueStockDetails();
                    foreach (var item in details)
                    {
                        mRawMaterialIssueStockDetails1 = GetRawmaterialIssueDetailsByISSueadndRMid(item.RawMaterialIssueStockId, item.RawMaterialId);
                        mRawMaterialIssueStockDetails1.IssueStatus = "StockIn";
                        mRawMaterialIssueStockDetails.Add(mRawMaterialIssueStockDetails1);
                    }
                    _mRawMaterialIssueStockDetailsRepository.UpdateAll(mRawMaterialIssueStockDetails);
                }
                if (_mRawMaterialIssueStockDetailsRepository.Save())
                {
                   List<RawMaterialTrack> rawMaterialTracks= new List<RawMaterialTrack>();
                   RawMaterialTrack rawMaterialTrack = new RawMaterialTrack();
                    foreach(var track in details)
                    {
                        rawMaterialTrack = GetRawmaterialTrkDetailsByISSueadndRMid(track.RawMaterialIssueStockId,track.RawMaterialId);
                        rawMaterialTrack.IssueStatus = "StockOut";
                        rawMaterialTracks.Add(rawMaterialTrack);
                    }
                    _rawMaterialTrackInfoRepository.UpdateAll(rawMaterialTracks);

                }
                IsSuccess = _rawMaterialTrackInfoRepository.Save();

                return IsSuccess;
            }
            else if (info.Status == "Rejected")
            {
                MRawMaterialIssueStockInfo mRawMaterialIssueStockInfo = new MRawMaterialIssueStockInfo();
                mRawMaterialIssueStockInfo = GetRawmaterialIssueInfoOneById(info.RawMaterialIssueStockId, 9);
                mRawMaterialIssueStockInfo.Status = "Reject";
                _mRawMaterialIssueStockInfoRepository.Update(mRawMaterialIssueStockInfo);
                if (_mRawMaterialIssueStockInfoRepository.Save())
                {
                    List<MRawMaterialIssueStockDetails> mRawMaterialIssueStockDetails = new List<MRawMaterialIssueStockDetails>();
                    MRawMaterialIssueStockDetails mRawMaterialIssueStockDetails1 = new MRawMaterialIssueStockDetails();
                    foreach (var item in details)
                    {
                        mRawMaterialIssueStockDetails1 = GetRawmaterialIssueDetailsByISSueadndRMid(item.RawMaterialIssueStockId, item.RawMaterialId);
                        mRawMaterialIssueStockDetails1.IssueStatus = "Reject";
                        mRawMaterialIssueStockDetails.Add(mRawMaterialIssueStockDetails1);
                    }
                    _mRawMaterialIssueStockDetailsRepository.UpdateAll(mRawMaterialIssueStockDetails);
                }
                if (_mRawMaterialIssueStockDetailsRepository.Save())
                {
                    List<RawMaterialTrack> rawMaterialTracks = new List<RawMaterialTrack>();
                    RawMaterialTrack rawMaterialTrack = new RawMaterialTrack();
                    foreach (var track in details)
                    {
                        rawMaterialTrack = GetRawmaterialTrkDetailsByISSueadndRMid(track.RawMaterialIssueStockId, track.RawMaterialId);
                        rawMaterialTrack.IssueStatus = "Reject";
                        rawMaterialTracks.Add(rawMaterialTrack);
                    }
                    _rawMaterialTrackInfoRepository.UpdateAll(rawMaterialTracks);

                }
                IsSuccess = _rawMaterialTrackInfoRepository.Save();

                return IsSuccess;

            }
            else
            {
                return IsSuccess;
            }

        }

        public MRawMaterialIssueStockDetails GetRawmaterialIssueDetailsByISSueadndRMid(long RawMaterialIssueStockId, long RawMaterialId)
        {
            return _mRawMaterialIssueStockDetailsRepository.GetOneByOrg(c=>c.RawMaterialIssueStockId== RawMaterialIssueStockId && c.RawMaterialId== RawMaterialId);
        }

        public RawMaterialTrack GetRawmaterialTrkDetailsByISSueadndRMid(long RawMaterialIssueStockId, long RawMaterialId)
        {
            return _rawMaterialTrackInfoRepository.GetOneByOrg(p=>p.RawMaterialIssueStockId== RawMaterialIssueStockId && p.RawMaterialId== RawMaterialId);
        }
    }
}
