 using ERPBLL.Agriculture.Interface;
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


        //contractor
        public MRawMaterialIssueStockInfoBusisness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._mRawMaterialIssueStockInfoRepository = new MRawMaterialIssueStockInfoRepository(this._agricultureUnitOfWork);
        }



    }
}
