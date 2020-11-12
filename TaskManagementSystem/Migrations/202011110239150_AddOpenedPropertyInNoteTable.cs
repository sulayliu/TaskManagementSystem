namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOpenedPropertyInNoteTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notes", "IsOpened", c => c.Boolean(nullable: false));
            AddColumn("dbo.Notes", "NotificationType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notes", "NotificationType");
            DropColumn("dbo.Notes", "IsOpened");
        }
    }
}
