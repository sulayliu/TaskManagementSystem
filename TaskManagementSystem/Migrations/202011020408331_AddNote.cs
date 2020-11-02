namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NoteContent = c.String(),
                        TaskId = c.Int(nullable: false),
                        DeveloperId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DeveloperId)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId)
                .Index(t => t.DeveloperId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.Notes", "DeveloperId", "dbo.AspNetUsers");
            DropIndex("dbo.Notes", new[] { "DeveloperId" });
            DropIndex("dbo.Notes", new[] { "TaskId" });
            DropTable("dbo.Notes");
        }
    }
}
