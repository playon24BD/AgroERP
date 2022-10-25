namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesPaymentREgistar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblProductSalesPaymentHistory",
                c => new
                    {
                        PaymentRegisterID = c.Long(nullable: false, identity: true),
                        PaymentAmount = c.Double(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        ProductSalesInfoId = c.Long(nullable: false),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.PaymentRegisterID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblProductSalesPaymentHistory");
        }
    }
}
