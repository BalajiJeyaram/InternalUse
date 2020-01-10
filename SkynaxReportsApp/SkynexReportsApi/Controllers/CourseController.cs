using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SkynexReportsApi.Models;
namespace SkynexReportsApi.Controllers
{
    public class CourseController : ApiController
    {
        Course[] Courses = new Course[]
        {
            new Course{ CourseID = 1, Credits = 100, Title="Computer Application"},
            new Course{ CourseID = 2, Credits = 100, Title="Master in Mathamatics"},
            new Course{ CourseID = 3, Credits = 100, Title="Master in Science"},
            new Course{ CourseID = 4, Credits = 100, Title="Master in Indian History"},
            new Course{ CourseID = 5, Credits = 100, Title="Master in American History"},
            new Course{ CourseID = 6, Credits = 100, Title="Master in Modern Technology"},
            new Course{ CourseID = 7, Credits = 100, Title="Bachelor of Arts"},
            new Course{ CourseID = 8, Credits = 100, Title="Bachelor of History"},
            new Course{ CourseID = 9, Credits = 100, Title="Bachelor of Science"},
            new Course{ CourseID = 10, Credits = 100, Title="Bachelor of Industrial Study"},


        };
        [Route("api/courses")]
        public IEnumerable<Course> GetAllCourses()
        {
            return Courses;
        }

        [Route("api/course/{id:int}")]
        public IHttpActionResult GetCourse(int id)
        {
            var course = Courses.FirstOrDefault((c) => c.CourseID == id);
            if (course == null)
                return NotFound();

            return Ok(course);
        }
    }
}
