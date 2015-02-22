using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisingFormsDatabase.Models
{
    public enum Grade
    {
        A, B, C, D, F, W
    }

    public class Course
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public int BaseCourseID { get; set; }
        public string Semester { get; set; }
        public Grade? Grade { get; set; }

        public BaseCourse BaseCourse { get; set; }
        public Student Student { get; set; }
    }
}