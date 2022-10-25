using ERPBO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IReturnRawMaterialBusiness
    {
        List<AgroDropdown> GetIssueRawMaterials(long orgId);
    }
}
