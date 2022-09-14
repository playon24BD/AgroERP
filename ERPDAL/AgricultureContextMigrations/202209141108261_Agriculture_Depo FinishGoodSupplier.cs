namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_DepoFinishGoodSupplier : DbMigration
    {
        public override void Up()
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
            
            AlterColumn("dbo.tblBankInfo", "MobileNumber", c => c.Long(nullable: false));
            DropColumn("dbo.tblBankInfo", "AccountNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblBankInfo", "AccountNumber", c => c.String());
            AlterColumn("dbo.tblBankInfo", "MobileNumber", c => c.String());
            DropTable("dbo.tblFinishGoodSupplierInfo");
        }
    }
}
