using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HONKCSUI.Models
{
    public class QuestionAnswer
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int AnswerType { get; set; }
        public int AnsweredStatus { get; set; } = 0;
    }

    public class CheckUser
    {
        public string userName { get; set; }
        public string password { get; set; }
        public bool LoginSuccess { get; set; }
        public string LoginMessage { get; set; }
    }
}