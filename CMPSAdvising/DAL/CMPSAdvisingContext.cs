
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CMPSAdvising.Models;

namespace CMPSAdvising.DAL
{
    public class CMPSAdvisingContext : DbContext
    {
        public CMPSAdvisingContext() : base("CMPSAdvisingContext")
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<BaseCourse> BaseCourses { get; set; }
        public DbSet<Concentration> Concentrations { get; set; }
        public DbSet<Prerequisite> Prerequisites { get; set; }
    }
}