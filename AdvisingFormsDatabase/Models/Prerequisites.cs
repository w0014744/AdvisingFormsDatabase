using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvisingFormsDatabase.Models
{
    public class Prerequisites
    {
        public int ID { get; set; }
        public string  prerequisiteCourse { get; set; }
        public BaseCourse BaseCourse { get; set; }
    }
}
