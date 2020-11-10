namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBudgetFieldToProjectTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Budget", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Budget");
        }
    }
}
