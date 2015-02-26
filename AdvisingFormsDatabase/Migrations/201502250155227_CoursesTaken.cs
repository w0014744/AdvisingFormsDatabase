namespace AdvisingFormsDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoursesTaken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Selected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Selected");
        }
    }
}
