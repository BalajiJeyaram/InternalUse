using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EWRTemplate.Models;

namespace EWRTemplate.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
            EWRTemplate_Model ewrtemplate = new EWRTemplate_Model();
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

        public ActionResult EWRTemplate()
        {
            ViewBag.Message = "Your EWR Template.";
            
            return View();
        }
        [HttpPost]
        public ActionResult ViewEWRTemplate(string inputusecase, string inputproblemstatement, string inputdesiredbehaviour,string inputcurrentbehaviour,
           string inputcaseno, string drpinputarealldevicesaffected, string inputwhatpercentage, string drpinputareallallsiteaffected, string inputcompetitivefunctioning,
            string drpinputwasitworking, string inputwhatrecentlychanged, string inputdeviceInfo, string inputspecificreq, string drpinputSAT3, string drpinputnologs, 
            string drpinputyeslogs, string inputbriefexplain, string inputhwshipment, string inputtools, string inputenv, string inputreproduction)
        {
            ViewBag.Message = inputcaseno;

            EWRTemplate_Model ewr = new EWRTemplate_Model() { usecase = inputusecase, problemstatement = inputproblemstatement, desiredbehavior = inputdesiredbehaviour, currentbehavior = inputcurrentbehaviour,
            casenumber = inputcaseno, arealldeviceaffected = drpinputareallallsiteaffected, devicesaffectedpercentage= inputwhatpercentage, areallsiteaffected = drpinputareallallsiteaffected, competitivedevicefunctioning = inputcompetitivefunctioning,
            wasitworkingpreviosuly = drpinputwasitworking, wasitrecentlychanged = inputwhatrecentlychanged, DeviceInformation = inputdeviceInfo,
            specificrequestofengineering = inputspecificreq, reproducewithinhoneywell = drpinputSAT3, logscolletedfromcustomer = drpinputnologs,
            didlogshowanyissue = drpinputyeslogs, hardwareshipment = inputhwshipment, accesstocustomertool = inputtools, environmentdetails = inputenv, reproductionsteps = inputreproduction};
            return View(ewr);
        }
    }
}