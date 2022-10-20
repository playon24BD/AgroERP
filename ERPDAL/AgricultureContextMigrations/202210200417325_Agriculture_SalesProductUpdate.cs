namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_SalesProductUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblProductSalesDetails", "MeasurementSize", c => c.String());
            AddColumn("dbo.tblProductSalesDetails", "DiscountTk", c => c.Double(nullable: false));
            CreateIndex("dbo.tblProductSalesDetails", "ProductSalesInfoId");
            AddForeignKey("dbo.tblProductSalesDetails", "ProductSalesInfoId", "dbo.tblProductSalesInfo", "ProductSalesInfoId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblProductSalesDetails", "ProductSalesInfoId", "dbo.tblProductSalesInfo");
            DropIndex("dbo.tblProductSalesDetails", new[] { "ProductSalesInfoId" });
            DropColumn("dbo.tblProductSalesDetails", "DiscountTk");
            DropColumn("dbo.tblProductSalesDetails", "MeasurementSize");
        }
    }
}
