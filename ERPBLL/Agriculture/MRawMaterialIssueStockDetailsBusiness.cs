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
    public class MRawMaterialIssueStockDetailsBusiness : IMRawMaterialIssueStockDetails
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly MRawMaterialIssueStockDetailsRepository _mRawMaterialIssueStockDetailsRepository;


        //contractor
        public MRawMaterialIssueStockDetailsBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._mRawMaterialIssueStockDetailsRepository = new MRawMaterialIssueStockDetailsRepository(this._agricultureUnitOfWork);
        }


        public IEnumerable<MRawMaterialIssueStockDetails> GetAllRawMaterialIssueStock()
        {
            return _mRawMaterialIssueStockDetailsRepository.GetAll().ToList();
        }

        public IEnumerable<MRawMaterialIssueStockDetailsDTO> GetIssueInOutInfos(string name)
        {
            //return this._agricultureUnitOfWork.Db.Database.SqlQuery<RegionSetupDTO>(QueryForRegion(orgId, name, regionId, divisionId)).ToList();
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<MRawMaterialIssueStockDetailsDTO>(QueryForRawMaterialIssueINOUTQTY(name)).ToList();
        }
        private string QueryForRawMaterialIssueINOUTQTY(string name)
        {
            string query = string.Empty;
            string param = string.Empty;

         
            if (name != null && name != "")
            {
                param += string.Format(@" and RM.RawMaterialName like '%{0}%'", name);
            }
            query = string.Format(@"
     
 SELECT Distinct RM.RawMaterialName,t.RawMaterialId,un.UnitName,
StockIN=isnull((SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where t.IssueStatus ='StockIn' and t.RawMaterialId=RM.RawMaterialId),0),
StockOut=isnull((SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where  t.IssueStatus ='StockOut'and t.RawMaterialId=RM.RawMaterialId),0),

GoodReturn=isnull((SELECT sum(rr.Quantity) FROM  tblReturnRawMaterial rr
where  t.RawMaterialId=rr.RawMaterialId and rr.ReturnType='Good' and rr.Status='Approved' ),0),

BadReturn=isnull((SELECT sum(rr.Quantity) FROM  tblReturnRawMaterial rr
where  t.RawMaterialId=rr.RawMaterialId and rr.ReturnType='Damage'and rr.Status='Approved'),0),

ReturnQty=isnull((SELECT sum(rr.Quantity) FROM  tblReturnRawMaterial rr
where  t.RawMaterialId=rr.RawMaterialId and rr.Status='Approved'),0),

CurrentStock=isnull((SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where t.IssueStatus ='StockIn' and t.RawMaterialId=RM.RawMaterialId ),0)-isnull((SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where  t.IssueStatus ='StockOut'and t.RawMaterialId=RM.RawMaterialId),0)-isnull((SELECT sum(rr.Quantity) FROM  tblReturnRawMaterial rr
where  t.RawMaterialId=rr.RawMaterialId and rr.Status='Approved'),0)

FROM  
tblMRawMaterialIssueStockDetails t 
INNER JOIN tblRawMaterialInfo RM on t.RawMaterialId=RM.RawMaterialId
inner join tblAgroUnitInfo un on RM.UnitId = un.UnitId
      where 1=1  {0}",
            Utility.ParamChecker(param));
            return query;
        }

     
        public IEnumerable<MRawMaterialIssueStockDetails> GetRawMatwrialissueDetailsByInfoId(long infoId, string Status)
        {
            return _mRawMaterialIssueStockDetailsRepository.GetAll(i => i.RawMaterialIssueStockId == infoId && i.IssueStatus == "StockIn").ToList();

        }


       

        public IEnumerable<MRawMaterialIssueStockDetails> RawMaterialStockIssueInfobyRawMaterialid(long rawMaterialId, long orgId)
        {
            //return _mRawMaterialIssueStockDetailsRepository.GetAll(f => f.RawMaterialId == rawMaterialId);
            return _mRawMaterialIssueStockDetailsRepository.GetAll(i => i.RawMaterialId == rawMaterialId && i.IssueStatus == "StockIn");
        }
        public IEnumerable<MRawMaterialIssueStockDetails> RawMaterialStockIssueInfobyRawMaterialidOut(long rawMaterialId, long orgId)
        {
            //return _mRawMaterialIssueStockDetailsRepository.GetAll(f => f.RawMaterialId == rawMaterialId);
            return _mRawMaterialIssueStockDetailsRepository.GetAll(i => i.RawMaterialId == rawMaterialId && i.IssueStatus == "StockOut");
        }

        public bool SaveRawMaterialIssueDetails(List<MRawMaterialIssueStockDetailsDTO> rawMaterialIssueStockDetailsDTOList, long userId, long orgId,string finishGoodProductionBatch)
        {

            bool IsSuccess = false;
            MRawMaterialIssueStockDetails rawMaterialIssueStockDetail = new MRawMaterialIssueStockDetails();


            if (rawMaterialIssueStockDetailsDTOList.Count() > 0)
            {
                //List<RawMaterialIssueStockDetails> rawMaterialIssueStockDetailsList = new List<RawMaterialIssueStockDetails>();
                foreach (var row in rawMaterialIssueStockDetailsDTOList)
                {
                    //var ff = row.RawMaterialIssueStockId;

                    //long Rmissuestockid = 0;
                   var  Rmissuestockid = row.RawMaterialIssueStockId -  50;

                    rawMaterialIssueStockDetail.RawMaterialId = row.RawMaterialId;
                    rawMaterialIssueStockDetail.Quantity = row.Quantity;
                    // rawMaterialIssueStockDetail.EntryUserId = row.EntryUserId;
                    //rawMaterialIssueStockDetail.OrganizationId = orgId;
                    rawMaterialIssueStockDetail.UnitID = row.UnitID;

                    rawMaterialIssueStockDetail.EntryDate = DateTime.Now;
                   // rawMaterialIssueStockDetail.RawMaterialIssueStockId = row.RawMaterialIssueStockId;
                    rawMaterialIssueStockDetail.FinishGoodProductionBatch= finishGoodProductionBatch;
                    //rawMaterialIssueStockDetail.IssueStatus = "StockOut";
                    rawMaterialIssueStockDetail.IssueStatus = "Pending";
                    rawMaterialIssueStockDetail.RawMaterialIssueStockId = Rmissuestockid;
           
                    //rawMaterialIssueStockDetailsList.Add(rawMaterialIssueStockDetail);
                    _mRawMaterialIssueStockDetailsRepository.Insert(rawMaterialIssueStockDetail);
                    IsSuccess = _mRawMaterialIssueStockDetailsRepository.Save();
                }

            }



            return IsSuccess;
        }

        public MRawMaterialIssueStockDetails GetRawMaterialIssueStockByMeterialId(long rawMaterialId, long orgId)
        {
            return _mRawMaterialIssueStockDetailsRepository.GetOneByOrg(i => i.RawMaterialId == rawMaterialId);
        }

        public IEnumerable<MRawMaterialIssueStockDetails> RawMaterialStockIssueInfobyRawMaterialidPending(long rawMaterialId, long orgId)
        {
            return _mRawMaterialIssueStockDetailsRepository.GetAll(i => i.RawMaterialId == rawMaterialId && i.IssueStatus == "Pending");
        }

    }
}
