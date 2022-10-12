namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_Rawmererial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblRawMaterialInfo", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblRawMaterialInfo", "Status");
        }
    }
}
