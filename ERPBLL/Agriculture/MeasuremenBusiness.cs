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
    public class MeasuremenBusiness: IMeasuremenBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly MeasurmentRepository _measurmentRepository;


        public MeasuremenBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._measurmentRepository = new MeasurmentRepository(this._agricultureUnitOfWork);
        }

        public MeasurementSetup GetMeasurementById(long measureMentId, long orgId)
        {
            return _measurmentRepository.GetOneByOrg(a=>a.MeasurementId==measureMentId && a.OrganizationId==orgId);
        }

        public IEnumerable<MeasurementSetup> GetMeasurementSetups(long orgId)
        {
            return _measurmentRepository.GetAll();
        }

        public bool SaveMeasureMent(long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
