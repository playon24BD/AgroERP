namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moneyreciptdomainaddfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblPaymentMoneyRecipt", "CommisionPercent", c => c.Double(nullable: false));
            AddColumn("dbo.tblPaymentMoneyRecipt", "flag", c => c.String());
            AddColumn("dbo.tblProductSalesInfo", "CommisionPercent", c => c.Double(nullable: false));
            AddColumn("dbo.tblProductSalesInfo", "CommisionAmount", c => c.Double(nullable: false));
            AddColumn("dbo.tblProductSalesInfo", "flag", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblProductSalesInfo", "flag");
            DropColumn("dbo.tblProductSalesInfo", "CommisionAmount");
            DropColumn("dbo.tblProductSalesInfo", "CommisionPercent");
            DropColumn("dbo.tblPaymentMoneyRecipt", "flag");
            DropColumn("dbo.tblPaymentMoneyRecipt", "CommisionPercent");
        }
    }
}
