namespace PerceiveServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrentTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Training", "CurrentTaskId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Training", "CurrentTaskId");
        }
    }
}
