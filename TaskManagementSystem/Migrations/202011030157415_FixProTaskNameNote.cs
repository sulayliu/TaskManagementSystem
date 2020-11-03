namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixProTaskNameNote : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notes", "TaskId", "dbo.ProTasks");
            DropIndex("dbo.Notes", new[] { "TaskId" });
            RenameColumn(table: "dbo.Notes", name: "TaskId", newName: "ProTaskId");
            AlterColumn("dbo.Notes", "ProTaskId", c => c.Int());
            CreateIndex("dbo.Notes", "ProTaskId");
            AddForeignKey("dbo.Notes", "ProTaskId", "dbo.ProTasks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "ProTaskId", "dbo.ProTasks");
            DropIndex("dbo.Notes", new[] { "ProTaskId" });
            AlterColumn("dbo.Notes", "ProTaskId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Notes", name: "ProTaskId", newName: "TaskId");
            CreateIndex("dbo.Notes", "TaskId");
            AddForeignKey("dbo.Notes", "TaskId", "dbo.ProTasks", "Id", cascadeDelete: true);
        }
    }
}
