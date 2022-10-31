namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesReturn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblSalesReturn",
                c => new
                    {
                        SalesReturnId = c.Long(nullable: false, identity: true),
                        ReturnCode = c.String(),
                        InvoiceNo = c.String(),
                        InvoiceDate = c.DateTime(nullable: false),
                        ReturnQuanity = c.Double(nullable: false),
                        ReturnPerUnitPrice = c.Double(nullable: false),
                        ReturnTotalPrice = c.Double(nullable: false),
                        Status = c.String(),
                        FinishGoodProductInfoId = c.Long(nullable: false),
                        MeasurementId = c.Long(nullable: false),
                        ProductSalesInfoId = c.Long(nullable: false),
                        ReturnDate = c.DateTime(),
                        AdjustmentDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.SalesReturnId);
            
            AddColumn("dbo.tblProductSalesPaymentHistory", "PaymentMode", c => c.String());
            AddColumn("dbo.tblProductSalesPaymentHistory", "AccounrNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblProductSalesPaymentHistory", "AccounrNumber");
            DropColumn("dbo.tblProductSalesPaymentHistory", "PaymentMode");
            DropTable("dbo.tblSalesReturn");
        }
    }
}
