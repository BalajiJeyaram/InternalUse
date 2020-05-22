using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HONKCSUI.Models
{
    public class ResultModel
    {
        public int answered { get; set; }
        public int notanswered { get; set; }
        public int passcore { get; set; }
        public int score { get; set; }
        public string examenddate { get; set; }
        public string examendtime { get; set; }
        public string examstartdate { get; set; }
        public string examstartendtime { get; set; }
    }
}