namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_productionfinishGoodInfoandDetailsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FinishGoodProductionDetails",
                c => new
                    {
                        FinishGoodProductDetailId = c.Long(nullable: false, identity: true),
                        FinishGoodProductionBatch = c.String(),
                        ReceipeBatchCode = c.String(),
                        RawMaterialId = c.Long(nullable: false),
                        FGRRawMaterQty = c.Int(nullable: false),
                        TotalQuantity = c.Int(nullable: false),
                        RequiredQuantity = c.Int(nullable: false),
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
                        Quanity = c.Int(nullable: false),
                        TargetQuantity = c.Int(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FinishGoodProductionInfoes");
            DropTable("dbo.FinishGoodProductionDetails");
        }
    }
}
