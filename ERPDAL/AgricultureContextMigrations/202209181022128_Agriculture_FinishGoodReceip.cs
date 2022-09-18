namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_FinishGoodReceip : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblFinishGoodRecipeDetails",
                c => new
                    {
                        FGRDetailsId = c.Long(nullable: false, identity: true),
                        RawMaterialId = c.Long(nullable: false),
                        FGRRawMaterQty = c.Int(nullable: false),
                        FGRRawMaterUnit = c.String(),
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
                        FGRQty = c.Int(nullable: false),
                        FGRUnit = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        EUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        UpUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.FGRId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblFinishGoodRecipeDetails", "FGRId", "dbo.tblFinishGoodRecipeInfo");
            DropIndex("dbo.tblFinishGoodRecipeDetails", new[] { "FGRId" });
            DropTable("dbo.tblFinishGoodRecipeInfo");
            DropTable("dbo.tblFinishGoodRecipeDetails");
        }
    }
}
