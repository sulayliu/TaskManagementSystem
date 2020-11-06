namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingUserRelationOnProject : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Projects", name: "ProjectManager_Id", newName: "UserId");
            RenameIndex(table: "dbo.Projects", name: "IX_ProjectManager_Id", newName: "IX_UserId");
            AddColumn("dbo.Projects", "UserName", c => c.String());
            DropColumn("dbo.Projects", "ManagerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "ManagerId", c => c.String());
            DropColumn("dbo.Projects", "UserName");
            RenameIndex(table: "dbo.Projects", name: "IX_UserId", newName: "IX_ProjectManager_Id");
            RenameColumn(table: "dbo.Projects", name: "UserId", newName: "ProjectManager_Id");
        }
    }
}
