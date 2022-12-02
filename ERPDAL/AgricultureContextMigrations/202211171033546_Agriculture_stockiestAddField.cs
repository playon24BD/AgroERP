namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_stockiestAddField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblPRawMaterialStockInfo", "ProductCode", c => c.String());
            AddColumn("dbo.tblStockiestInfo", "StockiestCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblStockiestInfo", "StockiestCode");
            DropColumn("dbo.tblPRawMaterialStockInfo", "ProductCode");
        }
    }
}
