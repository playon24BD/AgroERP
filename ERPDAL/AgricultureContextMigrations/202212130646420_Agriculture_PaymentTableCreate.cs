namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_PaymentTableCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblProductSalesPaymentHistory", "CommisionPercent", c => c.Double(nullable: false));
            AddColumn("dbo.tblProductSalesPaymentHistory", "CommisionAmount", c => c.Double(nullable: false));
            DropColumn("dbo.tblPaymentMoneyRecipt", "CommisionPercent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblPaymentMoneyRecipt", "CommisionPercent", c => c.Double(nullable: false));
            DropColumn("dbo.tblProductSalesPaymentHistory", "CommisionAmount");
            DropColumn("dbo.tblProductSalesPaymentHistory", "CommisionPercent");
        }
    }
}
