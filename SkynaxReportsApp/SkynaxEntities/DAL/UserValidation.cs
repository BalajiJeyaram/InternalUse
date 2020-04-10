using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkynaxEntities.DAL
{
    public static class UserValidation
    {

        public static bool ValidateCredential(string uname, string upass)
        {
            if (string.IsNullOrWhiteSpace(uname) || string.IsNullOrWhiteSpace(upass))
                return false;

            using (var context = new AssessmentContext())
            {
                context.Database.Connection.Open();
                var userexists = context.userprofiles.Select(x => x.UserName == uname && x.Password == upass).FirstOrDefault();
                return userexists;
            }
        }
    }
}
