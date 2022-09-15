using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IBankSetup
    {
        IEnumerable<BankSetup> GetAllBankSetup(long OrgId);
        BankSetup GetBankNameById(long bankId, long orgId);
        bool SaveBankInfo(BankSetupDTO infoDTO, long userId, long orgId);
    }
}
