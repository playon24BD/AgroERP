namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_Depo2 : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblPRawMaterialStockDetail", "PRawMaterialStockId", "dbo.tblPRawMaterialStockInfo");
            DropIndex("dbo.tblPRawMaterialStockDetail", new[] { "PRawMaterialStockId" });
            DropTable("dbo.tblPRawMaterialStockInfo");
            DropTable("dbo.tblPRawMaterialStockDetail");
        }
    }
}
