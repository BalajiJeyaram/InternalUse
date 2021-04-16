using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EWRTemplate.Models;

namespace EWRTemplate.Controllers
{
    public class HomeController : Controller
    {
       // EWRTemplate_Model ewrtemplate = new EWRTemplate_Model();
        public HomeController()
        {

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


        public JsonResult  SendEmail(EWRTemplate_Model model) 
        {
            //EWRTemplate_Model ewrmodel = new EWRTemplate_Model();
            //string outresult = string.Empty;
            //bool blnresult = SendEmail(ewrmodel, out outresult);
            //return Json(blnresult, JsonRequestBehavior.AllowGet);
            string result = string.Empty;
            if (ModelState.IsValid)
            {
                //var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("balaji.kj@honeywell.com"));  // replace with valid value 
                message.From = new MailAddress("balaji.kj@honeywell.com");  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = CreateBody(model) ;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "h387014",  // replace with valid value
                        Password = "April2020&#"  // replace with valid value
                    };
                    //smtp.Credentials = credential;
                    smtp.Host = "smtp.honeywell.com";
                    smtp.Port = 25;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    string eusername = "h387014";//GlobalVariables.EmailUsername;// ReturnConfigValue("EmailUserName");
                    string epassword = "April2020&$";// GlobalVariables.EmailPassword;// ReturnConfigValue("EmailPassword");

                    smtp.Credentials = new NetworkCredential(eusername, epassword);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    //exception = "Success";

                    result = "Email successfully sent to: balaji.kj@honeywell.com";

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        #region sendemail
        public static bool SendEmail(EWRTemplate_Model ewr, out string exception)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.IsBodyHtml = true;
                string htmlbody = string.Empty;
                htmlbody += "<h1> </h1>";

                string fromEmailaddress = "balaji.kj@honeywell.com";// ReturnConfigValue("FromEmailAddress");
                fromEmailaddress = (fromEmailaddress == string.Empty ? "balaji.kj@honeywell.com" : fromEmailaddress);

                //if (fromEmailaddress.Substring(fromEmailaddress.Length - 1, 1) == ";")
                //    fromEmailaddress = fromEmailaddress.Remove(fromEmailaddress.Length, 1);

                //if (fromEmailaddress != null || fromEmailaddress != string.Empty)
                //    if (IsValidEmail(fromEmailaddress) == false)
                //    {
                //        exception = "Invalid From Email Address!";
                //        return false;
                //    }

                message.From = new MailAddress(fromEmailaddress);
                MailAddress maila = new MailAddress("balaji.kj@honeywell.com");
                message.To.Add(maila);

                //foreach (var Ccaddress in ConfigurationManager.AppSettings["CCEmailAddress"].ToString().Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                //{
                //    if (IsValidEmail(Ccaddress) == false)
                //    {
                //        exception = "Invalid CC Email Address!";
                //        return false;
                //    }
                //    message.CC.Add(Ccaddress);
                //}

                //foreach (var Bccaddress in ConfigurationManager.AppSettings["BCCEmailAddress"].ToString().Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                //{
                //    if (IsValidEmail(Bccaddress) == false)
                //    {
                //        exception = "Invalid BCC Email Address!";
                //        return false;
                //    }
                //    message.Bcc.Add(Bccaddress);
                //}

                string subject = "ReturnConfigValue('EmailSubject')";
                message.Subject = (subject == string.Empty ? "KCS Assessment" : subject);
                message.Body = "CreateBody(kcs)";

                smtp.Port = 25;
                smtp.Host = "smtp.honeywell.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                string eusername = "h387014";//GlobalVariables.EmailUsername;// ReturnConfigValue("EmailUserName");
                string epassword = "April2020&$";// GlobalVariables.EmailPassword;// ReturnConfigValue("EmailPassword");
                if (eusername == string.Empty)
                {
                    exception = "Configure EMail UserName!";
                    return false;
                }
                if (epassword == string.Empty)
                {
                    exception = "Configure EMail Password!";
                    return false;
                }

                smtp.Credentials = new NetworkCredential(eusername, epassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                exception = "Success";
                return true;
            }
            catch (Exception ex)
            {
                exception = ex.Message.ToString() + " - " + ex.TargetSite.Module + " - " + ex.TargetSite.Name;
                return false;
            }
        }

        public static string CreateBody(EWRTemplate_Model ewr)
        {
            string returvalue = string.Empty;

            string filename = AppDomain.CurrentDomain.BaseDirectory + @"\EmailTemplate.html";

            using (StreamReader reader = new StreamReader(filename))
                returvalue = reader.ReadToEnd();

            returvalue = returvalue.Replace("{Usecase}", ewr.usecase);
            returvalue = returvalue.Replace("{problemstatement}", ewr.problemstatement);
            returvalue = returvalue.Replace("{desiredbehaviour}", ewr.desiredbehavior);
            returvalue = returvalue.Replace("{currentbehaviour}", ewr.currentbehavior);
            returvalue = returvalue.Replace("{casenumber}", ewr.casenumber);
            returvalue = returvalue.Replace("{devicesaffected}", ewr.arealldeviceaffected);
            returvalue = returvalue.Replace("{whatpercentage}", ewr.devicesaffectedpercentage);
            returvalue = returvalue.Replace("{sitesaffected}", ewr.areallsiteaffected);
            returvalue = returvalue.Replace("{competitivedevice}", ewr.competitivedevicefunctioning);
            returvalue = returvalue.Replace("{workingpreviously}", ewr.wasitworkingpreviosuly);
            returvalue = returvalue.Replace("{recentlychanged}", ewr.wasitrecentlychanged);
            returvalue = returvalue.Replace("{deviceinformation}", ewr.DeviceInformation);
            returvalue = returvalue.Replace("{specificrequest}", ewr.specificrequestofengineering);
            returvalue = returvalue.Replace("{SAreproduce}", ewr.reproducewithinhoneywell);
            returvalue = returvalue.Replace("{logscollected}", ewr.logscolletedfromcustomer);
            returvalue = returvalue.Replace("{logshow}", ewr.didlogshowanyissue);
            returvalue = returvalue.Replace("{hardwareshipment}", ewr.hardwareshipment);
            returvalue = returvalue.Replace("{customertools}", ewr.accesstocustomertool);
            returvalue = returvalue.Replace("{environment}", ewr.environmentdetails);
            returvalue = returvalue.Replace("{reproductionsteps}", ewr.reproductionsteps);


            return returvalue;
        }

        #endregion
    }
}