namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblPaymentMoneyRecipt", "Status", c => c.String());
            AddColumn("dbo.tblProductSalesPaymentHistory", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblProductSalesPaymentHistory", "Status");
            DropColumn("dbo.tblPaymentMoneyRecipt", "Status");
        }
    }
}
