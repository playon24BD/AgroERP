namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_OrganizationId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblUserAssign", "OrganizationId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblUserAssign", "OrganizationId");
        }
    }
}
