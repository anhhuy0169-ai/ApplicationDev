namespace ApplicationDev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyTraineeTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainees", "Age", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trainees", "Age");
        }
    }
}
