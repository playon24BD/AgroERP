using ERPBO.ControlPanel.DomainModels;
using ERPBO.ControlPanel.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.ControlPanel.Interface
{
   public interface IBranchBusiness
    {
        IEnumerable<Branch> GetAllBranches();
        IEnumerable<Branch> GetBranchByOrgId(long orgId);
        bool IsDuplicateBrachName(string branchName, long id, long orgId);
        bool SaveBranch(BranchDTO branchDTO, long userId, long orgId);
        Branch GetBranchOneByOrgId(long id, long orgId);
    }
}
