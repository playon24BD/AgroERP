namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_tblRawMaterialIssueStockInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblRawMaterialIssueStockDetails",
                c => new
                    {
                        RawMaterialIssueStockDetailsId = c.Long(nullable: false, identity: true),
                        OrganizationId = c.Long(nullable: false),
                        RawMaterialId = c.Long(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Unit = c.String(),
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
                        Quantity = c.Int(nullable: false),
                        Unit = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.RawMaterialIssueStockId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblRawMaterialIssueStockDetails", "RawMaterialIssueStockId", "dbo.tblRawMaterialIssueStockInfo");
            DropIndex("dbo.tblRawMaterialIssueStockDetails", new[] { "RawMaterialIssueStockId" });
            DropTable("dbo.tblRawMaterialIssueStockInfo");
            DropTable("dbo.tblRawMaterialIssueStockDetails");
        }
    }
}
