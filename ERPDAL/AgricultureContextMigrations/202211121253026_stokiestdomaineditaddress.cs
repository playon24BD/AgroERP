namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stokiestdomaineditaddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblStockiestInfo", "StockiestAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblStockiestInfo", "StockiestAddress");
        }
    }
}
