namespace ApplicationDev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableAssignTrainerToCourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignTrainerToCourses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TrainerID = c.String(maxLength: 128),
                        CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.TrainerID)
                .Index(t => t.TrainerID)
                .Index(t => t.CourseID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignTrainerToCourses", "TrainerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AssignTrainerToCourses", "CourseID", "dbo.Courses");
            DropIndex("dbo.AssignTrainerToCourses", new[] { "CourseID" });
            DropIndex("dbo.AssignTrainerToCourses", new[] { "TrainerID" });
            DropTable("dbo.AssignTrainerToCourses");
        }
    }
}
