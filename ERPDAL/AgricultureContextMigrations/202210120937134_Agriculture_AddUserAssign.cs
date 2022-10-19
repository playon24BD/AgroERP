namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_AddUserAssign : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblUserAssign",
                c => new
                    {
                        UserAssignId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ZoneId = c.Long(nullable: false),
                        DivisionId = c.Long(nullable: false),
                        RegionId = c.Long(nullable: false),
                        AreaId = c.Long(nullable: false),
                        TerritoryId = c.Long(nullable: false),
                        StockiestId = c.Long(nullable: false),
                        Remarks = c.String(),
                        Flag = c.String(),
                        Status = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.UserAssignId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblUserAssign");
        }
    }
}