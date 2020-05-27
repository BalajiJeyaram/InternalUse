using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCSEntities.DAL
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

        public static bool CreateStudent(studentprofile stuprofile)
        {
            using (var context = new AssessmentContext())
            {
                context.Database.Connection.Open();
                stuprofile.CreatedDate = DateTime.Now;
                stuprofile.LastModifiedDate = DateTime.Now;
                context.studentprofile.Add(stuprofile);
                context.SaveChanges();

                var userexists = context.studentprofile.Select(x => x.FirstName == stuprofile.FirstName && x.LastName == stuprofile.LastName && x.DoB== stuprofile.DoB).FirstOrDefault();
                return userexists;
            }

        }
        public static bool ModifyStudent(studentprofile stuprofile)
        {

            using (var context = new AssessmentContext())
            {
                try
                {
                    context.Database.Connection.Open();
                    studentprofile studentPro = context.studentprofile.Where(x=>x.iD == stuprofile.iD).First();
                    studentPro.FirstName = stuprofile.FirstName;
                    studentPro.LastName = stuprofile.LastName;
                    studentPro.DoB = stuprofile.DoB;
                    studentPro.Emailaddress = stuprofile.Emailaddress;
                    studentPro.LastModifiedDate = DateTime.Now;

                    context.SaveChanges();
                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException)
                {

                }

                var userexists = context.studentprofile.Select(x => x.FirstName == stuprofile.FirstName && x.LastName == stuprofile.LastName && x.DoB == stuprofile.DoB).FirstOrDefault();
                return userexists;
            }

        }
        public static int FindUserId(string user_name)
        {
            using (var context = new AssessmentContext())
            {
                var userexists = from userinfo in context.userprofiles 
                                 where userinfo.UserName == user_name
                                 select userinfo.iD;
                return userexists.First();
            }
        }

        public static string FindUserName(int useriD)
        {
            using (var context = new AssessmentContext())
            {
                var userexists = from userinfo in context.userprofiles
                                 where userinfo.iD == useriD
                                 select userinfo.UserName;
                return userexists.First();
            }
        }


    }
}
