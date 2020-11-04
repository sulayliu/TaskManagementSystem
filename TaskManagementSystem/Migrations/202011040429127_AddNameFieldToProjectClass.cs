namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameFieldToProjectClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Name");
        }
    }
}
