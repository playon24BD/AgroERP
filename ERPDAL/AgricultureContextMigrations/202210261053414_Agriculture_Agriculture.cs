namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_Agriculture : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblAgroUnitInfo",
                c => new
                    {
                        UnitId = c.Long(nullable: false, identity: true),
                        UnitName = c.String(),
                        Status = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.UnitId);
            
            CreateTable(
                "dbo.tblAreaSetup",
                c => new
                    {
                        AreaId = c.Long(nullable: false, identity: true),
                        AreaName = c.String(),
                        Status = c.String(),
                        RegionId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.AreaId);
            
            CreateTable(
                "dbo.tblAreaUser",
                c => new
                    {
                        AreaUserId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        AreaId = c.Long(nullable: false),
                        DistributionType = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        Status = c.String(),
                        Flag = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AreaUserId);
            
            CreateTable(
                "dbo.tblBankInfo",
                c => new
                    {
                        BankId = c.Long(nullable: false, identity: true),
                        BankName = c.String(),
                        MobileNumber = c.String(),
                        AccountNumber = c.String(),
                        Email = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.BankId);
            
            CreateTable(
                "dbo.tblDepotInfo",
                c => new
                    {
                        DepotId = c.Long(nullable: false, identity: true),
                        OrganizationId = c.Long(nullable: false),
                        DepotName = c.String(),
                        RoleId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.DepotId);
            
            CreateTable(
                "dbo.tblDistributionUser",
                c => new
                    {
                        DistributionUserId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ZoneId = c.Long(),
                        DivisionId = c.Long(),
                        RegionId = c.Long(),
                        AreaId = c.Long(),
                        TerritoryId = c.Long(),
                        StockiestId = c.Long(),
                        DistributionType = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        Status = c.String(),
                        Flag = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.DistributionUserId);
            
            CreateTable(
                "dbo.tblDivisionInfo",
                c => new
                    {
                        DivisionId = c.Long(nullable: false, identity: true),
                        DivisionName = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        ZoneId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.DivisionId);
            
            CreateTable(
                "dbo.DivisionUsers",
                c => new
                    {
                        DivisionUserId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        DivisionId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        Status = c.String(),
                        OrganizationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.DivisionUserId);
            
            CreateTable(
                "dbo.tblFinishGoodProductInfo",
                c => new
                    {
                        FinishGoodProductId = c.Long(nullable: false, identity: true),
                        FinishGoodProductName = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUser = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUser = c.Long(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.FinishGoodProductId);
            
            CreateTable(
                "dbo.FinishGoodProductionDetails",
                c => new
                    {
                        FinishGoodProductDetailId = c.Long(nullable: false, identity: true),
                        FinishGoodProductionBatch = c.String(),
                        ReceipeBatchCode = c.String(),
                        RawMaterialId = c.Long(nullable: false),
                        FGRRawMaterQty = c.Double(nullable: false),
                        TotalQuantity = c.Double(nullable: false),
                        RequiredQuantity = c.Double(nullable: false),
                        Status = c.String(),
                        Remarks = c.String(),
                        flag = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.FinishGoodProductDetailId);
            
            CreateTable(
                "dbo.FinishGoodProductionInfoes",
                c => new
                    {
                        FinishGoodProductInfoId = c.Long(nullable: false, identity: true),
                        FinishGoodProductionBatch = c.String(),
                        ReceipeBatchCode = c.String(),
                        FinishGoodProductId = c.Long(nullable: false),
                        Quanity = c.Double(nullable: false),
                        TargetQuantity = c.Double(nullable: false),
                        Status = c.String(),
                        Remarks = c.String(),
                        flag = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.FinishGoodProductInfoId);
            
            CreateTable(
                "dbo.tblFinishGoodRecipeDetails",
                c => new
                    {
                        FGRDetailsId = c.Long(nullable: false, identity: true),
                        RawMaterialId = c.Long(nullable: false),
                        ReceipeBatchCode = c.String(),
                        FGRRawMaterQty = c.Double(nullable: false),
                        UnitId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        EUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        UpUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        FGRId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.FGRDetailsId)
                .ForeignKey("dbo.tblFinishGoodRecipeInfo", t => t.FGRId, cascadeDelete: true)
                .Index(t => t.FGRId);
            
            CreateTable(
                "dbo.tblFinishGoodRecipeInfo",
                c => new
                    {
                        FGRId = c.Long(nullable: false, identity: true),
                        FinishGoodProductId = c.Long(nullable: false),
                        ReceipeBatchCode = c.String(),
                        FGRQty = c.Int(nullable: false),
                        UnitId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        EUserId = c.Long(),
                        Status = c.String(),
                        EntryDate = c.DateTime(),
                        UpUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.FGRId);
            
            CreateTable(
                "dbo.tblFinishGoodSupplierInfo",
                c => new
                    {
                        FinishGoodSupplierId = c.Long(nullable: false, identity: true),
                        FinishGoodSupplierName = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        MobileNumber = c.Long(nullable: false),
                        Address = c.String(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.FinishGoodSupplierId);
            
            CreateTable(
                "dbo.tblMeasurement",
                c => new
                    {
                        MeasurementId = c.Long(nullable: false, identity: true),
                        MeasurementName = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        MasterCarton = c.Int(nullable: false),
                        InnerBox = c.Int(nullable: false),
                        PackSize = c.Double(nullable: false),
                        UnitId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.MeasurementId);
            
            CreateTable(
                "dbo.tblMRawMaterialIssueStockDetails",
                c => new
                    {
                        RawMaterialIssueStockDetailId = c.Long(nullable: false, identity: true),
                        RawMaterialId = c.Long(nullable: false),
                        Quantity = c.Double(nullable: false),
                        UnitID = c.Long(nullable: false),
                        IssueStatus = c.String(),
                        EntryDate = c.DateTime(),
                        RawMaterialIssueStockId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.RawMaterialIssueStockDetailId)
                .ForeignKey("dbo.tblMRawMaterialIssueStockInfo", t => t.RawMaterialIssueStockId, cascadeDelete: true)
                .Index(t => t.RawMaterialIssueStockId);
            
            CreateTable(
                "dbo.tblMRawMaterialIssueStockInfo",
                c => new
                    {
                        RawMaterialIssueStockId = c.Long(nullable: false, identity: true),
                        ProductBatchCode = c.String(),
                        Status = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        OrganizationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.RawMaterialIssueStockId);
            
            CreateTable(
                "dbo.tblPRawMaterialStockDetail",
                c => new
                    {
                        PRawMaterialStockDetailId = c.Long(nullable: false, identity: true),
                        RawMaterialId = c.Long(nullable: false),
                        Quantity = c.Double(nullable: false),
                        UnitID = c.Long(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        SubTotal = c.Double(nullable: false),
                        StockDate = c.DateTime(),
                        StockIssueDate = c.DateTime(),
                        Status = c.String(),
                        DepotId = c.Long(nullable: false),
                        ExpireDate = c.DateTime(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        PRawMaterialStockId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.PRawMaterialStockDetailId)
                .ForeignKey("dbo.tblPRawMaterialStockInfo", t => t.PRawMaterialStockId, cascadeDelete: true)
                .Index(t => t.PRawMaterialStockId);
            
            CreateTable(
                "dbo.tblPRawMaterialStockInfo",
                c => new
                    {
                        PRawMaterialStockId = c.Long(nullable: false, identity: true),
                        BatchCode = c.String(),
                        ChalanNo = c.String(),
                        ChalanDate = c.DateTime(nullable: false),
                        InvoiceNo = c.String(),
                        InvoiceDate = c.DateTime(nullable: false),
                        IssueStatus = c.String(),
                        TotalAmount = c.Double(nullable: false),
                        RawMaterialSupplierId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.PRawMaterialStockId);
            
            CreateTable(
                "dbo.tblProductSalesDetails",
                c => new
                    {
                        ProductSalesDetailsId = c.Long(nullable: false, identity: true),
                        FinishGoodProductInfoId = c.String(),
                        Quanity = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        MeasurementId = c.Long(nullable: false),
                        MeasurementSize = c.String(),
                        Discount = c.Double(nullable: false),
                        DiscountTk = c.Double(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        ProductSalesInfoId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ProductSalesDetailsId)
                .ForeignKey("dbo.tblProductSalesInfo", t => t.ProductSalesInfoId, cascadeDelete: true)
                .Index(t => t.ProductSalesInfoId);
            
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
                        PaidAmount = c.Double(nullable: false),
                        DueAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ProductSalesInfoId);
            
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
            
            CreateTable(
                "dbo.tblRawMaterialInfo",
                c => new
                    {
                        RawMaterialId = c.Long(nullable: false, identity: true),
                        OrganizationId = c.Long(nullable: false),
                        RawMaterialName = c.String(),
                        RoleId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(nullable: false),
                        DepotId = c.Long(nullable: false),
                        Status = c.String(),
                        UnitId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.RawMaterialId);
            
            CreateTable(
                "dbo.tblRawMaterialIssueStockDetails",
                c => new
                    {
                        RawMaterialIssueStockDetailsId = c.Long(nullable: false, identity: true),
                        OrganizationId = c.Long(nullable: false),
                        RawMaterialId = c.Long(nullable: false),
                        Quantity = c.Double(nullable: false),
                        UnitId = c.Long(nullable: false),
                        IssueDate = c.DateTime(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        Status = c.String(),
                        RawMaterialIssueStockId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.RawMaterialIssueStockDetailsId)
                .ForeignKey("dbo.tblRawMaterialIssueStockInfo", t => t.RawMaterialIssueStockId, cascadeDelete: true)
                .Index(t => t.RawMaterialIssueStockId);
            
            CreateTable(
                "dbo.tblRawMaterialIssueStockInfo",
                c => new
                    {
                        RawMaterialIssueStockId = c.Long(nullable: false, identity: true),
                        ProductBatchCode = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        FinishGoodProductId = c.Long(nullable: false),
                        RawMaterialId = c.Long(nullable: false),
                        Quantity = c.Double(nullable: false),
                        UnitId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.RawMaterialIssueStockId);
            
            CreateTable(
                "dbo.tblRawMaterialStockDetail",
                c => new
                    {
                        RawMaterialStockDetailId = c.Long(nullable: false, identity: true),
                        OrganizationId = c.Long(nullable: false),
                        RawMaterialId = c.Long(nullable: false),
                        Quantity = c.Double(nullable: false),
                        UnitId = c.Long(nullable: false),
                        StockDate = c.DateTime(),
                        StockIssueDate = c.DateTime(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        Status = c.String(),
                        RawMaterialSupplierId = c.Long(nullable: false),
                        RawMaterialStockId = c.Long(nullable: false),
                        ExpireDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.RawMaterialStockDetailId)
                .ForeignKey("dbo.tblRawMaterialStockInfo", t => t.RawMaterialStockId, cascadeDelete: true)
                .Index(t => t.RawMaterialStockId);
            
            CreateTable(
                "dbo.tblRawMaterialStockInfo",
                c => new
                    {
                        RawMaterialStockId = c.Long(nullable: false, identity: true),
                        BatchCode = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        RawMaterialId = c.Long(nullable: false),
                        Quantity = c.Double(nullable: false),
                        UnitId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        IssueStatus = c.String(),
                        ExpireDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.RawMaterialStockId);
            
            CreateTable(
                "dbo.tblRawMaterialSupplierInfo",
                c => new
                    {
                        RawMaterialSupplierId = c.Long(nullable: false, identity: true),
                        RawMaterialSupplierName = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        MobileNumber = c.String(),
                        Address = c.String(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        Status = c.String(),
                        TradeLicense = c.String(),
                        TIN = c.String(),
                        BIN = c.String(),
                    })
                .PrimaryKey(t => t.RawMaterialSupplierId);
            
            CreateTable(
                "dbo.tblRawMaterialTrackInfo",
                c => new
                    {
                        RawMaterialTrackId = c.Long(nullable: false, identity: true),
                        RawMaterialId = c.Long(nullable: false),
                        Quantity = c.Double(nullable: false),
                        IssueDate = c.DateTime(),
                        IssueStatus = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.RawMaterialTrackId);
            
            CreateTable(
                "dbo.tblRegionInfos",
                c => new
                    {
                        RegionId = c.Long(nullable: false, identity: true),
                        RegionName = c.String(),
                        Status = c.String(),
                        DivisionId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.RegionId);
            
            CreateTable(
                "dbo.tblReturnRawMaterial",
                c => new
                    {
                        ReturnRawMaterialId = c.Long(nullable: false, identity: true),
                        RawMaterialId = c.Long(nullable: false),
                        Quantity = c.Double(nullable: false),
                        UnitId = c.Long(nullable: false),
                        ReturnType = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.ReturnRawMaterialId);
            
            CreateTable(
                "dbo.tblRegionUser",
                c => new
                    {
                        RegionUserId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        RegionId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        Status = c.String(),
                        Flag = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.RegionUserId);
            
            CreateTable(
                "dbo.tblStockiestInfo",
                c => new
                    {
                        StockiestId = c.Long(nullable: false, identity: true),
                        StockiestName = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        TerritoryId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.StockiestId);
            
            CreateTable(
                "dbo.tblStockiestUser",
                c => new
                    {
                        StockiestUserId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        StockiestId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        Status = c.String(),
                        Flag = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.StockiestUserId);
            
            CreateTable(
                "dbo.tblTerritoryInfos",
                c => new
                    {
                        TerritoryId = c.Long(nullable: false, identity: true),
                        TerritoryName = c.String(),
                        Status = c.String(),
                        AreaId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.TerritoryId);
            
            CreateTable(
                "dbo.tblTerritoryUser",
                c => new
                    {
                        TerritoryUserId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        TerritoryId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        Status = c.String(),
                        Flag = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.TerritoryUserId);
            
            CreateTable(
                "dbo.tblUserAssign",
                c => new
                    {
                        UserAssignId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        ZoneId = c.Long(nullable: false),
                        DivisionId = c.Long(nullable: false),
                        RegionId = c.Long(nullable: false),
                        AreaId = c.Long(nullable: false),
                        TerritoryId = c.Long(nullable: false),
                        StockiestId = c.Long(nullable: false),
                        Remarks = c.String(),
                        Flag = c.String(),
                        Status = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.UserAssignId);
            
            CreateTable(
                "dbo.tblUserInfo",
                c => new
                    {
                        UserId = c.Long(nullable: false, identity: true),
                        UserName = c.String(),
                        MobileNumber = c.String(),
                        Designation = c.String(),
                        DepartmentName = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Status = c.String(),
                        RoleId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.tblZoneDetail",
                c => new
                    {
                        ZoneDetailId = c.Long(nullable: false, identity: true),
                        DivisionId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        ZoneId = c.Long(nullable: false),
                        Status = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.ZoneDetailId)
                .ForeignKey("dbo.tblZoneInfo", t => t.ZoneId, cascadeDelete: true)
                .Index(t => t.ZoneId);
            
            CreateTable(
                "dbo.tblZoneInfo",
                c => new
                    {
                        ZoneId = c.Long(nullable: false, identity: true),
                        ZoneName = c.String(),
                        DivisionId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        Status = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.ZoneId);
            
            CreateTable(
                "dbo.tblZoneInfos",
                c => new
                    {
                        ZoneId = c.Long(nullable: false, identity: true),
                        ZoneName = c.String(),
                        Status = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.ZoneId);
            
            CreateTable(
                "dbo.tblZoneUser",
                c => new
                    {
                        ZoneUserId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ZoneId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        Status = c.String(),
                        Flag = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ZoneUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblZoneDetail", "ZoneId", "dbo.tblZoneInfo");
            DropForeignKey("dbo.tblRawMaterialStockDetail", "RawMaterialStockId", "dbo.tblRawMaterialStockInfo");
            DropForeignKey("dbo.tblRawMaterialIssueStockDetails", "RawMaterialIssueStockId", "dbo.tblRawMaterialIssueStockInfo");
            DropForeignKey("dbo.tblProductSalesDetails", "ProductSalesInfoId", "dbo.tblProductSalesInfo");
            DropForeignKey("dbo.tblPRawMaterialStockDetail", "PRawMaterialStockId", "dbo.tblPRawMaterialStockInfo");
            DropForeignKey("dbo.tblMRawMaterialIssueStockDetails", "RawMaterialIssueStockId", "dbo.tblMRawMaterialIssueStockInfo");
            DropForeignKey("dbo.tblFinishGoodRecipeDetails", "FGRId", "dbo.tblFinishGoodRecipeInfo");
            DropIndex("dbo.tblZoneDetail", new[] { "ZoneId" });
            DropIndex("dbo.tblRawMaterialStockDetail", new[] { "RawMaterialStockId" });
            DropIndex("dbo.tblRawMaterialIssueStockDetails", new[] { "RawMaterialIssueStockId" });
            DropIndex("dbo.tblProductSalesDetails", new[] { "ProductSalesInfoId" });
            DropIndex("dbo.tblPRawMaterialStockDetail", new[] { "PRawMaterialStockId" });
            DropIndex("dbo.tblMRawMaterialIssueStockDetails", new[] { "RawMaterialIssueStockId" });
            DropIndex("dbo.tblFinishGoodRecipeDetails", new[] { "FGRId" });
            DropTable("dbo.tblZoneUser");
            DropTable("dbo.tblZoneInfos");
            DropTable("dbo.tblZoneInfo");
            DropTable("dbo.tblZoneDetail");
            DropTable("dbo.tblUserInfo");
            DropTable("dbo.tblUserAssign");
            DropTable("dbo.tblTerritoryUser");
            DropTable("dbo.tblTerritoryInfos");
            DropTable("dbo.tblStockiestUser");
            DropTable("dbo.tblStockiestInfo");
            DropTable("dbo.tblRegionUser");
            DropTable("dbo.tblReturnRawMaterial");
            DropTable("dbo.tblRegionInfos");
            DropTable("dbo.tblRawMaterialTrackInfo");
            DropTable("dbo.tblRawMaterialSupplierInfo");
            DropTable("dbo.tblRawMaterialStockInfo");
            DropTable("dbo.tblRawMaterialStockDetail");
            DropTable("dbo.tblRawMaterialIssueStockInfo");
            DropTable("dbo.tblRawMaterialIssueStockDetails");
            DropTable("dbo.tblRawMaterialInfo");
            DropTable("dbo.tblProductSalesPaymentHistory");
            DropTable("dbo.tblProductSalesInfo");
            DropTable("dbo.tblProductSalesDetails");
            DropTable("dbo.tblPRawMaterialStockInfo");
            DropTable("dbo.tblPRawMaterialStockDetail");
            DropTable("dbo.tblMRawMaterialIssueStockInfo");
            DropTable("dbo.tblMRawMaterialIssueStockDetails");
            DropTable("dbo.tblMeasurement");
            DropTable("dbo.tblFinishGoodSupplierInfo");
            DropTable("dbo.tblFinishGoodRecipeInfo");
            DropTable("dbo.tblFinishGoodRecipeDetails");
            DropTable("dbo.FinishGoodProductionInfoes");
            DropTable("dbo.FinishGoodProductionDetails");
            DropTable("dbo.tblFinishGoodProductInfo");
            DropTable("dbo.DivisionUsers");
            DropTable("dbo.tblDivisionInfo");
            DropTable("dbo.tblDistributionUser");
            DropTable("dbo.tblDepotInfo");
            DropTable("dbo.tblBankInfo");
            DropTable("dbo.tblAreaUser");
            DropTable("dbo.tblAreaSetup");
            DropTable("dbo.tblAgroUnitInfo");
        }
    }
}
