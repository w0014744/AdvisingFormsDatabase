using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisingFormsDatabase.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WNumber { get; set; }
        public int HoursCompleted { get; set; }
        public double GPA {get; set;}
        public int ConcentrationID { get; set; }

        public Concentration StudentConcentration { get; set; }

        public virtual ICollection<Course> CoursesTaken { get; set; }
        public virtual ICollection<Course> CoursesRecommended { get; set; }

        public Student()
        {
            StudentConcentration = new Concentration();
            CoursesTaken = new List<Course>();
            CoursesRecommended = new List<Course>();
        }
    }
}