namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixNameOnTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProTasks", "Name", c => c.String());
            DropColumn("dbo.ProTasks", "TaskName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProTasks", "TaskName", c => c.String());
            DropColumn("dbo.ProTasks", "Name");
        }
    }
}
