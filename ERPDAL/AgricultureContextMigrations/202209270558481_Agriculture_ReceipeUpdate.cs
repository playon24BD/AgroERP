namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_ReceipeUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblFinishGoodRecipeDetails", "FGRRawMaterQty", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblFinishGoodRecipeDetails", "FGRRawMaterQty", c => c.Int(nullable: false));
        }
    }
}
