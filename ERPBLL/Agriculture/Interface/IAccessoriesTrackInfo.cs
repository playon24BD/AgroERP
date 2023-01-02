using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IAccessoriesTrackInfo
    {

        IEnumerable<AccessoriesTrackInfoDTO> GetAccessoriesPurchaseStock();
    }
}
