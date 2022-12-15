using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IRawMaterialTrack
    {
        IEnumerable<RawMaterialTrack> GetAllRawMaterialTruck();
        IEnumerable<RawMaterialTrackDTO> GetMainStockInOutInfos(string name);

        IEnumerable<RawMaterialTrack> RawMaterialStockInbyRawMaterialid(long rawMaterialId);
        IEnumerable<RawMaterialTrack> RawMaterialStockoutbyRawMaterialid(long rawMaterialId);

        RawMaterialTrack GetRawMaterialStockByMeterialId(long rawMaterialId);
    }
}
