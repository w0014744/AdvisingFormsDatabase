namespace CMPSAdvising.Migrations
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
                "dbo.Prerequisites",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PrereqName = c.String(),
                        BaseCourse_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BaseCourses", t => t.BaseCourse_ID)
                .Index(t => t.BaseCourse_ID);
            
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
                        Semester = c.String(),
                        Grade = c.Int(),
                        Selected = c.Boolean(),
                        BaseCourse_ID = c.Int(),
                        Student_ID = c.Int(),
                        Student_ID1 = c.Int(),
                        Student_ID2 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BaseCourses", t => t.BaseCourse_ID)
                .ForeignKey("dbo.Students", t => t.Student_ID)
                .ForeignKey("dbo.Students", t => t.Student_ID1)
                .ForeignKey("dbo.Students", t => t.Student_ID2)
                .Index(t => t.BaseCourse_ID)
                .Index(t => t.Student_ID)
                .Index(t => t.Student_ID1)
                .Index(t => t.Student_ID2);
            
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
                        StudentConcentration_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Concentrations", t => t.StudentConcentration_ID)
                .Index(t => t.StudentConcentration_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Student_ID2", "dbo.Students");
            DropForeignKey("dbo.Students", "StudentConcentration_ID", "dbo.Concentrations");
            DropForeignKey("dbo.Courses", "Student_ID1", "dbo.Students");
            DropForeignKey("dbo.Courses", "Student_ID", "dbo.Students");
            DropForeignKey("dbo.Courses", "BaseCourse_ID", "dbo.BaseCourses");
            DropForeignKey("dbo.BaseCourses", "Concentration_ID", "dbo.Concentrations");
            DropForeignKey("dbo.Prerequisites", "BaseCourse_ID", "dbo.BaseCourses");
            DropIndex("dbo.Students", new[] { "StudentConcentration_ID" });
            DropIndex("dbo.Courses", new[] { "Student_ID2" });
            DropIndex("dbo.Courses", new[] { "Student_ID1" });
            DropIndex("dbo.Courses", new[] { "Student_ID" });
            DropIndex("dbo.Courses", new[] { "BaseCourse_ID" });
            DropIndex("dbo.Prerequisites", new[] { "BaseCourse_ID" });
            DropIndex("dbo.BaseCourses", new[] { "Concentration_ID" });
            DropTable("dbo.Students");
            DropTable("dbo.Courses");
            DropTable("dbo.Concentrations");
            DropTable("dbo.Prerequisites");
            DropTable("dbo.BaseCourses");
        }
    }
}
