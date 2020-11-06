namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingProTaskUserID : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProTasks", name: "DeveloperId", newName: "UserId");
            RenameIndex(table: "dbo.ProTasks", name: "IX_DeveloperId", newName: "IX_UserId");
            AddColumn("dbo.ProTasks", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProTasks", "UserName");
            RenameIndex(table: "dbo.ProTasks", name: "IX_UserId", newName: "IX_DeveloperId");
            RenameColumn(table: "dbo.ProTasks", name: "UserId", newName: "DeveloperId");
        }
    }
}
