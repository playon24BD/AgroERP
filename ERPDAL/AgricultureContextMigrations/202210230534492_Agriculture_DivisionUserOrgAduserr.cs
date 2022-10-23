namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_DivisionUserOrgAduserr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DivisionUsers", "OrganizationId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DivisionUsers", "OrganizationId");
        }
    }
}
