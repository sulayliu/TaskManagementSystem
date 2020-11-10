namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOverdueTaskField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProTasks", "IsItOverdue", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProTasks", "IsItOverdue");
        }
    }
}
