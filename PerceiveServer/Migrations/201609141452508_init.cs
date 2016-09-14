namespace PerceiveServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignment",
                c => new
                    {
                        AssignmentID = c.Long(nullable: false, identity: true),
                        TrainingID = c.Long(nullable: false),
                        TaskID = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AssignmentID)
                .ForeignKey("dbo.Task", t => t.TaskID, cascadeDelete: true)
                .ForeignKey("dbo.Training", t => t.TrainingID, cascadeDelete: true)
                .Index(t => t.TrainingID)
                .Index(t => t.TaskID);
            
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        SourceID = c.String(),
                        DestinationID = c.String(),
                        SelectLevel = c.Int(nullable: false),
                        Instruction = c.String(),
                        SelectTransmitType = c.Int(nullable: false),
                        FaultCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Training",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CreatDate = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        PauseTime = c.DateTime(),
                        PausePosition = c.String(),
                        CurrentTaskId = c.Long(),
                        FaultCount = c.Int(nullable: false),
                        FinishDate = c.DateTime(),
                        IsFinished = c.Boolean(nullable: false),
                        UserId = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FullName = c.String(),
                        Age = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        PassWord = c.String(),
                        Username = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LoginDate = c.DateTime(),
                        IsOnline = c.Boolean(nullable: false),
                        ConnectionID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Training", "UserId", "dbo.User");
            DropForeignKey("dbo.Assignment", "TrainingID", "dbo.Training");
            DropForeignKey("dbo.Assignment", "TaskID", "dbo.Task");
            DropIndex("dbo.Training", new[] { "UserId" });
            DropIndex("dbo.Assignment", new[] { "TaskID" });
            DropIndex("dbo.Assignment", new[] { "TrainingID" });
            DropTable("dbo.User");
            DropTable("dbo.Training");
            DropTable("dbo.Task");
            DropTable("dbo.Assignment");
        }
    }
}
