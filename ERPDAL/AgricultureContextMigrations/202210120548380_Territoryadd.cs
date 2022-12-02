namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Territoryadd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblTerritoryInfos",
                c => new
                    {
                        TerritoryId = c.Long(nullable: false, identity: true),
                        TerritoryName = c.String(),
                        Status = c.String(),
                        AreaId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.TerritoryId);
            
            DropTable("dbo.tblStockiestInfo");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tblStockiestInfo",
                c => new
                    {
                        StockiestId = c.Long(nullable: false, identity: true),
                        StockiestName = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        TerritoryId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.StockiestId);
            
            DropTable("dbo.tblTerritoryInfos");
        }
    }
}
