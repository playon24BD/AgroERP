using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IRawMaterialSupplier
    {
        IEnumerable<RawMaterialSupplier> GetAllRawMaterialSupplierInfo(long OrgId);
        bool SaveRawMaterialSupplierName(RawMaterialSupplierDTO rawMaterialSupplier, long userId, long orgId);
    }
}
