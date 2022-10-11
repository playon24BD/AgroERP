namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_Areadddedee : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tblAreaSetup", "AreaAsignName");
            DropColumn("dbo.tblAreaSetup", "MobileNumber");
            DropColumn("dbo.tblAreaSetup", "Remarks");
            DropColumn("dbo.tblAreaSetup", "ZoneId");
            DropColumn("dbo.tblAreaSetup", "DivisionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblAreaSetup", "DivisionId", c => c.Long(nullable: false));
            AddColumn("dbo.tblAreaSetup", "ZoneId", c => c.Long(nullable: false));
            AddColumn("dbo.tblAreaSetup", "Remarks", c => c.String());
            AddColumn("dbo.tblAreaSetup", "MobileNumber", c => c.String());
            AddColumn("dbo.tblAreaSetup", "AreaAsignName", c => c.String());
        }
    }
}
