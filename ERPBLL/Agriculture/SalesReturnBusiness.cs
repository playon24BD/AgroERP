using ERPBLL.Agriculture.Interface;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class SalesReturnBusiness : ISalesReturn
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly SalesReturnRepository _salesReturnRepository;


        public SalesReturnBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._salesReturnRepository = new SalesReturnRepository(this._agricultureUnitOfWork);
           
        }



    }
}
