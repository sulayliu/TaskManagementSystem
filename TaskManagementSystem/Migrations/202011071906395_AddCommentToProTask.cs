namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentToProTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProTasks", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProTasks", "Comment");
        }
    }
}
