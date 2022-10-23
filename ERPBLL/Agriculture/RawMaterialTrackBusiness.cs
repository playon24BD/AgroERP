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
    public class RawMaterialTrackBusiness : IRawMaterialTrack
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly RawMaterialTrackInfoRepository _rawMaterialTrackInfoRepository;


        //contractor
        public RawMaterialTrackBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._rawMaterialTrackInfoRepository = new RawMaterialTrackInfoRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<RawMaterialTrack> GetAllRawMaterialTruck()
        {
            return _rawMaterialTrackInfoRepository.GetAll().ToList();
        }
    }
}
