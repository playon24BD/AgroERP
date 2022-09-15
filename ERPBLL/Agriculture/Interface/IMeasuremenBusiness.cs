using ERPBO.Agriculture.DomainModels;
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
        bool SaveMeasureMent(long orgId);


    }
}
