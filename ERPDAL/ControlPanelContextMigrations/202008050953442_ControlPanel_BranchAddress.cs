namespace ERPDAL.ControlPanelContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ControlPanel_BranchAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblBranch", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblBranch", "Address");
        }
    }
}
