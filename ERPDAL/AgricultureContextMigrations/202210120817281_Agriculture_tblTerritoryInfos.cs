namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_tblTerritoryInfos : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.tblTerritoryInfos");
        }
    }
}
