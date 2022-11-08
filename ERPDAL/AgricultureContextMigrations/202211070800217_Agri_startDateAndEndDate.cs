namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agri_startDateAndEndDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblCommissionOnProduct", "StartDate", c => c.DateTime());
            AddColumn("dbo.tblCommissionOnProduct", "EndDate", c => c.DateTime());
            AddColumn("dbo.tblCommissionProductHistory", "StartDate", c => c.DateTime());
            AddColumn("dbo.tblCommissionProductHistory", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblCommissionProductHistory", "EndDate");
            DropColumn("dbo.tblCommissionProductHistory", "StartDate");
            DropColumn("dbo.tblCommissionOnProduct", "EndDate");
            DropColumn("dbo.tblCommissionOnProduct", "StartDate");
        }
    }
}
