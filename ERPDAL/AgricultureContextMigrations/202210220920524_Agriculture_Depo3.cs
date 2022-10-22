namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_Depo3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblMRawMaterialIssueStockDetails",
                c => new
                    {
                        RawMaterialIssueStockDetailId = c.Long(nullable: false, identity: true),
                        RawMaterialId = c.Long(nullable: false),
                        Quantity = c.Double(nullable: false),
                        UnitID = c.Long(nullable: false),
                        IssueStatus = c.Long(nullable: false),
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
                        IssueStatus = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        OrganizationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.RawMaterialIssueStockId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblMRawMaterialIssueStockDetails", "RawMaterialIssueStockId", "dbo.tblMRawMaterialIssueStockInfo");
            DropIndex("dbo.tblMRawMaterialIssueStockDetails", new[] { "RawMaterialIssueStockId" });
            DropTable("dbo.tblRawMaterialTrackInfo");
            DropTable("dbo.tblMRawMaterialIssueStockInfo");
            DropTable("dbo.tblMRawMaterialIssueStockDetails");
        }
    }
}

//ThreeTableadd, MRAwInfo,MRAwDetails, RawTruck