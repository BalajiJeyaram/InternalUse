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
        private AssessmentContext dbcontext = new AssessmentContext();
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

        public ActionResult SubmitKCSAssessment(string AInfo1, string AInfo2, string AInfo3, string AInfo4, string AInfo5, string AInfo6,
            int LInfo1, string CInfo1, string CInfo2, string CInfo3, string CInfo4, string CInfo5, string CInfo6,
            int AQIInfo1, int AQIInfo2, int AQIInfo3, int AQIInfo4, int AQIInfo5, int AQIInfo6, int AQIInfo7, int AQIInfo8, int AQIInfo9,
            string CoachComments)
        {
            try
            {
                if (Session["InvalidUser"].ToString() == "ValidUser")
                {
                    KCSAssessment kCSAssessment = new KCSAssessment()
                    {
                        //iD = 1,
                        AInfo1 = Convert.ToDateTime(AInfo1),
                        AInfo2 = AInfo2,
                        AInfo3 = AInfo3,
                        AInfo4 = AInfo4,
                        AInfo5 = AInfo5,
                        AInfo6 = AInfo6,
                        LInfo1 = LInfo1,
                        CInfo1 = CInfo1,
                        CInfo2 = CInfo2,
                        CInfo3 = CInfo3,
                        CInfo4 = CInfo4,
                        CInfo5 = CInfo5,
                        CInfo6 = CInfo6,
                        AQIInfo1 = AQIInfo1,
                        AQIInfo2 = AQIInfo2,
                        AQIInfo3 = AQIInfo3,
                        AQIInfo4 = AQIInfo4,
                        AQIInfo5 = AQIInfo5,
                        AQIInfo6 = AQIInfo6,
                        AQIInfo7 = AQIInfo7,
                        AQIInfo8 = AQIInfo8,
                        AQIInfo9 = AQIInfo9,
                        CoachComments = CoachComments
                    };

                    bool result = KCSAssessmentValidation.CreateKCSAssessment(kCSAssessment);

                    return View("AssessmentList", dbcontext.kcsassessment.ToList());
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

        public ActionResult AssessmentList(List<KCSAssessment> kcsassessmentlist)
        {
            return View("AssessmentList", kcsassessmentlist == null ? dbcontext.kcsassessment.ToList() : kcsassessmentlist);
        }

        [HttpGet]
        public ActionResult EditPage(int iD)
        {
            try
            {
                if (Session["InvalidUser"].ToString() == "ValidUser")
                {
                    return View("EditAssessment", dbcontext.kcsassessment.Where(x => x.iD == iD).FirstOrDefault());
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

        [HttpPost]
        public ActionResult EditAssessment(KCSAssessment kcsAssessment)
        {
            return View();
        }
    }
}