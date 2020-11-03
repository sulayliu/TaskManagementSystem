namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTasknameOnNoteClass : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Notes", name: "TaskId", newName: "ProTaskId");
            RenameIndex(table: "dbo.Notes", name: "IX_TaskId", newName: "IX_ProTaskId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Notes", name: "IX_ProTaskId", newName: "IX_TaskId");
            RenameColumn(table: "dbo.Notes", name: "ProTaskId", newName: "TaskId");
        }
    }
}
