namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductionCOst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblProductionPerproductCost",
                c => new
                    {
                        ProductionPerproductCostId = c.Long(nullable: false, identity: true),
                        FinishGoodProductId = c.Long(nullable: false),
                        FGRId = c.Long(nullable: false),
                        PerProductRMtotalCost = c.Double(nullable: false),
                        PerProductOtherCost = c.Double(nullable: false),
                        PerProductMainCost = c.Double(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUser = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUser = c.Long(),
                        Status = c.String(),
                        Flag = c.String(),
                    })
                .PrimaryKey(t => t.ProductionPerproductCostId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblProductionPerproductCost");
        }
    }
}
