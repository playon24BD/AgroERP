namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_zoneUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblZoneUser",
                c => new
                    {
                        ZoneUserId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ZoneId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        Status = c.String(),
                        Flag = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ZoneUserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblZoneUser");
        }
    }
}
