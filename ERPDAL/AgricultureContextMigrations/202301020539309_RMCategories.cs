namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RMCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblRMCategories",
                c => new
                    {
                        RMCategorieId = c.Long(nullable: false, identity: true),
                        RMCategorieName = c.String(),
                        Flag = c.String(),
                    })
                .PrimaryKey(t => t.RMCategorieId);
            
            AddColumn("dbo.tblPackageDetails", "AccessoriesId", c => c.Long(nullable: false));
            AddColumn("dbo.tblPackageDetails", "AccessoriesQuanity", c => c.Double(nullable: false));
            AddColumn("dbo.tblRawMaterialInfo", "RMCategorieId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblRawMaterialInfo", "RMCategorieId");
            DropColumn("dbo.tblPackageDetails", "AccessoriesQuanity");
            DropColumn("dbo.tblPackageDetails", "AccessoriesId");
            DropTable("dbo.tblRMCategories");
        }
    }
}
