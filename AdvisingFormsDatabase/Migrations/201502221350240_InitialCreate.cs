namespace AdvisingFormsDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaseCourses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Department = c.String(),
                        CourseNumber = c.Int(nullable: false),
                        CreditHours = c.Int(nullable: false),
                        Concentration_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Concentrations", t => t.Concentration_ID)
                .Index(t => t.Concentration_ID);
            
            CreateTable(
                "dbo.Concentrations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        HoursRequired = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        BaseCourseID = c.Int(nullable: false),
                        Semester = c.String(),
                        Grade = c.Int(),
                        Student_ID = c.Int(),
                        Student_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BaseCourses", t => t.BaseCourseID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_ID)
                .ForeignKey("dbo.Students", t => t.Student_ID1)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.BaseCourseID)
                .Index(t => t.Student_ID)
                .Index(t => t.Student_ID1);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        WNumber = c.String(),
                        HoursCompleted = c.Int(nullable: false),
                        GPA = c.Double(nullable: false),
                        ConcentrationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Concentrations", t => t.ConcentrationID, cascadeDelete: true)
                .Index(t => t.ConcentrationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Students", "ConcentrationID", "dbo.Concentrations");
            DropForeignKey("dbo.Courses", "Student_ID1", "dbo.Students");
            DropForeignKey("dbo.Courses", "Student_ID", "dbo.Students");
            DropForeignKey("dbo.Courses", "BaseCourseID", "dbo.BaseCourses");
            DropForeignKey("dbo.BaseCourses", "Concentration_ID", "dbo.Concentrations");
            DropIndex("dbo.Students", new[] { "ConcentrationID" });
            DropIndex("dbo.Courses", new[] { "Student_ID1" });
            DropIndex("dbo.Courses", new[] { "Student_ID" });
            DropIndex("dbo.Courses", new[] { "BaseCourseID" });
            DropIndex("dbo.Courses", new[] { "StudentID" });
            DropIndex("dbo.BaseCourses", new[] { "Concentration_ID" });
            DropTable("dbo.Students");
            DropTable("dbo.Courses");
            DropTable("dbo.Concentrations");
            DropTable("dbo.BaseCourses");
        }
    }
}
