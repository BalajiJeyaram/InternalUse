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


        public async Task<JsonResult> SendEmail() 
        {
            //EWRTemplate_Model ewrmodel = new EWRTemplate_Model();
            //string outresult = string.Empty;
            //bool blnresult = SendEmail(ewrmodel, out outresult);
            //return Json(blnresult, JsonRequestBehavior.AllowGet);
            string result = string.Empty;
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
                        UserName = "h387014",  // replace with valid value
                        Password = "April2020&#"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    //return RedirectToAction("Sent");

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

            returvalue = returvalue.Replace("{TSEName}", "ewr.AInfo3");
            returvalue = returvalue.Replace("{Coach}", "ewr.AInfo4");
            returvalue = returvalue.Replace("{Case}", "ewr.AInfo5");
            returvalue = returvalue.Replace("{CaseTitle}", "ewr.AInfo8");
            returvalue = returvalue.Replace("{Article}", "ewr.AInfo6");
            returvalue = returvalue.Replace("{CIIndex}", "ewr.CIIndex");
            returvalue = returvalue.Replace("{AQIIndex}", "ewr.AQIIndex.ToString()");
            returvalue = returvalue.Replace("{Comments}", "ewr.CoachComments");

            return returvalue;
        }

        #endregion
    }
}