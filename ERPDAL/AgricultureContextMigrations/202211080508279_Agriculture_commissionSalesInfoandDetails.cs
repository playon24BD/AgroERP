namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_commissionSalesInfoandDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCommisionOnProductSalesDetails",
                c => new
                    {
                        CommissionOnProductSalesDetailsId = c.Long(nullable: false, identity: true),
                        CommissionOnProductOnSalesId = c.Long(nullable: false),
                        ProductSalesInfoId = c.Long(nullable: false),
                        CommissionOnProductId = c.Long(nullable: false),
                        FinishGoodProductId = c.Long(nullable: false),
                        InvoiceNo = c.String(),
                        PaymentMode = c.String(),
                        Credit = c.Double(nullable: false),
                        Cash = c.Double(nullable: false),
                        TotalCommission = c.Double(nullable: false),
                        Remarks = c.String(),
                        Status = c.String(),
                        Flag = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.CommissionOnProductSalesDetailsId);
            
            CreateTable(
                "dbo.tblCommissionOnProductSales",
                c => new
                    {
                        CommissionOnProductOnSalesId = c.Long(nullable: false, identity: true),
                        ProductSalesInfoId = c.Long(nullable: false),
                        FinishGoodProductId = c.Long(nullable: false),
                        InvoiceNo = c.String(),
                        PaymentMode = c.String(),
                        Remarks = c.String(),
                        Status = c.String(),
                        Flag = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.CommissionOnProductOnSalesId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblCommissionOnProductSales");
            DropTable("dbo.tblCommisionOnProductSalesDetails");
        }
    }
}
