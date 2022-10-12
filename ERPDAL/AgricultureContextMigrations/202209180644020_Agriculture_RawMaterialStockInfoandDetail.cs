namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_RawMaterialStockInfoandDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblRawMaterialStockDetail",
                c => new
                    {
                        RawMaterialStockDetailId = c.Long(nullable: false, identity: true),
                        OrganizationId = c.Long(nullable: false),
                        RawMaterialId = c.Long(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Unit = c.String(),
                        StockDate = c.DateTime(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        Status = c.String(),
                        RawMaterialStockId = c.Long(nullable: false),
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
                        Quantity = c.Int(nullable: false),
                        Unit = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.RawMaterialStockId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblRawMaterialStockDetail", "RawMaterialStockId", "dbo.tblRawMaterialStockInfo");
            DropIndex("dbo.tblRawMaterialStockDetail", new[] { "RawMaterialStockId" });
            DropTable("dbo.tblRawMaterialStockInfo");
            DropTable("dbo.tblRawMaterialStockDetail");
        }
    }
}
