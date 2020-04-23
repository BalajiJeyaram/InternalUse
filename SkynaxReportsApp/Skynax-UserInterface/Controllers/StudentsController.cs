using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using SkynaxEntities.DAL;
using SkynaxEntities.Models;

namespace Skynax_UserInterface.Controllers
{
    public class StudentsController : Controller
    {
        private AssessmentContext assessmentdb = new AssessmentContext();
        private SchoolContext db = new SchoolContext();
        private static HttpClient client = new HttpClient();
        

        // GET: Students
        public ActionResult Index()
        {
            return View(assessmentdb.studentprofile.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            try
            {
                //throw new NullReferenceException("Test");

                if (Session["InvalidUser"].ToString() == "ValidUser")
                {
                    ViewBag.Message = "Assessment Page";
                    return View();
                }
                else
                {
                    Session["InvalidUser"] = "You did not login yet!";
                    //return View("LogIn");
                    return RedirectToAction("LogIn", "Home");
                }
            }
            catch (NullReferenceException nullexp)
            {
                return RedirectToAction("Index", "Error", new { message = nullexp.StackTrace });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { message = ex.StackTrace });
            }

            //return View();
        }

        public ActionResult SubmitStudent(string user_name,string first_name, string last_name, string dob, string email)
        {
            studentprofile sprofile = new studentprofile()
            {
                iD = 0,
                FirstName = first_name,
                LastName = last_name,
                DoB = Convert.ToDateTime(dob),
                Emailaddress = email,
                Optional = "A",
                useriD = UserValidation.FindUserId(user_name)

            };

            bool result = UserValidation.CreateStudent(sprofile);
                return View("Create");
        }

        public ActionResult GetCourses()
        {
            string path= "http://localhost:50079/api/courses";
            IList<Course> localcource = null;
            var response =  client.GetAsync(path);
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var resultobject =  result.Content.ReadAsAsync<IList<Course>>();
                localcource = resultobject.Result;
            }
            return View(localcource);

        }


        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,LastName,FirstMidName,EnrollmentDate")] Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Students.Add(student);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(student);
        //}

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentprofile student = assessmentdb.studentprofile.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            student.username = UserValidation.FindUserName(student.useriD);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
