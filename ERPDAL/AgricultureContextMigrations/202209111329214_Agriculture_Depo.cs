namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_Depo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblDepotInfo",
                c => new
                    {
                        DepotId = c.Long(nullable: false, identity: true),
                        OrganizationId = c.Long(nullable: false),
                        DepotName = c.String(),
                        RoleId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(nullable: false),
                        EntryUserId = c.Long(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.DepotId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblDepotInfo");
        }
    }
}
