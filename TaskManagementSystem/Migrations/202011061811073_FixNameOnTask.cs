namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixNameOnTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProTasks", "Name", c => c.String());
           
        }
        
        public override void Down()
        {
           
            DropColumn("dbo.ProTasks", "Name");
        }
    }
}
