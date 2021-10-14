namespace ApplicationDev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTraineeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trainees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TraineeID = c.String(nullable: false),
                        Full_Name = c.String(),
                        Email = c.String(),
                        DateOfBirth = c.String(),
                        Education = c.String(),
                        Trainees_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Trainees_Id)
                .Index(t => t.Trainees_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainees", "Trainees_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Trainees", new[] { "Trainees_Id" });
            DropTable("dbo.Trainees");
        }
    }
}
