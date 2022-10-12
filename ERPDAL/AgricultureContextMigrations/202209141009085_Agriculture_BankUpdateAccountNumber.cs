namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_BankUpdateAccountNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblBankInfo", "AccountNumber", c => c.String());
            AlterColumn("dbo.tblBankInfo", "MobileNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblBankInfo", "MobileNumber", c => c.Long(nullable: false));
            DropColumn("dbo.tblBankInfo", "AccountNumber");
        }
    }
}
