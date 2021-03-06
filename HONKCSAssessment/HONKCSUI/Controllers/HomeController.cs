﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HONKCSUI.Models;
using KCSEntities.DAL;
namespace HONKCSUI.Controllers
{

    public class HomeController : Controller
    {
        private AssessmentContext dbcontext = new AssessmentContext();
        private List<QuestionAnswer> QAT = new List<QuestionAnswer>();
        private CheckUser checklogonuser = new CheckUser();
        public HomeController()
        {

            //using (var context = new AssessmentContext())
            //{
            //    context.Database.Connection.Open();
            //    var user = new testtable()
            //    {
            //        UserName = "balajikj1",
            //        Password = "password",
            //        ActiveUser = "Y",
            //        CreatedDate = DateTime.Now,
            //        LastModifiedDate = DateTime.Now
            //    };
            //    context.testtable.Add(user);
            //    context.SaveChanges();
            //}

            QAT.Add(new QuestionAnswer() { Answer = "It is Honeywell Device#It is Operating System#It is Toothpaste", AnswerType = 1, Question = "Practical. Configure EDA60 so that when reading a QR code it takes the first group of characters until it finds a comma, other characters are then discarded. This should work just for QR codes." });
            QAT.Add(new QuestionAnswer() { Answer = "It is a Honeywell Application#It is an Apple Product#It is an Amazon app", AnswerType = 1, Question = "Can you use cellular data when there is no Wi-Fi on a CN50 (32 GB, Numeric, WWAN)" });
            QAT.Add(new QuestionAnswer() { Answer = "Description...", AnswerType = 2, Question = "What would be the biggest restriction when creating a bar code on Enterprise Provisioner to download and then install an application into a device?" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Honeywell Device#It is Operating System#It is Toothpaste", AnswerType = 1, Question = "Name 3 tasks you can perform with bar codes generated using Enterprise Provisione" });
            QAT.Add(new QuestionAnswer() { Answer = "It is Enterprise Application#It is a website where an enduser can buy product#It is thirdparty Website", AnswerType = 1, Question = "Normally licenses when generated are exported into two different type of files, which are these?" });
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogIn()
        {
            Session["InvalidUser"] = "Logged Out";
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
                return RedirectToAction("Index", "Error", new { message = nullexp.StackTrace });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { message = ex.StackTrace });
            }


        }

        public ActionResult LabTest()
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
            if (Session["InvalidUser"].ToString() == "ValidUser")
            {
                return View(QAT);
            }
            else
            {
                Session["InvalidUser"] = "You did not login yet!";
                return RedirectToAction("LogIn", "Home");
            }

        }
        public ActionResult SubmitContactUs(string Firstname, string Lastname, string Email, string Subject)
        {
            try
            {
                if (Session["InvalidUser"].ToString() == "ValidUser")
                {
                    
                    Contact cont = new Contact() { Firstname = Firstname, Lastname = Lastname, Email = Email, Subject = Subject };
                    dbcontext.contactus.Add(cont);
                    int i = dbcontext.SaveChanges();
                    ViewBag.Message = i >= 1 ? "We've received it! Someone from Honeywell Tech Support team will be in touch base with you." : "zero record saved";
                    return View("ContactUs");
                }
                else
                {
                    Session["InvalidUser"] = "You did not login yet!";
                    return RedirectToAction("LogIn", "Home");
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
            else
            {
                returnobject = new CheckUser() { userName = returnobject.userName, password = returnobject.password, LoginMessage = "Invalid User/Password", LoginSuccess = false };
            }
            return Json(new { returnobject }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidateUser(string login, string password)
        {
            //if (UserValidation.ValidateCredential(login, password))
            if (UserValidation.ValidateCredential(login, password) == true)
            {
                Session["InvalidUser"] = "ValidUser";
                Session["LoginedUser"] = login;
                //return View("Index");
                return RedirectToAction("Index", "Home");
            }

            else
            {
                Session["InvalidUser"] = "Invalid User";
                Session["LoginedUser"] = "";
                return View("LogIn");
            }

        }

        

        public ActionResult AssessmentResult()
        {
            ViewBag.Message = "Assessment Result Page";
            return View();
        }

        public ActionResult SubmitAssessment(List<QuestionAnswer> questionAnswer)
        {

            return View("AssessmentResult", new ResultModel()
            {
                answered = 20,
                notanswered = 5,
                passcore = 100,
                score = 90,
                examstartdate = DateTime.Today.ToShortDateString(),
                examstartendtime = DateTime.Today.ToShortTimeString(),
                examenddate = DateTime.Today.ToShortDateString(),
                examendtime = DateTime.Today.ToShortTimeString()
            });
            //return Json(new ResultModel() {answered=20,notanswered=5,passcore=100,
            //    score =90,examstartdate=DateTime.Today.ToShortDateString(),
            //    examstartendtime =DateTime.Today.ToShortTimeString(),
            //    examenddate =DateTime.Today.ToShortDateString(),
            //    examendtime =DateTime.Today.ToShortTimeString() },JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> SendEmail()
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(""));  // replace with valid value 
                message.From = new MailAddress("");  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, "Balaji KJ", "", "This is test email sent from ASP.Net MVC application");
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "",  // replace with valid value
                        Password = ""  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //accessmentdb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}