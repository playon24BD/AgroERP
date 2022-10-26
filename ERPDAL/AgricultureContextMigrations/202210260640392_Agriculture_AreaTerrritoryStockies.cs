namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_AreaTerrritoryStockies : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblAreaUser",
                c => new
                    {
                        AreaUserId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        AreaId = c.Long(nullable: false),
                        DistributionType = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        Status = c.String(),
                        Flag = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AreaUserId);
            
            CreateTable(
                "dbo.tblStockiestUser",
                c => new
                    {
                        StockiestUserId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        StockiestId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        Status = c.String(),
                        Flag = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.StockiestUserId);
            
            CreateTable(
                "dbo.tblTerritoryUser",
                c => new
                    {
                        TerritoryUserId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        TerritoryId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        Status = c.String(),
                        Flag = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.TerritoryUserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblTerritoryUser");
            DropTable("dbo.tblStockiestUser");
            DropTable("dbo.tblAreaUser");
        }
    }
}
