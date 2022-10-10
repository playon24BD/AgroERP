namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class regionadd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblRegionInfos",
                c => new
                    {
                        RegionId = c.Long(nullable: false, identity: true),
                        RegionName = c.String(),
                        Remarks = c.String(),
                        MobileNumber = c.String(),
                        RegionAsignName = c.String(),
                        Status = c.String(),
                        DivisionId = c.Long(nullable: false),
                        ZoneId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.RegionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblRegionInfos");
        }
    }
}
