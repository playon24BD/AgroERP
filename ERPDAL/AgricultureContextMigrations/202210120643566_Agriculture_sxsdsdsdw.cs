namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_sxsdsdsdw : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.tblTerritoryInfos");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tblTerritoryInfos",
                c => new
                    {
                        TerritoryId = c.Long(nullable: false, identity: true),
                        TerritoryName = c.String(),
                        Status = c.String(),
                        AreaId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.TerritoryId);
            
        }
    }
}
