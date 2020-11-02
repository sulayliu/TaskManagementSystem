namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectTaskClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        ManagerId = c.String(),
                        ProjectManager_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ProjectManager_Id)
                .Index(t => t.ProjectManager_Id);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        TaskContent = c.String(),
                        DeveloperId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DeveloperId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.DeveloperId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Tasks", "DeveloperId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projects", "ProjectManager_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Tasks", new[] { "DeveloperId" });
            DropIndex("dbo.Tasks", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "ProjectManager_Id" });
            DropTable("dbo.Tasks");
            DropTable("dbo.Projects");
        }
    }
}
