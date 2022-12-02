using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.ViewModels;
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
        IEnumerable<DivisionUser> GetAllDivisionsByUserIdAndDivisionId(long userId, long orgId);
        bool UpdateDivisions(List<string> divisions, long userId, long suserId, long orgId);
        List<DivisionInfoViewModel> GetAllDiv(long userId, long orgId);
    }
}
