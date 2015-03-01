namespace CMPSAdvising.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CMPSAdvising11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "BaseCourse_ID", "dbo.BaseCourses");
            DropForeignKey("dbo.Courses", "Student_ID2", "dbo.Students");
            DropIndex("dbo.Courses", new[] { "BaseCourse_ID" });
            DropIndex("dbo.Courses", new[] { "Student_ID2" });
            RenameColumn(table: "dbo.Courses", name: "BaseCourse_ID", newName: "BaseCourseID");
            RenameColumn(table: "dbo.Courses", name: "Student_ID2", newName: "StudentID");
            AlterColumn("dbo.Courses", "BaseCourseID", c => c.Int(nullable: false));
            AlterColumn("dbo.Courses", "StudentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "BaseCourseID");
            CreateIndex("dbo.Courses", "StudentID");
            AddForeignKey("dbo.Courses", "BaseCourseID", "dbo.BaseCourses", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Courses", "StudentID", "dbo.Students", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Courses", "BaseCourseID", "dbo.BaseCourses");
            DropIndex("dbo.Courses", new[] { "StudentID" });
            DropIndex("dbo.Courses", new[] { "BaseCourseID" });
            AlterColumn("dbo.Courses", "StudentID", c => c.Int());
            AlterColumn("dbo.Courses", "BaseCourseID", c => c.Int());
            RenameColumn(table: "dbo.Courses", name: "StudentID", newName: "Student_ID2");
            RenameColumn(table: "dbo.Courses", name: "BaseCourseID", newName: "BaseCourse_ID");
            CreateIndex("dbo.Courses", "Student_ID2");
            CreateIndex("dbo.Courses", "BaseCourse_ID");
            AddForeignKey("dbo.Courses", "Student_ID2", "dbo.Students", "ID");
            AddForeignKey("dbo.Courses", "BaseCourse_ID", "dbo.BaseCourses", "ID");
        }
    }
}
