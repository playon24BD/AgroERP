namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_RawMaterialReturn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblReturnRawMaterial",
                c => new
                    {
                        ReturnRawMaterialId = c.Long(nullable: false, identity: true),
                        RawMaterialId = c.Long(nullable: false),
                        Quantity = c.Double(nullable: false),
                        UnitId = c.Long(nullable: false),
                        ReturnType = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.ReturnRawMaterialId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblReturnRawMaterial");
        }
    }
}
