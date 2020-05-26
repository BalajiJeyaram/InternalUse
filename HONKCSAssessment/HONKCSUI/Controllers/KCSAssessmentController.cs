using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
    }
}