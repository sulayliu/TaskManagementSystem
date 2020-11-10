namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTheBrokenProject : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notes", "ProTaskId", "dbo.ProTasks");
            DropIndex("dbo.Notes", new[] { "ProTaskId" });
            RenameColumn(table: "dbo.Notes", name: "DeveloperId", newName: "UserId");
            RenameIndex(table: "dbo.Notes", name: "IX_DeveloperId", newName: "IX_UserId");
            AddColumn("dbo.Notes", "ProjectId", c => c.Int(nullable: false));
            AddColumn("dbo.Notes", "Priority", c => c.Boolean(nullable: false));
            AddColumn("dbo.Projects", "Budget", c => c.Double(nullable: false));
            AlterColumn("dbo.Notes", "ProTaskId", c => c.Int(nullable: false));
            CreateIndex("dbo.Notes", "ProjectId");
            CreateIndex("dbo.Notes", "ProTaskId");
            AddForeignKey("dbo.Notes", "ProjectId", "dbo.Projects", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Notes", "ProTaskId", "dbo.ProTasks", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "ProTaskId", "dbo.ProTasks");
            DropForeignKey("dbo.Notes", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Notes", new[] { "ProTaskId" });
            DropIndex("dbo.Notes", new[] { "ProjectId" });
            AlterColumn("dbo.Notes", "ProTaskId", c => c.Int());
            DropColumn("dbo.Projects", "Budget");
            DropColumn("dbo.Notes", "Priority");
            DropColumn("dbo.Notes", "ProjectId");
            RenameIndex(table: "dbo.Notes", name: "IX_UserId", newName: "IX_DeveloperId");
            RenameColumn(table: "dbo.Notes", name: "UserId", newName: "DeveloperId");
            CreateIndex("dbo.Notes", "ProTaskId");
            AddForeignKey("dbo.Notes", "ProTaskId", "dbo.ProTasks", "Id");
        }
    }
}
