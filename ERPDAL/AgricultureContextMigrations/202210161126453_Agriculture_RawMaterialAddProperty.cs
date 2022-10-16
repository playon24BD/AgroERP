namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_RawMaterialAddProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblRawMaterialInfo", "Unit", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblRawMaterialInfo", "Unit");
        }
    }
}
