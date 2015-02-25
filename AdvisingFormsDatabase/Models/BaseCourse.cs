using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdvisingFormsDatabase.Models
{
    public class BaseCourse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public int CourseNumber { get; set; }
        public int CreditHours { get; set; }

        public int? ParentItemId { get; set; }

        public virtual ICollection<string> Prerequisites { get; set; }

        public BaseCourse()
        {
            Prerequisites = new List<string>();
        }
    }
}
