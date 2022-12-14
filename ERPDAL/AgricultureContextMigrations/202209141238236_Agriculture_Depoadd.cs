namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_Depoadd : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.tblFinishGoodSupplierInfo");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tblFinishGoodSupplierInfo",
                c => new
                    {
                        FinishGoodSupplierId = c.Long(nullable: false, identity: true),
                        FinishGoodSupplierName = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        MobileNumber = c.Long(nullable: false),
                        Address = c.String(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.FinishGoodSupplierId);
            
        }
    }
}
