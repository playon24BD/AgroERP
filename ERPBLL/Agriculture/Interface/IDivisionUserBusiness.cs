using ERPBO.Agriculture.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
   public interface IDivisionUserBusiness
    {
        bool SaveDivisionsUser(List<string> divisions, long userId, long suserId, long orgId);
        DivisionUser GetAllDivisionsByUserIdAndDivisionId(long userId, long divisionId, long orgId);
        bool UpdateDivisions(List<long> divisions, long userId, long suserId, long orgId);
    }
}
