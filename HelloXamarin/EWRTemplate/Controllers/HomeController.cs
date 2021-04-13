﻿using System;
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
            string drpinputwasitworking, string inputwhatrecentlychanged, string inputdeviceInfo, string inputspecificreq, string inputSAT3, string inputnologs, 
            string inputyeslogs, string inputbriefexplain, string inputhwshipment, string inputtools, string inputenv, string inputreproduction)
        {
            ViewBag.Message = inputcaseno;

            EWRTemplate_Model ewr = new EWRTemplate_Model() { usecase = inputusecase };
            return View(ewr);
        }
    }
}