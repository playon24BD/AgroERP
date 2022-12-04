using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IAgroUnitInfo
    {
        AgroUnitInfo GetAgroInfoById(long unitId, long orgId);

        IEnumerable<AgroUnitInfoDTO> GetAgroUnitInfos(long? unitId, long orgId);

        IEnumerable<AgroUnitInfo> GetAllAgroUnitInfo(long OrgId);
        bool SaveAgroUnitList(AgroUnitInfoDTO infoDTO, long userId, long orgId);
        AgroUnitInfo GetUnitId(string ProductUnit);
        AgroUnitInfo UnitIdwiseUnitName(long UnitIds);
        AgroUnitInfo UnitIdwiseUnitNameList(double UnitList);
    }
}
