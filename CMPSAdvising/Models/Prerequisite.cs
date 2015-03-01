using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPSAdvising.Models
{
    public class Prerequisite
    {
        public int ID { get; set; }
        public string PrereqName { get; set; }
        public BaseCourse BaseCourse { get; set; }
    }
}