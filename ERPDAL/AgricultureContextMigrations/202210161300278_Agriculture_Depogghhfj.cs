namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_Depogghhfj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblRawMaterialInfo", "Unit", c => c.String());
            DropTable("dbo.tblProductSalesDetails");
            DropTable("dbo.tblProductSalesInfo");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tblProductSalesInfo",
                c => new
                    {
                        ProductSalesInfoId = c.Long(nullable: false, identity: true),
                        InvoiceNo = c.String(),
                        InvoiceDate = c.DateTime(),
                        ChallanNo = c.String(),
                        Depot = c.String(),
                        ChallanDate = c.DateTime(),
                        UserAssignId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        ZoneId = c.Long(nullable: false),
                        DivisionId = c.Long(nullable: false),
                        RegionId = c.Long(nullable: false),
                        AreaId = c.Long(nullable: false),
                        TerritoryId = c.Long(nullable: false),
                        StockiestId = c.Long(nullable: false),
                        VehicleType = c.String(),
                        VehicleNumber = c.String(),
                        DriverName = c.String(),
                        DeliveryPlace = c.String(),
                        Do_ADO_DA = c.String(),
                        DoADO_Name = c.String(),
                        PaymentMode = c.String(),
                        TotalAmount = c.Double(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.ProductSalesInfoId);
            
            CreateTable(
                "dbo.tblProductSalesDetails",
                c => new
                    {
                        ProductSalesDetailsId = c.Long(nullable: false, identity: true),
                        ProductSalesInfoId = c.Long(nullable: false),
                        FinishGoodProductInfoId = c.String(),
                        Quanity = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        MeasurementId = c.Long(nullable: false),
                        Discount = c.Double(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.ProductSalesDetailsId);
            
            DropColumn("dbo.tblRawMaterialInfo", "Unit");
        }
    }
}
