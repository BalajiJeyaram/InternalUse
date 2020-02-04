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
            QAT.Add(new QuestionAnswer() { Answer="It is Honeywell Device#It is Operating System#It is Toothpaste",AnswerType=1,Question="What is EDA51"});
            QAT.Add(new QuestionAnswer() { Answer = "It is a Honeywell Application#It is an Apple Product#It is an Amazon app", AnswerType = 1, Question = "What is Honeywell Provisioner" });
            QAT.Add(new QuestionAnswer() { Answer = "Description...", AnswerType = 2, Question = "Write 2-3 lines about SMART TE" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Honeywell Device#It is Operating System#It is Toothpaste", AnswerType = 1, Question = "What is Dolphin" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Enterprise Application#It is a website where an enduser can buy product#It is thirdparty Website", AnswerType = 1, Question = "What is SalesForce" });
            QAT.Add(new QuestionAnswer() { Answer = "Description...", AnswerType = 2, Question = "Write 2-3 lines about CN60" });
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