namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_DepoStockiestchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblStockiestInfo", "StockiestPhoneNumber", c => c.String());
            AddColumn("dbo.tblStockiestInfo", "StockiestMail", c => c.String());
            AddColumn("dbo.tblStockiestInfo", "StockiestTradeLicense", c => c.String());
            AddColumn("dbo.tblStockiestInfo", "StockiestNID", c => c.String());
            AddColumn("dbo.tblStockiestInfo", "StockiestContactPerson", c => c.String());
            AddColumn("dbo.tblStockiestInfo", "StockiestContactPersonPHNumber", c => c.String());
            AddColumn("dbo.tblStockiestInfo", "AreaId", c => c.Long(nullable: false));
            AddColumn("dbo.tblStockiestInfo", "CreditLimit", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblStockiestInfo", "CreditLimit");
            DropColumn("dbo.tblStockiestInfo", "AreaId");
            DropColumn("dbo.tblStockiestInfo", "StockiestContactPersonPHNumber");
            DropColumn("dbo.tblStockiestInfo", "StockiestContactPerson");
            DropColumn("dbo.tblStockiestInfo", "StockiestNID");
            DropColumn("dbo.tblStockiestInfo", "StockiestTradeLicense");
            DropColumn("dbo.tblStockiestInfo", "StockiestMail");
            DropColumn("dbo.tblStockiestInfo", "StockiestPhoneNumber");
        }
    }
}
