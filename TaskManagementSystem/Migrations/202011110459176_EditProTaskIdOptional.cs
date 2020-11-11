namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditProTaskIdOptional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notes", "ProTaskId", "dbo.ProTasks");
            DropIndex("dbo.Notes", new[] { "ProTaskId" });
            AlterColumn("dbo.Notes", "ProTaskId", c => c.Int());
            CreateIndex("dbo.Notes", "ProTaskId");
            AddForeignKey("dbo.Notes", "ProTaskId", "dbo.ProTasks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "ProTaskId", "dbo.ProTasks");
            DropIndex("dbo.Notes", new[] { "ProTaskId" });
            AlterColumn("dbo.Notes", "ProTaskId", c => c.Int(nullable: false));
            CreateIndex("dbo.Notes", "ProTaskId");
            AddForeignKey("dbo.Notes", "ProTaskId", "dbo.ProTasks", "Id", cascadeDelete: true);
        }
    }
}
