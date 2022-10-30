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
        IEnumerable<RawMaterialRequisitionDetails> GetRawMaterialRequisitionDetailsbyInfo(long InfoId,long orgId);
        RawMaterialRequisitionDetails GetRawMaterialRequisitionDetailsbyId(long DetailsId, long orgId);
        bool SaveRawMaterialRequisitionDetails(List<RawMaterialRequisitionDetailsDTO> rawMaterialRequisitionDetailsDTO ,long infoId,long userId, long orgId);
    }
}
