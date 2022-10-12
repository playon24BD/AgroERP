namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_rtdgdfgdf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblAreaSetup",
                c => new
                    {
                        AreaId = c.Long(nullable: false, identity: true),
                        AreaName = c.String(),
                        AreaAsignName = c.String(),
                        MobileNumber = c.String(),
                        Status = c.String(),
                        Remarks = c.String(),
                        ZoneId = c.Long(nullable: false),
                        DivisionId = c.Long(nullable: false),
                        RegionId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.AreaId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblAreaSetup");
        }
    }
}
