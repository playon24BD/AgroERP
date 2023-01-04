using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IMeasuremenBusiness
    {
        IEnumerable<MeasurementSetupDTO> GetMeasurementSetups(long orgId);
        IEnumerable<MeasurementSetup> GetAllMeasurement(int MasterCarton,int InnerBox, double PackSize, long UnitId);
        IEnumerable<MeasurementSetupDTO> GetMeasurementListSearch(long? unitId, string status,string measurementName,long orgId);
        MeasurementSetup GetMeasurementById(long measureMentId, long orgId);
        //bool SaveMeasureMent(List<MeasurementSetupDTO> measurementDTO,  long orgId);
        bool SaveMeasureMent(List<MeasurementSetupDTO> measurementDTO, long userId, long orgId);
        bool UpdateMeasureMent( MeasurementSetupDTO measurementSetup,long userId, long orgId);


    }
}
