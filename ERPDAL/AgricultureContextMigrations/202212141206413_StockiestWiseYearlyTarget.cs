namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockiestWiseYearlyTarget : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblStockiestWiseYearlyTarget",
                c => new
                    {
                        StockiestWiseYearlyTargetId = c.Long(nullable: false, identity: true),
                        StockiestId = c.Long(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        TargetQty = c.Double(nullable: false),
                        Flag = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.StockiestWiseYearlyTargetId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblStockiestWiseYearlyTarget");
        }
    }
}
