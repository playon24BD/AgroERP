namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_BankMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblBankInfo", "MobileNumber", c => c.Long(nullable: false));
            AddColumn("dbo.tblBankInfo", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblBankInfo", "Email");
            DropColumn("dbo.tblBankInfo", "MobileNumber");
        }
    }
}
