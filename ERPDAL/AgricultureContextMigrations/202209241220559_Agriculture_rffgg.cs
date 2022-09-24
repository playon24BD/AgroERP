namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_rffgg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblFinishGoodRecipeInfo", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblFinishGoodRecipeInfo", "Status");
        }
    }
}
