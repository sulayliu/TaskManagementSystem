namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDeadlinePropertyInProjectAndProTaskTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "CreatedTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "Deadline", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProTasks", "CreatedTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProTasks", "Deadline", c => c.DateTime(nullable: false));
            DropColumn("dbo.Projects", "Time");
            DropColumn("dbo.ProTasks", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProTasks", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.ProTasks", "Deadline");
            DropColumn("dbo.ProTasks", "CreatedTime");
            DropColumn("dbo.Projects", "Deadline");
            DropColumn("dbo.Projects", "CreatedTime");
        }
    }
}
