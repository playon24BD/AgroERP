namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_RegionUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblRegionUser",
                c => new
                    {
                        RegionUserId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        RegionId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        Status = c.String(),
                        Flag = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.RegionUserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblRegionUser");
        }
    }
}
