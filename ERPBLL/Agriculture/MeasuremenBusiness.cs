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

        public IEnumerable<MeasurementSetupDTO> GetMeasurementSetups(long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<MeasurementSetupDTO>(QueryForAgroMasurment(orgId)).ToList();
            //return _measurmentRepository.GetAll(a => a.OrganizationId == orgId);
            //return this._agricultureUnitOfWork.Db.Database.SqlQuery<MeasurementSetupDTO>(QueryForCheckUnit(orgId)).ToList();
        }

        private string QueryForAgroMasurment(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;
           // param += string.Format(@" and OrganizationId={0}", orgId);


            query = string.Format(@"SELECT * FROM PackageDetails	
Where 1=1 ", Utility.ParamChecker(param));

            return query;
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
            double UnitKG=0;
            double MasterCarton;
            double InnerBox ;
            double PackSize;

            List<MeasurementSetup> measurements = new List<MeasurementSetup>();
            if (measurementDTO.Count() > 0)
            {
                foreach (var item in measurementDTO)
                {
                    if (item.MasterCarton > 0)
                    {
                        MasterCarton = item.MasterCarton;
                    }
                    else
                    {
                        MasterCarton = 1;
                    }
                    if (item.InnerBox > 0)
                    {
                        InnerBox = item.InnerBox;
                    }
                    else
                    {
                        InnerBox = 1;
                    }
                    if (item.PackSize > 0)
                    {
                        PackSize = item.PackSize;
                    }
                    else
                    {
                        PackSize = 1;
                    }

                    if (item.UnitId == 1)
                    {

                        UnitKG =Convert.ToInt32( MasterCarton * InnerBox * PackSize);
                    }
                    if (item.UnitId == 2)
                    {
                        UnitKG = Convert.ToInt32(MasterCarton * InnerBox * PackSize / 1000);
                    }
                    if (item.UnitId == 3)
                    {
                        UnitKG = Convert.ToInt32(MasterCarton * InnerBox * PackSize);
                    }
                    if (item.UnitId == 5)
                    {
                        UnitKG = Convert.ToInt32(MasterCarton * InnerBox * PackSize / 1000);
                    }
                    MeasurementSetup measurement = new MeasurementSetup
                    {
                        MeasurementName = item.MeasurementName,
                        OrganizationId = orgId,
                        MasterCarton = item.MasterCarton,
                        InnerBox = item.InnerBox,
                        PackSize = item.PackSize,
                        UnitId = item.UnitId,
                        Status = "Active",
                        EntryUserId = userId,
                        EntryDate = DateTime.Now,
                        UnitKG = UnitKG


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
            double UnitKG = 0;
            double MasterCarton;
            double InnerBox;
            double PackSize;
            MeasurementSetup measurement = new MeasurementSetup();
            if (measurementSetup.MeasurementId>0)
            {
                if (measurementSetup.MasterCarton > 0)
                {
                    MasterCarton = measurementSetup.MasterCarton;
                }
                else
                {
                    MasterCarton = 1;
                }
                if (measurementSetup.InnerBox > 0)
                {
                    InnerBox = measurementSetup.InnerBox;
                }
                else
                {
                    InnerBox = 1;
                }
                if (measurementSetup.PackSize > 0)
                {
                    PackSize = measurementSetup.PackSize;
                }
                else
                {
                    PackSize = 1;
                }

                if (measurementSetup.UnitId == 1)
                {

                    UnitKG = Convert.ToInt32(MasterCarton * InnerBox * PackSize);
                }
                if (measurementSetup.UnitId == 2)
                {
                    UnitKG = Convert.ToInt32(MasterCarton * InnerBox * PackSize / 1000);
                }
                if (measurementSetup.UnitId==3)
                {
                    UnitKG = Convert.ToInt32(MasterCarton * InnerBox * PackSize);
                }
                if (measurementSetup.UnitId == 5)
                {
                    UnitKG = Convert.ToInt32(MasterCarton * InnerBox * PackSize / 1000);
                }


                measurement = GetMeasurementById(measurementSetup.MeasurementId, orgId);
                measurement.MeasurementName = measurementSetup.MeasurementName;
                measurement.MasterCarton = measurementSetup.MasterCarton;
                measurement.InnerBox = measurementSetup.InnerBox;
                measurement.PackSize = measurementSetup.PackSize;
                measurement.UnitId = measurementSetup.UnitId;
                measurement.Status = measurementSetup.Status;
                measurement.UpdateDate = DateTime.Now;
                measurement.UpdateUserId = userId;
                measurement.UnitKG = measurementSetup.UnitKG;

            }
            _measurmentRepository.Update(measurement);
            IsSuccess = _measurmentRepository.Save();


            return IsSuccess;
        }
    }
}
