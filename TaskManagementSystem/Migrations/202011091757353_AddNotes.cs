namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notes", "DeveloperId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notes", "ProTaskId", "dbo.ProTasks");
            DropIndex("dbo.Notes", new[] { "ProTaskId" });
            DropIndex("dbo.Notes", new[] { "DeveloperId" });
            AddColumn("dbo.Notes", "UserId", c => c.String());
            AddColumn("dbo.Notes", "ProjectId", c => c.Int(nullable: false));
            AddColumn("dbo.Notes", "Priority", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Notes", "ProTaskId", c => c.Int(nullable: false));
            CreateIndex("dbo.Notes", "ProjectId");
            CreateIndex("dbo.Notes", "ProTaskId");
            AddForeignKey("dbo.Notes", "ProjectId", "dbo.Projects", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Notes", "ProTaskId", "dbo.ProTasks", "Id", cascadeDelete: false);
            DropColumn("dbo.Notes", "DeveloperId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notes", "DeveloperId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Notes", "ProTaskId", "dbo.ProTasks");
            DropForeignKey("dbo.Notes", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Notes", new[] { "ProTaskId" });
            DropIndex("dbo.Notes", new[] { "ProjectId" });
            AlterColumn("dbo.Notes", "ProTaskId", c => c.Int());
            DropColumn("dbo.Notes", "Priority");
            DropColumn("dbo.Notes", "ProjectId");
            DropColumn("dbo.Notes", "UserId");
            CreateIndex("dbo.Notes", "DeveloperId");
            CreateIndex("dbo.Notes", "ProTaskId");
            AddForeignKey("dbo.Notes", "ProTaskId", "dbo.ProTasks", "Id");
            AddForeignKey("dbo.Notes", "DeveloperId", "dbo.AspNetUsers", "Id");
        }
    }
}
