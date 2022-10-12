namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_Divisiondert : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblDivisionInfo", "RoleId", c => c.Long(nullable: false));
            AddColumn("dbo.tblDivisionInfo", "Status", c => c.String());
            DropColumn("dbo.tblDivisionInfo", "DivisionAssignName");
            DropColumn("dbo.tblDivisionInfo", "MobileNumber");
            DropColumn("dbo.tblDivisionInfo", "Remarks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblDivisionInfo", "Remarks", c => c.String());
            AddColumn("dbo.tblDivisionInfo", "MobileNumber", c => c.String());
            AddColumn("dbo.tblDivisionInfo", "DivisionAssignName", c => c.String());
            DropColumn("dbo.tblDivisionInfo", "Status");
            DropColumn("dbo.tblDivisionInfo", "RoleId");
        }
    }
}
