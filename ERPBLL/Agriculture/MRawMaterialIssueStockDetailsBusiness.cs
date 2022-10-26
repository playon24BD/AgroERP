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

        public IEnumerable<MRawMaterialIssueStockDetails> GetRawMatwrialissueDetailsByInfoId(long infoId)
        {
            return _mRawMaterialIssueStockDetailsRepository.GetAll(i => i.RawMaterialIssueStockId == infoId).ToList();
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


    }
}
