namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserRelationNotes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notes", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Notes", "UserId");
            AddForeignKey("dbo.Notes", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Notes", new[] { "UserId" });
            AlterColumn("dbo.Notes", "UserId", c => c.String());
        }
    }
}
