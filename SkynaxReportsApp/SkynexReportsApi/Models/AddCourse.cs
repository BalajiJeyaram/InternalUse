using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkynexReportsApi.Models
{
    public class AddCourse
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
    }
}