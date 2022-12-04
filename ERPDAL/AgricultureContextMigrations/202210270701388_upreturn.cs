namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upreturn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblReturnRawMaterial", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblReturnRawMaterial", "Status");
        }
    }
}
