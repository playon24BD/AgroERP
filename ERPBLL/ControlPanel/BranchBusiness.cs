using ERPBLL.ControlPanel.Interface;
using ERPBO.ControlPanel.DomainModels;
using ERPBO.ControlPanel.DTOModels;
using ERPDAL.ControlPanelDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.ControlPanel
{
    public class BranchBusiness : IBranchBusiness
    {
        private readonly IControlPanelUnitOfWork _controlPanelUnitOfWork; // database
        private readonly BranchRepository branchRepository; // repo
        public BranchBusiness(IControlPanelUnitOfWork controlPanelUnitOfWork)
        {
            this._controlPanelUnitOfWork = controlPanelUnitOfWork;
            branchRepository = new BranchRepository(this._controlPanelUnitOfWork);
        }

        public IEnumerable<Branch> GetAllBranches()
        {
            return branchRepository.GetAll().ToList();
        }

        public IEnumerable<Branch> GetBranchByOrgId(long orgId)
        {
            return branchRepository.GetAll(br => br.OrganizationId == orgId).ToList();
        }

        public Branch GetBranchOneByOrgId(long id, long orgId)
        {
            return branchRepository.GetOneByOrg(br => br.BranchId == id && br.OrganizationId == orgId);
        }

        public bool IsDuplicateBrachName(string branchName, long id, long orgId)
        {
            return branchRepository.GetOneByOrg(br => br.BranchName == branchName && br.BranchId != id && br.OrganizationId == orgId ) != null ? true : false;
        }

        public bool SaveBranch(BranchDTO branchDTO, long userId, long orgId)
        {
            
            try
            {
                if (branchDTO.BranchId == 0)
                {
                    Branch branch = new Branch();
                    branch.BranchName = branchDTO.BranchName;
                    branch.ShortName = branchDTO.ShortName;
                    branch.MobileNo = branchDTO.MobileNo;
                    branch.Address = branchDTO.Address;
                    branch.Email = branchDTO.Email;
                    branch.PhoneNo = branchDTO.PhoneNo;
                    branch.Fax = branchDTO.Fax;
                    branch.IsActive = branchDTO.IsActive;
                    branch.EntryDate = DateTime.Now;
                    branch.EUserId = userId;
                    branch.OrganizationId = branchDTO.OrgId;
                    branch.Remarks = branchDTO.Remarks;
                    branchRepository.Insert(branch);

                }
                else
                {
                    var branchInDb = GetBranchOneByOrgId(branchDTO.BranchId, branchDTO.OrgId);
                    branchInDb.BranchName = branchDTO.BranchName;
                    branchInDb.ShortName = branchDTO.ShortName;
                    branchInDb.MobileNo = branchDTO.MobileNo;
                    branchInDb.Address = branchDTO.Address;
                    branchInDb.Email = branchDTO.Email;
                    branchInDb.PhoneNo = branchDTO.PhoneNo;
                    branchInDb.Fax = branchDTO.Fax;
                    branchInDb.IsActive = branchDTO.IsActive;
                    branchInDb.UpdateDate = DateTime.Now;
                    branchInDb.UpUserId = userId;
                    branchInDb.Remarks = branchDTO.Remarks;
                    branchRepository.Update(branchInDb);
                }
                return branchRepository.Save();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
