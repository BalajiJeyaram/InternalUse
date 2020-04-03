using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkynaxEntities.DAL;
using SkynaxEntities.Models;

namespace SkynaxEntities
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {
                var user = new userprofile() { iD = 1, UserName = "balajikj", Password = "password",
                    ActiveUser = Convert.ToChar("Y"), CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now };
            } 
        }
    }
}
