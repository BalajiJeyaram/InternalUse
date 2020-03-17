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
        private CheckUser checklogonuser = new CheckUser();
        public HomeController()
        {
            QAT.Add(new QuestionAnswer() { Answer="It is Honeywell Device#It is Operating System#It is Toothpaste",AnswerType=1,Question="What is EDA51"});
            QAT.Add(new QuestionAnswer() { Answer = "It is a Honeywell Application#It is an Apple Product#It is an Amazon app", AnswerType = 1, Question = "What is Honeywell Provisioner" });
            QAT.Add(new QuestionAnswer() { Answer = "Description...", AnswerType = 2, Question = "Write 2-3 lines about SMART TE" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Honeywell Device#It is Operating System#It is Toothpaste", AnswerType = 1, Question = "What is Dolphin" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Enterprise Application#It is a website where an enduser can buy product#It is thirdparty Website", AnswerType = 1, Question = "What is SalesForce" });
            QAT.Add(new QuestionAnswer() { Answer = "Description...", AnswerType = 2, Question = "Write 2-3 lines about CN60" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Honeywell Device#It is Operating System#It is Toothpaste", AnswerType = 1, Question = "What is EDA51" });
            QAT.Add(new QuestionAnswer() { Answer = "It is a Honeywell Application#It is an Apple Product#It is an Amazon app", AnswerType = 1, Question = "What is Honeywell Provisioner" });
            QAT.Add(new QuestionAnswer() { Answer = "Description...", AnswerType = 2, Question = "Write 2-3 lines about SMART TE" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Honeywell Device#It is Operating System#It is Toothpaste", AnswerType = 1, Question = "What is Dolphin" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Enterprise Application#It is a website where an enduser can buy product#It is thirdparty Website", AnswerType = 1, Question = "What is SalesForce" });
            QAT.Add(new QuestionAnswer() { Answer = "Description...", AnswerType = 2, Question = "Write 2-3 lines about CN60" });
            checklogonuser = new CheckUser() { LoginMessage = "Welcome Admin", LoginSuccess = true, password = "admin", userName = "admin" };
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
        public JsonResult CheckUser(CheckUser userparam)
        {
            var returnobject = userparam;
            if (userparam.userName == "admin")
            {
                returnobject = checklogonuser;
            }
            else {
                returnobject = new CheckUser() { userName = returnobject.userName, password= returnobject.password, LoginMessage="Invalid User/Password",LoginSuccess=false};
            }
            return Json(new { returnobject },JsonRequestBehavior.AllowGet); 
        }
    }
}