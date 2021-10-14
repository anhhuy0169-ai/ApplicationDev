namespace ApplicationDev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTrainerTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TrainerID = c.String(nullable: false),
                        Full_Name = c.String(maxLength: 255),
                        Specialty = c.String(maxLength: 255),
                        Email = c.String(maxLength: 255),
                        Age = c.String(),
                        Address = c.String(maxLength: 255),
                        Trainers_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Trainers_Id)
                .Index(t => t.Trainers_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainers", "Trainers_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Trainers", new[] { "Trainers_Id" });
            DropTable("dbo.Trainers");
        }
    }
}
