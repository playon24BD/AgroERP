namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_BankSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblBankInfo",
                c => new
                    {
                        BankId = c.Long(nullable: false, identity: true),
                        BankName = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.BankId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblBankInfo");
        }
    }
}
