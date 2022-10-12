namespace ERPDAL.ControlPanelContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ControlPanel_AllEntities_16May2020 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblApplicationUsers",
                c => new
                    {
                        UserId = c.Long(nullable: false, identity: true),
                        FullName = c.String(maxLength: 150),
                        UserName = c.String(maxLength: 50),
                        Password = c.String(),
                        EmployeeId = c.String(),
                        Email = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsRoleActive = c.Boolean(nullable: false),
                        RoleId = c.Long(nullable: false),
                        EUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        UpUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        BranchId = c.Long(nullable: false),
                        MobileNo = c.String(),
                        Address = c.String(),
                        Desigation = c.String(),
                        ConfirmPassword = c.String(),
                        OrganizationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.tblBranch", t => t.BranchId, cascadeDelete: true)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.tblBranch",
                c => new
                    {
                        BranchId = c.Long(nullable: false, identity: true),
                        BranchName = c.String(),
                        ShortName = c.String(),
                        MobileNo = c.String(),
                        Email = c.String(),
                        PhoneNo = c.String(),
                        Fax = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        EUserId = c.Long(),
                        Remarks = c.String(maxLength: 150),
                        EntryDate = c.DateTime(),
                        UpUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        OrganizationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.BranchId)
                .ForeignKey("dbo.tblOrganizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.tblOrganizations",
                c => new
                    {
                        OrganizationId = c.Long(nullable: false, identity: true),
                        OrganizationName = c.String(maxLength: 150),
                        ShortName = c.String(maxLength: 50),
                        Address = c.String(maxLength: 150),
                        Email = c.String(maxLength: 150),
                        PhoneNumber = c.String(maxLength: 50),
                        MobileNumber = c.String(maxLength: 50),
                        Fax = c.String(maxLength: 50),
                        Website = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        ContractDate = c.DateTime(),
                        OrgLogoPath = c.String(maxLength: 250),
                        ReportLogoPath = c.String(maxLength: 250),
                        EUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        UpUserId = c.Long(),
                        AppType = c.String(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.OrganizationId);
            
            CreateTable(
                "dbo.tblMainMenus",
                c => new
                    {
                        MMId = c.Long(nullable: false, identity: true),
                        MenuName = c.String(),
                        EUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        UpUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        MId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.MMId)
                .ForeignKey("dbo.tblModules", t => t.MId, cascadeDelete: true)
                .Index(t => t.MId);
            
            CreateTable(
                "dbo.tblModules",
                c => new
                    {
                        MId = c.Long(nullable: false, identity: true),
                        ModuleName = c.String(),
                        IconName = c.String(),
                        IconColor = c.String(),
                        EUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        UpUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.MId);
            
            CreateTable(
                "dbo.tblSubMenus",
                c => new
                    {
                        SubMenuId = c.Long(nullable: false, identity: true),
                        SubMenuName = c.String(maxLength: 100),
                        ControllerName = c.String(maxLength: 150),
                        ActionName = c.String(maxLength: 150),
                        IconClass = c.String(maxLength: 100),
                        IsViewable = c.Boolean(nullable: false),
                        IsActAsParent = c.Boolean(nullable: false),
                        ParentSubMenuId = c.Long(),
                        EUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        UpUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        MMId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.SubMenuId)
                .ForeignKey("dbo.tblMainMenus", t => t.MMId, cascadeDelete: true)
                .Index(t => t.MMId);
            
            CreateTable(
                "dbo.tblOrganizationAuthorization",
                c => new
                    {
                        OAuthId = c.Long(nullable: false, identity: true),
                        OrganizationId = c.Long(nullable: false),
                        MainmenuId = c.Long(nullable: false),
                        ModuleId = c.Long(nullable: false),
                        EUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        UpUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.OAuthId);
            
            CreateTable(
                "dbo.tblRoleAuthorization",
                c => new
                    {
                        TaskId = c.Long(nullable: false, identity: true),
                        RoleId = c.Long(nullable: false),
                        ModuleId = c.Long(nullable: false),
                        MainmenuId = c.Long(nullable: false),
                        SubmenuId = c.Long(nullable: false),
                        ParentSubmenuId = c.Long(nullable: false),
                        Add = c.Boolean(nullable: false),
                        Edit = c.Boolean(nullable: false),
                        Detail = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        Approval = c.Boolean(nullable: false),
                        Report = c.Boolean(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        EUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        UpUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.TaskId);
            
            CreateTable(
                "dbo.tblRoles",
                c => new
                    {
                        RoleId = c.Long(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 100),
                        OrganizationId = c.Long(nullable: false),
                        EUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        UpUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.tblUserAuthorization",
                c => new
                    {
                        TaskId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        RoleId = c.Long(),
                        ModuleId = c.Long(nullable: false),
                        MainmenuId = c.Long(nullable: false),
                        SubmenuId = c.Long(nullable: false),
                        ParentSubmenuId = c.Long(nullable: false),
                        Add = c.Boolean(nullable: false),
                        Edit = c.Boolean(nullable: false),
                        Detail = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        Approval = c.Boolean(nullable: false),
                        Report = c.Boolean(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        EUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        UpUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.TaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblSubMenus", "MMId", "dbo.tblMainMenus");
            DropForeignKey("dbo.tblMainMenus", "MId", "dbo.tblModules");
            DropForeignKey("dbo.tblApplicationUsers", "BranchId", "dbo.tblBranch");
            DropForeignKey("dbo.tblBranch", "OrganizationId", "dbo.tblOrganizations");
            DropIndex("dbo.tblSubMenus", new[] { "MMId" });
            DropIndex("dbo.tblMainMenus", new[] { "MId" });
            DropIndex("dbo.tblBranch", new[] { "OrganizationId" });
            DropIndex("dbo.tblApplicationUsers", new[] { "BranchId" });
            DropTable("dbo.tblUserAuthorization");
            DropTable("dbo.tblRoles");
            DropTable("dbo.tblRoleAuthorization");
            DropTable("dbo.tblOrganizationAuthorization");
            DropTable("dbo.tblSubMenus");
            DropTable("dbo.tblModules");
            DropTable("dbo.tblMainMenus");
            DropTable("dbo.tblOrganizations");
            DropTable("dbo.tblBranch");
            DropTable("dbo.tblApplicationUsers");
        }
    }
}
