namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Priceconfiguration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblProductPriceConfiguration",
                c => new
                    {
                        ProductPriceConfigurationId = c.Long(nullable: false, identity: true),
                        FinishGoodProductId = c.Long(nullable: false),
                        FGRId = c.Long(nullable: false),
                        ProductPrice = c.Double(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUser = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUser = c.Long(),
                        Status = c.String(),
                        Flag = c.String(),
                    })
                .PrimaryKey(t => t.ProductPriceConfigurationId);
            
            CreateTable(
                "dbo.tblProductPricingHistory",
                c => new
                    {
                        ProductPricingHistoryId = c.Long(nullable: false, identity: true),
                        FinishGoodProductId = c.Long(nullable: false),
                        FGRId = c.Long(nullable: false),
                        ProductPrice = c.Double(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUser = c.Long(),
                        Status = c.String(),
                        Flag = c.String(),
                    })
                .PrimaryKey(t => t.ProductPricingHistoryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblProductPricingHistory");
            DropTable("dbo.tblProductPriceConfiguration");
        }
    }
}
