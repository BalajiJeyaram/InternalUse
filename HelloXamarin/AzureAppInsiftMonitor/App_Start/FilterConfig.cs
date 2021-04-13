using System.Web;
using System.Web.Mvc;

namespace AzureAppInsiftMonitor
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new ErrorHandler.AiHandleErrorAttribute());
        }
    }
}
