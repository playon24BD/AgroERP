using ERPBLL.Agriculture.Interface;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class BankSetupBusiness:IBankSetup
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly BankSetupRepository _bankSetupRepository;

        public BankSetupBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._bankSetupRepository = new BankSetupRepository(this._agricultureUnitOfWork);
        }
    }
}
