namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_UserInfoMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblUserInfo",
                c => new
                    {
                        UserId = c.Long(nullable: false, identity: true),
                        UserName = c.String(),
                        MobileNumber = c.String(),
                        Designation = c.String(),
                        DepartmentName = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Status = c.String(),
                        RoleId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblUserInfo");
        }
    }
}
