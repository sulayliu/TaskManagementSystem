namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixContentOnProTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProTasks", "Content", c => c.String());
            DropColumn("dbo.ProTasks", "TaskContent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProTasks", "TaskContent", c => c.String());
            DropColumn("dbo.ProTasks", "Content");
        }
    }
}
