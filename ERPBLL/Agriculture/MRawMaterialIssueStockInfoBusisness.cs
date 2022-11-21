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


        //contractor
        public MRawMaterialIssueStockInfoBusisness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._mRawMaterialIssueStockInfoRepository = new MRawMaterialIssueStockInfoRepository(this._agricultureUnitOfWork);
            this._rawMaterialTrackInfoRepository = new RawMaterialTrackInfoRepository(this._agricultureUnitOfWork);
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
                    Status = "Active",

                };
                List<MRawMaterialIssueStockDetails> modelDetails = new List<MRawMaterialIssueStockDetails>();
                List<RawMaterialTrack> modeltrk = new List<RawMaterialTrack>();//RawMaterialTrackInfo table update

                foreach (var item in details)
                {
                    MRawMaterialIssueStockDetails materialIssueStockDetails = new MRawMaterialIssueStockDetails()
                    {

                        RawMaterialId = item.RawMaterialId,
                        Quantity = item.Quantity,
                        UnitID = item.UnitID,
                        IssueStatus = "StockIn",                  
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
                        IssueStatus = "StockOut",
                        type="NonRequisiton",
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

        public IEnumerable<MRawMaterialDataReport> MRawMaterialReport()
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<MRawMaterialDataReport>(QueryForMRawMaterialReport()).ToList();
        }

        private string QueryForMRawMaterialReport()
        {
            string query = string.Empty;
            string param = string.Empty;

            

            query = string.Format(@"



SELECT Distinct RM.RawMaterialName,t.RawMaterialId,un.UnitName,
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
    }
}
