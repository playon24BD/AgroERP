namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_DistributionUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblDistributionUser",
                c => new
                    {
                        DistributionUserId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ZoneId = c.Long(),
                        DivisionId = c.Long(),
                        RegionId = c.Long(),
                        AreaId = c.Long(),
                        TerritoryId = c.Long(),
                        StockiestId = c.Long(),
                        DistributionType = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        Status = c.String(),
                        Flag = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.DistributionUserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblDistributionUser");
        }
    }
}
