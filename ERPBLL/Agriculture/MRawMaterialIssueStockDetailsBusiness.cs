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

        public IEnumerable<MRawMaterialIssueStockDetails> GetRawMatwrialissueDetailsByInfoId(long infoId,string Status)
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
