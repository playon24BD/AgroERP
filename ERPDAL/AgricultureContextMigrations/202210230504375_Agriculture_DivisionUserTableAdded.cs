namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_DivisionUserTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DivisionUsers",
                c => new
                    {
                        DivisionUserId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        DivisionId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.DivisionUserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DivisionUsers");
        }
    }
}
