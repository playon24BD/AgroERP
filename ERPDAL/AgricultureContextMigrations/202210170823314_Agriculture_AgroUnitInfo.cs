namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_AgroUnitInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblAgroUnitInfo",
                c => new
                    {
                        UnitId = c.Long(nullable: false, identity: true),
                        UnitName = c.String(),
                        Status = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                    })
                .PrimaryKey(t => t.UnitId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblAgroUnitInfo");
        }
    }
}
