using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KCSEntities.DAL
{
    public static class KCSAssessmentValidation
    {
        public static bool CreateKCSAssessment(KCSAssessment kcsAssessment)
        {
            using (var context = new AssessmentContext())
            {
                context.Database.Connection.Open();
                kcsAssessment.CIIndex = CIDevision(kcsAssessment);
                kcsAssessment.AQIIndex = AQIPoints(kcsAssessment);
                kcsAssessment.CreatedDate = DateTime.Now;
                kcsAssessment.LastModifiedDate = DateTime.Now;
                kcsAssessment.CreatedBy = UserValidation.FindUserId(HttpContext.Current.Session["LoginedUser"].ToString());
                kcsAssessment.LastModifiedBy = UserValidation.FindUserId(HttpContext.Current.Session["LoginedUser"].ToString());
                context.kcsassessment.Add(kcsAssessment);
                context.SaveChanges();

                var userexists = context.kcsassessment.Select(x => x.AInfo5 == kcsAssessment.AInfo5  && x.AInfo6 == kcsAssessment.AInfo6).FirstOrDefault();
                return userexists;
            }

        }
        public static string CIDevision(KCSAssessment kcsa)
        {
            bool returnvalue = false;
            bool firstsetcheck = false;
            bool secondsetcheck = false;
            bool thirdsetcheck = false;
         
            firstsetcheck = ((kcsa.CInfo1 == "Yes" && kcsa.CInfo2 == "Yes") || (kcsa.CInfo1=="Yes" && kcsa.CInfo2 =="N/A") || (kcsa.CInfo1 =="No" && kcsa.CInfo2 =="N/A")) ? true : false;
            secondsetcheck = ((kcsa.CInfo3 == "Yes" && kcsa.CInfo4 == "Yes") || (kcsa.CInfo3 == "Yes" && kcsa.CInfo4 == "N/A") || (kcsa.CInfo3 == "No" && kcsa.CInfo4 == "N/A")) ? true : false;
            thirdsetcheck = ((kcsa.CInfo5 == "Yes" && kcsa.CInfo6 == "Yes") || (kcsa.CInfo5 == "Yes" && kcsa.CInfo6 == "N/A") || (kcsa.CInfo5 == "No" && kcsa.CInfo6 == "N/A")) ? true : false;

            returnvalue = (firstsetcheck == secondsetcheck == thirdsetcheck) ? true : false;

            return (returnvalue==true? "KCS Correctly Applied" : "KCS Incorrectly Applied");
        }

        public static int AQIPoints(KCSAssessment kcsa)
        {
            return (kcsa.AQIInfo1 + kcsa.AQIInfo2 + kcsa.AQIInfo3 + kcsa.AQIInfo4 +
                kcsa.AQIInfo5 + kcsa.AQIInfo6 + kcsa.AQIInfo7 + kcsa.AQIInfo8 + kcsa.AQIInfo9);
        }
    }
}
