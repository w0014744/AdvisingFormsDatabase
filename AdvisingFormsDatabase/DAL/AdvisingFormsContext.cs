using AdvisingFormsDatabase.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AdvisingFormsDatabase.DAL
{
    public class AdvisingFormsContext : DbContext
    {

        public AdvisingFormsContext() : base("AdvisingFormsContext")
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<BaseCourse> BaseCourses { get; set; }
        public DbSet<Concentration> Concentrations { get; set; }
        public DbSet<Prerequisites> Prerequisites { get; set; }

    }
}