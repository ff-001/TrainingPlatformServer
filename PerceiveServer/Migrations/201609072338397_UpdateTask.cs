namespace PerceiveServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTask : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Task", "SourceID", c => c.String());
            AlterColumn("dbo.Task", "DestinationID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Task", "DestinationID", c => c.Int(nullable: false));
            AlterColumn("dbo.Task", "SourceID", c => c.Int(nullable: false));
        }
    }
}
