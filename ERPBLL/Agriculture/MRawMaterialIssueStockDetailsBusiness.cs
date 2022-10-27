﻿using ERPBLL.Agriculture.Interface;
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

<<<<<<< Updated upstream
        public IEnumerable<MRawMaterialIssueStockDetails> GetRawMatwrialissueDetailsByInfoId(long infoId,string Status)
=======
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
StockIN=(SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where t.IssueStatus ='StockIn' and t.RawMaterialId=RM.RawMaterialId),
StockOut=(SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where  t.IssueStatus ='StockOut'and t.RawMaterialId=RM.RawMaterialId),
CurrentStock=(SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where t.IssueStatus ='StockIn'and t.RawMaterialId=RM.RawMaterialId)-(SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
where  t.IssueStatus ='StockOut'and t.RawMaterialId=RM.RawMaterialId)

FROM  
tblMRawMaterialIssueStockDetails t 
INNER JOIN tblRawMaterialInfo RM on t.RawMaterialId=RM.RawMaterialId
inner join tblAgroUnitInfo un on RM.UnitId = un.UnitId
      where 1=1  {0}",
            Utility.ParamChecker(param));
            return query;
        }

        public IEnumerable<MRawMaterialIssueStockDetails> GetRawMatwrialissueDetailsByInfoId(long infoId)
>>>>>>> Stashed changes
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

        public bool SaveRawMaterialIssueDetails(List<MRawMaterialIssueStockDetailsDTO> rawMaterialIssueStockDetailsDTOList, long userId, long orgId)
        {

            bool IsSuccess = false;
            MRawMaterialIssueStockDetails rawMaterialIssueStockDetail = new MRawMaterialIssueStockDetails();


            if (rawMaterialIssueStockDetailsDTOList.Count() > 0)
            {
                //List<RawMaterialIssueStockDetails> rawMaterialIssueStockDetailsList = new List<RawMaterialIssueStockDetails>();
                foreach (var row in rawMaterialIssueStockDetailsDTOList)
                {
                    rawMaterialIssueStockDetail.RawMaterialId = row.RawMaterialId;
                    rawMaterialIssueStockDetail.Quantity = row.Quantity;
                   // rawMaterialIssueStockDetail.EntryUserId = row.EntryUserId;
                    //rawMaterialIssueStockDetail.OrganizationId = orgId;
                    rawMaterialIssueStockDetail.UnitID = row.UnitID;

                    rawMaterialIssueStockDetail.EntryDate = DateTime.Now;
                    rawMaterialIssueStockDetail.RawMaterialIssueStockId = row.RawMaterialIssueStockId;
                    rawMaterialIssueStockDetail.IssueStatus = "StockOut";
                    //rawMaterialIssueStockDetailsList.Add(rawMaterialIssueStockDetail);
                    _mRawMaterialIssueStockDetailsRepository.Insert(rawMaterialIssueStockDetail);
                    IsSuccess = _mRawMaterialIssueStockDetailsRepository.Save();
                }

            }



            return IsSuccess;
        }

        public MRawMaterialIssueStockDetails GetRawMaterialIssueStockByMeterialId(long rawMaterialId, long orgId)
        {
            return _mRawMaterialIssueStockDetailsRepository.GetOneByOrg(i => i.RawMaterialId == rawMaterialId );
        }
    }
}
