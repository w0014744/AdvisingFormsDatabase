using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPSAdvising.Models
{
    public class Concentration
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int HoursRequired { get; set; }

        public virtual ICollection<BaseCourse> RequiredCourses { get; set; }
    }
}