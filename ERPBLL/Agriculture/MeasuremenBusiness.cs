using ERPBLL.Agriculture.Interface;
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
    public class MeasuremenBusiness : IMeasuremenBusiness
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
            return _measurmentRepository.GetOneByOrg(a => a.MeasurementId == measureMentId && a.OrganizationId == orgId);
        }

        public IEnumerable<MeasurementSetup> GetMeasurementSetups(long orgId)
        {
            return _measurmentRepository.GetAll();
        }

        //public bool SaveMeasureMent(List<MeasurementSetupDTO> measurementDTO, long orgId)
        //{
        //    bool IsSuccess = false;

        //    List<MeasurementSetup> measurements = new List<MeasurementSetup>();
        //    if (measurementDTO.Count()> 0)
        //    {
        //        foreach(var item in measurementDTO)
        //        {
        //            MeasurementSetup measurement = new MeasurementSetup
        //            {
        //                MeasurementName = item.MeasurementName,
        //                OrganizationId = orgId,
        //                MasterCarton = item.MasterCarton,
        //                InnerBox = item.InnerBox,
        //                PackSize = item.PackSize,
        //                Unit = item.Unit,
        //                Status="Active",
                        
                        

        //            };
        //            measurements.Add(measurement);



        //        }
        //        _measurmentRepository.InsertAll(measurements);
        //    }

        //    IsSuccess = _measurmentRepository.Save();

        //    return IsSuccess;

        //}

        public bool SaveMeasureMent(List<MeasurementSetupDTO> measurementDTO, long userId, long orgId)
        {
            bool IsSuccess = false;

            List<MeasurementSetup> measurements = new List<MeasurementSetup>();
            if (measurementDTO.Count() > 0)
            {
                foreach (var item in measurementDTO)
                {
                    MeasurementSetup measurement = new MeasurementSetup
                    {
                        MeasurementName = item.MeasurementName,
                        OrganizationId = orgId,
                        MasterCarton = item.MasterCarton,
                        InnerBox = item.InnerBox,
                        PackSize = item.PackSize,
                        Unit = item.Unit,
                        Status = "Active",
                        EntryUserId = userId,
                        EntryDate= DateTime.Now
                        

                    };
                    measurements.Add(measurement);



                }
                _measurmentRepository.InsertAll(measurements);
            }

            IsSuccess = _measurmentRepository.Save();

            return IsSuccess;
        }

        public bool UpdateMeasureMent(MeasurementSetupDTO measurementSetup,long userId, long orgId)
        {
            bool IsSuccess = false;
            MeasurementSetup measurement = new MeasurementSetup();
            if (measurementSetup.MeasurementId>0)
            {

                measurement = GetMeasurementById(measurementSetup.MeasurementId, orgId);
                measurement.MeasurementName = measurementSetup.MeasurementName;
                measurement.MasterCarton = measurementSetup.MasterCarton;
                measurement.InnerBox = measurementSetup.InnerBox;
                measurement.PackSize = measurementSetup.PackSize;
                measurement.Unit = measurementSetup.Unit;
                measurement.Status = measurementSetup.Status;
                measurement.UpdateDate = DateTime.Now;
                measurement.UpdateUserId = userId;

            }
            _measurmentRepository.Update(measurement);
            IsSuccess = _measurmentRepository.Save();


            return IsSuccess;
        }
    }
}
