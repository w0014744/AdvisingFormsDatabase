using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPSAdvising.Models
{
    public class BaseCourse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public int CourseNumber { get; set; }
        public int CreditHours { get; set; }

        public virtual ICollection<Prerequisite> PreReqs { get; set; }
    }
}