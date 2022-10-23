using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DomainModels;
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
    }
}
