namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserManagerClass : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Tasks", newName: "ProTasks");
            AddColumn("dbo.Notes", "Comment", c => c.String());
            AddColumn("dbo.Projects", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "IsCompleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Notifications", "Details", c => c.String());
            AddColumn("dbo.ProTasks", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProTasks", "CompletedPercentage", c => c.Double(nullable: false));
            DropColumn("dbo.Notes", "NoteContent");
            DropColumn("dbo.Notifications", "NContent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "NContent", c => c.String());
            AddColumn("dbo.Notes", "NoteContent", c => c.String());
            DropColumn("dbo.ProTasks", "CompletedPercentage");
            DropColumn("dbo.ProTasks", "Time");
            DropColumn("dbo.Notifications", "Details");
            DropColumn("dbo.Projects", "IsCompleted");
            DropColumn("dbo.Projects", "Time");
            DropColumn("dbo.Notes", "Comment");
            RenameTable(name: "dbo.ProTasks", newName: "Tasks");
        }
    }
}
