using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Skynax_UserInterface
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void session_start(object sender, EventArgs e)
        {
            Session.Timeout = 20;
            //Application.Lock();
            //Application["OnlineCount"] = Convert.ToInt32(Application["Onlinecount"]) + 1;
            Session["InvalidUser"] = "InvalidUser";
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception != null)
            {
                Response.Redirect(string.Format("~/Error/Index/?message={0}",exception.Message));
            }
        }
    }
}
