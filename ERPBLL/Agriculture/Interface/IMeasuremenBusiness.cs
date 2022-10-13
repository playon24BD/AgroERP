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
        IEnumerable<MeasurementSetup> GetMeasurementSetups(long orgId);
        MeasurementSetup GetMeasurementById(long measureMentId, long orgId);
        //bool SaveMeasureMent(List<MeasurementSetupDTO> measurementDTO,  long orgId);
        bool SaveMeasureMent(List<MeasurementSetupDTO> measurementDTO, long userId, long orgId);
        bool UpdateMeasureMent( MeasurementSetupDTO measurementSetup,long userId, long orgId);


    }
}
