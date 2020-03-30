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
            QAT.Add(new QuestionAnswer() { Answer="It is Honeywell Device#It is Operating System#It is Toothpaste",AnswerType=1,Question= "Practical. Configure EDA60 so that when reading a QR code it takes the first group of characters until it finds a comma, other characters are then discarded. This should work just for QR codes." });
            QAT.Add(new QuestionAnswer() { Answer = "It is a Honeywell Application#It is an Apple Product#It is an Amazon app", AnswerType = 1, Question = "Can you use cellular data when there is no Wi-Fi on a CN50 (32 GB, Numeric, WWAN)" });
            QAT.Add(new QuestionAnswer() { Answer = "Description...", AnswerType = 2, Question = "What would be the biggest restriction when creating a bar code on Enterprise Provisioner to download and then install an application into a device?" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Honeywell Device#It is Operating System#It is Toothpaste", AnswerType = 1, Question = "Name 3 tasks you can perform with bar codes generated using Enterprise Provisione" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Enterprise Application#It is a website where an enduser can buy product#It is thirdparty Website", AnswerType = 1, Question = "Normally licenses when generated are exported into two different type of files, which are these?" });
            QAT.Add(new QuestionAnswer() { Answer = "Description...", AnswerType = 2, Question = "Honeywell apps per platform (Windows/Android). Specify name of pack and apps on each pack" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Honeywell Device#It is Operating System#It is Toothpaste", AnswerType = 1, Question = "What is the most effective and recommended way to recover information in our device when AppLock credentials have been forgotten?" });
            QAT.Add(new QuestionAnswer() { Answer = "It is a Honeywell Application#It is an Apple Product#It is an Amazon app", AnswerType = 1, Question = "What are the two ways to enable OCR?" });
            QAT.Add(new QuestionAnswer() { Answer = "Description...", AnswerType = 2, Question = "What server instance is recommended to install alongside Staging Hub?" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Honeywell Device#It is Operating System#It is Toothpaste", AnswerType = 1, Question = "What is the wild card character usable in white listing URL to add subsequent URLs?" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Enterprise Application#It is a website where an enduser can buy product#It is thirdparty Website", AnswerType = 1, Question = "Encryption method used to encrypt Enterprise Browser and Launcher passwords?" });
            QAT.Add(new QuestionAnswer() { Answer = "Description...", AnswerType = 2, Question = "What type of reset must be performed if the Dubai terminal is downgraded from Android O to Android N?" });

            QAT.Add(new QuestionAnswer() { Answer = "It is Honeywell Device#It is Operating System#It is Toothpaste", AnswerType = 1, Question = "Applications built for Android P can use either HTTP or HTTPS for browsing?" });
            QAT.Add(new QuestionAnswer() { Answer = "It is a Honeywell Application#It is an Apple Product#It is an Amazon app", AnswerType = 1, Question = "Which two imagers will be available for the EDA71?" });
            QAT.Add(new QuestionAnswer() { Answer = "Description...", AnswerType = 2, Question = "EDA71 offers a WWAN radio option?" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Honeywell Device#It is Operating System#It is Toothpaste", AnswerType = 1, Question = "CK65 scan engines available are:" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Enterprise Application#It is a website where an enduser can buy product#It is thirdparty Website", AnswerType = 1, Question = "What would happen if you remove the battery of a CK65 while using Honeywell Enterprise Browser?" });
            QAT.Add(new QuestionAnswer() { Answer = "Description...", AnswerType = 2, Question = "What is PCAP feature on a touchscreen?" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Honeywell Device#It is Operating System#It is Toothpaste", AnswerType = 1, Question = "How many multi-touch points does PCAP support?" });
            QAT.Add(new QuestionAnswer() { Answer = "It is a Honeywell Application#It is an Apple Product#It is an Amazon app", AnswerType = 1, Question = "What is the maximum size for a SD Card in a VM1A?" });
            QAT.Add(new QuestionAnswer() { Answer = "Description...", AnswerType = 2, Question = "Name 3 new features on Android 8." });
            QAT.Add(new QuestionAnswer() { Answer = "It is Honeywell Device#It is Operating System#It is Toothpaste", AnswerType = 1, Question = "How does PIP (picture in picture) works?" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Enterprise Application#It is a website where an enduser can buy product#It is thirdparty Website", AnswerType = 1, Question = "What tool inside Power Tools will let us know the IP Address, MAC Address, RSS of our device/connection?" });
            QAT.Add(new QuestionAnswer() { Answer = "Description...", AnswerType = 2, Question = "What does the HUpgrader utility do?" });
            checklogonuser = new CheckUser() { LoginMessage = "Welcome Admin", LoginSuccess = true, password = "admin", userName = "admin" };
        }
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogIn()
        {
            Session["InvalidUser"]="Logged Out";
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
            try
            {
                //throw new NullReferenceException("Test");
                
                if (Session["InvalidUser"].ToString() == "ValidUser")
                {
                    ViewBag.Message = "Assessment Page";
                    return View(QAT);
                }
                else
                {
                    Session["InvalidUser"] = "You did not login yet!";
                    return View("LogIn");
                }
            }
            catch (NullReferenceException nullexp)
            {
                return RedirectToAction("Index", "Error",new { message=nullexp.StackTrace});
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { message = ex.StackTrace });
            }


        }
        public ActionResult PracticalTest()
        {
            //ViewBag.Message = "Pracical Test Page";

            //return View(QAT);
            try
            {
                if (Session["InvalidUser"].ToString() == "ValidUser")
                {
                    ViewBag.Message = "Pracical Test Page";
                    return View(QAT);
                }
                else
                {
                    Session["InvalidUser"] = "You did not login yet!";
                    return View("LogIn");
                }
            }
            catch (NullReferenceException nullexp)
            {
                return RedirectToAction("Index", "Error", new { message = nullexp.StackTrace });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { message = ex.StackTrace });
            }

        }
        public ActionResult Courses()
        {
            //ViewBag.Message = "Courses Page";

            //return View(QAT);
            try
            {
                if (Session["InvalidUser"].ToString() == "ValidUser")
                {
                    ViewBag.Message = "Courses Page";
                    return View(QAT);
                }
                else
                {
                    Session["InvalidUser"] = "You did not login yet!";
                    return View("LogIn");
                }
            }
            catch (NullReferenceException nullexp)
            {
                return RedirectToAction("Index", "Error", new { message = nullexp.StackTrace });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { message = ex.StackTrace });
            }

        }
        public ActionResult Help()
        {
            //ViewBag.Message = "Help Page";

            //return View(QAT);
            try
            {
                if (Session["InvalidUser"].ToString() == "ValidUser")
                {
                    ViewBag.Message = "Help Page";
                    return View(QAT);
                }
                else
                {
                    Session["InvalidUser"] = "You did not login yet!";
                    return View("LogIn");
                }
            }
            catch (NullReferenceException nullexp)
            {
                return RedirectToAction("Index", "Error", new { message = nullexp.StackTrace });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { message = ex.StackTrace });
            }
        }
        public ActionResult ContactUs()
        {
            ViewBag.Message = "Contact Us Page";

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

        public ActionResult ValidateUser(string login)
        {
            if (login == "admin") {
                @Session["InvalidUser"] = "ValidUser";
                return View("Index");
            }
                
            else
            {
                Session["InvalidUser"] = "Invalid User";
                return View("LogIn");
            }
                
        }
        public ActionResult AssessmentResult()
        {
            ViewBag.Message = "Assessment Result Page";
            return View();
        }

        public JsonResult GetResult()
        {
            return Json(new ResultModel() {answered=20,notanswered=5,passcore=100,
                score =90,examstartdate=DateTime.Today.ToShortDateString(),
                examstartendtime =DateTime.Today.ToShortTimeString(),
                examenddate =DateTime.Today.ToShortDateString(),
                examendtime =DateTime.Today.ToShortTimeString() },JsonRequestBehavior.AllowGet);
        }
    }
}