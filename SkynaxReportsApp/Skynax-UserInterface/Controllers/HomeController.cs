using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skynax_UserInterface.Models;

namespace Skynax_UserInterface.Controllers
{
    
    public class HomeController : Controller
    {
        private List<QuestionAnswer> QAT = new List<QuestionAnswer>();
        public HomeController()
        {
            QAT.Add(new QuestionAnswer() { Answer="Sample",AnswerType=1,Question="What's your name"});
            QAT.Add(new QuestionAnswer() { Answer = "1234", AnswerType = 1, Question = "What's your mother name" });
            QAT.Add(new QuestionAnswer() { Answer = "Sample", AnswerType = 1, Question = "What's your name" });
            QAT.Add(new QuestionAnswer() { Answer = "1234", AnswerType = 1, Question = "What's your mother name" });
            QAT.Add(new QuestionAnswer() { Answer = "Sample", AnswerType = 1, Question = "What's your name" });
            QAT.Add(new QuestionAnswer() { Answer = "1234", AnswerType = 1, Question = "What's your mother name" });
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Assessment()
        {
            ViewBag.Message = "Assessment Page";

            return View(QAT);
        }
        public JsonResult GetQuestions()
        {
            ViewBag.Message = "GetQuestions Method";

            return Json(QAT, JsonRequestBehavior.AllowGet);
        }
    }
}