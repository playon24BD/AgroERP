namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_DepoFinishProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblFinishGoodProductInfo",
                c => new
                    {
                        FinishGoodProductId = c.Long(nullable: false, identity: true),
                        FinishGoodProductName = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UpdateUser = c.Long(nullable: false),
                        EntryDate = c.DateTime(nullable: false),
                        EntryUser = c.Long(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.FinishGoodProductId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblFinishGoodProductInfo");
        }
    }
}
