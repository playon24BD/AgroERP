namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_StockiestInfo : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblStockiestInfo");
        }
    }
}
