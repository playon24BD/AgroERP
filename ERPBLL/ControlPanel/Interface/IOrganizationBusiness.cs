using ERPBO.ControlPanel.DomainModels;
using ERPBO.ControlPanel.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.ControlPanel.Interface
{
    public interface IOrganizationBusiness
    {
        IEnumerable<Organization> GetAllOrganizations();
        Organization GetOrganizationById(long id);
        bool SaveOrganization(OrganizationDTO organization,long userId);
    }
}
