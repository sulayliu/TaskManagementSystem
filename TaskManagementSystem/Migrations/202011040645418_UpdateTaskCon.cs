namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTaskCon : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Projects", name: "ProjectManager_Id", newName: "UserId");
            RenameColumn(table: "dbo.ProTasks", name: "DeveloperId", newName: "UserId");
            RenameIndex(table: "dbo.Projects", name: "IX_ProjectManager_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.ProTasks", name: "IX_DeveloperId", newName: "IX_UserId");
            AddColumn("dbo.Projects", "Name", c => c.String());
            AddColumn("dbo.Projects", "UserName", c => c.String());
            AddColumn("dbo.ProTasks", "UserName", c => c.String());
            DropColumn("dbo.Projects", "ManagerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "ManagerId", c => c.String());
            DropColumn("dbo.ProTasks", "UserName");
            DropColumn("dbo.Projects", "UserName");
            DropColumn("dbo.Projects", "Name");
            RenameIndex(table: "dbo.ProTasks", name: "IX_UserId", newName: "IX_DeveloperId");
            RenameIndex(table: "dbo.Projects", name: "IX_UserId", newName: "IX_ProjectManager_Id");
            RenameColumn(table: "dbo.ProTasks", name: "UserId", newName: "DeveloperId");
            RenameColumn(table: "dbo.Projects", name: "UserId", newName: "ProjectManager_Id");
        }
    }
}
