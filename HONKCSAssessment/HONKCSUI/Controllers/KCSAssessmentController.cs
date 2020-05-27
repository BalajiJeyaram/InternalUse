using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KCSEntities.DAL;
namespace HONKCSUI.Controllers
{
    public class KCSAssessmentController : Controller
    {
        // GET: KCSAssessment
        public ActionResult Index()
        {
            //return View();
            try
            {
                if (Session["InvalidUser"].ToString() == "ValidUser")
                {
                    ViewBag.Message = "Courses Page";
                    return View();
                }
                else
                {
                    Session["InvalidUser"] = "You did not login yet!";
                    //return View("LogIn");
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

        public ActionResult Report()
        {
            try
            {
                if (Session["InvalidUser"].ToString() == "ValidUser")
                {
                    ViewBag.Message = "Courses Page";
                    return View();
                }
                else
                {
                    Session["InvalidUser"] = "You did not login yet!";
                    //return View("LogIn");
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

            //return View();
        }

        public ActionResult SubmitKCSAssessment(string AInfo1, string AInfo2, string AInfo3, string AInfo4, string AInfo5, string AInfo6)
        {
            KCSAssessment kCSAssessment = new KCSAssessment()
            {
                //iD = 1,
                AInfo1 = Convert.ToDateTime(AInfo1),
                AInfo2 = AInfo2,
                AInfo3 = AInfo3,
                AInfo4 = AInfo4,
                AInfo5 = AInfo5,
                AInfo6 = AInfo6

                //useriD = UserValidation.FindUserId(user_name)

            };

            bool result = KCSAssessmentValidation.CreateKCSAssessment(kCSAssessment);
            return RedirectToAction("SubmitAssessment", "Home");
            //return View("Create");
            //return View();
        }
    }
}