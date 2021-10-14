namespace ApplicationDev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableAssignTraineeToCourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignTraineeToCourses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TraineeID = c.String(maxLength: 128),
                        CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.TraineeID)
                .Index(t => t.TraineeID)
                .Index(t => t.CourseID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignTraineeToCourses", "TraineeID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AssignTraineeToCourses", "CourseID", "dbo.Courses");
            DropIndex("dbo.AssignTraineeToCourses", new[] { "CourseID" });
            DropIndex("dbo.AssignTraineeToCourses", new[] { "TraineeID" });
            DropTable("dbo.AssignTraineeToCourses");
        }
    }
}
