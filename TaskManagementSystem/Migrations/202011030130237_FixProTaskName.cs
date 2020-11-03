namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixProTaskName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Notifications", name: "TaskId", newName: "ProTaskId");
            RenameIndex(table: "dbo.Notifications", name: "IX_TaskId", newName: "IX_ProTaskId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Notifications", name: "IX_ProTaskId", newName: "IX_TaskId");
            RenameColumn(table: "dbo.Notifications", name: "ProTaskId", newName: "TaskId");
        }
    }
}
