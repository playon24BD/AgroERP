namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PAYMENTeDIT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblPaymentMoneyRecipt",
                c => new
                    {
                        PaymentMoneyReciptId = c.Long(nullable: false, identity: true),
                        MoneyReciptNo = c.String(),
                        StockiestId = c.Long(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        BankName = c.String(),
                        BranchName = c.String(),
                        CustomerName = c.String(),
                        PaymentMode = c.String(),
                        AccounrNumber = c.String(),
                    })
                .PrimaryKey(t => t.PaymentMoneyReciptId);
            
            AddColumn("dbo.tblProductSalesPaymentHistory", "PaymentMoneyReciptId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblProductSalesPaymentHistory", "PaymentMoneyReciptId");
            DropTable("dbo.tblPaymentMoneyRecipt");
        }
    }
}
