using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IRawMaterialRequisitionDetailsBusiness
    {
        IEnumerable<RawMaterialRequisitionDetails> GetRawMaterialRequisitionDetails(long orgId);
        RawMaterialRequisitionDetails GetRawMaterialRequisitionDetailsbyId(long DetailsId, long orgId);
        bool SaveRawMaterialRequisitionDetails(List<RawMaterialRequisitionDetailsDTO> rawMaterialRequisitionDetailsDTO ,long userId, long orgId);
    }
}
