namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_Zones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblZoneInfos",
                c => new
                    {
                        ZoneId = c.Long(nullable: false, identity: true),
                        ZoneName = c.String(),
                        Remarks = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.ZoneId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblZoneInfos");
        }
    }
}
