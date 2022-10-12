namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_RawMaterial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblRawMaterialInfo",
                c => new
                    {
                        RawMaterialId = c.Long(nullable: false, identity: true),
                        OrganizationId = c.Long(nullable: false),
                        RawMaterialName = c.String(),
                        RoleId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(nullable: false),
                        EntryUserId = c.Long(nullable: false),
                        DepotId = c.Long(nullable: false),
                        ExpireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RawMaterialId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblRawMaterialInfo");
        }
    }
}
