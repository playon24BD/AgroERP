namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Accessoriestbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblAccessoriesInfo",
                c => new
                    {
                        AccessoriesId = c.Long(nullable: false, identity: true),
                        AccessoriesName = c.String(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.AccessoriesId);
            
            CreateTable(
                "dbo.tblAccessoriesPurchaseDetails",
                c => new
                    {
                        AccessoriesPurchaseDetailsId = c.Long(nullable: false, identity: true),
                        AccessoriesId = c.Long(nullable: false),
                        Quantity = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        SubTotal = c.Double(nullable: false),
                        Status = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        AccessoriesPurchaseInfoId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AccessoriesPurchaseDetailsId)
                .ForeignKey("dbo.tblAccessoriesPurchaseInfo", t => t.AccessoriesPurchaseInfoId, cascadeDelete: true)
                .Index(t => t.AccessoriesPurchaseInfoId);
            
            CreateTable(
                "dbo.tblAccessoriesPurchaseInfo",
                c => new
                    {
                        AccessoriesPurchaseInfoId = c.Long(nullable: false, identity: true),
                        BatchCode = c.String(),
                        InvoiceNo = c.String(),
                        InvoiceDate = c.DateTime(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        RawMaterialSupplierId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        Flag = c.String(),
                    })
                .PrimaryKey(t => t.AccessoriesPurchaseInfoId);
            
            CreateTable(
                "dbo.tblAccessoriesTrackInfo",
                c => new
                    {
                        AccessoriesTrackInfoId = c.Long(nullable: false, identity: true),
                        AccessoriesId = c.Long(nullable: false),
                        Quantity = c.Double(nullable: false),
                        IssueStatus = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        AccessoriesPurchaseInfoId = c.Long(nullable: false),
                        ProductSalesInfoId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AccessoriesTrackInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblAccessoriesPurchaseDetails", "AccessoriesPurchaseInfoId", "dbo.tblAccessoriesPurchaseInfo");
            DropIndex("dbo.tblAccessoriesPurchaseDetails", new[] { "AccessoriesPurchaseInfoId" });
            DropTable("dbo.tblAccessoriesTrackInfo");
            DropTable("dbo.tblAccessoriesPurchaseInfo");
            DropTable("dbo.tblAccessoriesPurchaseDetails");
            DropTable("dbo.tblAccessoriesInfo");
        }
    }
}
