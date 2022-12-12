using ERPBLL.Agriculture.Interface;
using ERPBLL.ControlPanel.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class PaymentMoneyReciptBusiness : IPaymentMoneyRecipt
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly PaymentMoneyReciptRepository _paymentMoneyReciptRepository;

        public PaymentMoneyReciptBusiness(IAgricultureUnitOfWork agricultureUnitOfWork )
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;

            this._paymentMoneyReciptRepository = new PaymentMoneyReciptRepository(this._agricultureUnitOfWork);

        }

        public IEnumerable<PaymentMoneyRecipt> GetAllPaymentMoneyRecipt()
        {
            return _paymentMoneyReciptRepository.GetAll().ToList();
        }
    }
}
