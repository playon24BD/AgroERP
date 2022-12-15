namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockiestWiseTargetAddField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblStockiestWiseYearlyTarget", "TerritoryId", c => c.Long(nullable: false));
            AddColumn("dbo.tblStockiestWiseYearlyTarget", "Day", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblStockiestWiseYearlyTarget", "Day");
            DropColumn("dbo.tblStockiestWiseYearlyTarget", "TerritoryId");
        }
    }
}
