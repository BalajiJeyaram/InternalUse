﻿using System;
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
    }
}
