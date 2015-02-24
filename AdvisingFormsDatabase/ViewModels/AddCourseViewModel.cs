using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdvisingFormsDatabase.Models;

namespace AdvisingFormsDatabase.ViewModels
{
    public class AddCourseViewModel
    {
        public Student Student { get; set; }
        public List<BaseCourse> AvailCourses { get; set; }

        public AddCourseViewModel (Student s, List<BaseCourse> c)
        {
            Student = s;
            AvailCourses = c;
        }
    }
}