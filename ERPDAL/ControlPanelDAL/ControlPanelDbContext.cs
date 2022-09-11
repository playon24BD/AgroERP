using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPBO.ControlPanel.DomainModels;

namespace ERPDAL.ControlPanelDAL
{
    public class ControlPanelDbContext : DbContext
    {
        public ControlPanelDbContext():base("ControlPanel")
        {

        }
        public DbSet<Organization> tblOrganizations { get; set; }
        public DbSet<Branch> tblBranch { get; set; }
        public DbSet<AppUser> tblApplicationUsers { get; set; }
        public DbSet<Role> tblRoles { get; set; }
        public DbSet<Module> tblModules { get; set; }
        public DbSet<MainMenu> tblMainMenus { get; set; }
        public DbSet<SubMenu> tblSubMenus { get; set; }
        public DbSet<OrganizationAuthorization> tblOrganizationAuthorization { get; set; }
        public DbSet<UserAuthorization> tblUserAuthorization { get; set; }
        public DbSet<RoleAuthorization> tblRoleAuthorization { get; set; }
    }
}
