namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_RawMaterialSupplier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblRawMaterialSupplierInfo",
                c => new
                    {
                        RawMaterialSupplierId = c.Long(nullable: false, identity: true),
                        RawMaterialSupplierName = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        MobileNumber = c.String(),
                        Address = c.String(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.RawMaterialSupplierId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblRawMaterialSupplierInfo");
        }
    }
}
