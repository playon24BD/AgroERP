namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_ZoneInfoAndDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblZoneDetail",
                c => new
                    {
                        ZoneDetailId = c.Long(nullable: false, identity: true),
                        DivisionId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        ZoneId = c.Long(nullable: false),
                        Status = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.ZoneDetailId)
                .ForeignKey("dbo.tblZoneInfo", t => t.ZoneId, cascadeDelete: true)
                .Index(t => t.ZoneId);
            
            CreateTable(
                "dbo.tblZoneInfo",
                c => new
                    {
                        ZoneId = c.Long(nullable: false, identity: true),
                        ZoneName = c.String(),
                        DivisionId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        Status = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.ZoneId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblZoneDetail", "ZoneId", "dbo.tblZoneInfo");
            DropIndex("dbo.tblZoneDetail", new[] { "ZoneId" });
            DropTable("dbo.tblZoneInfo");
            DropTable("dbo.tblZoneDetail");
        }
    }
}
