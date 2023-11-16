using MySql.Data.MySqlClient;
using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolProject.Controllers
{
    public class CourseDataController : ApiController
    {
        // Access School database through access context class
        private SchoolDbContext School = new SchoolDbContext();

        // This API Controller will access the class table of the school database
        /// <summary>
        /// Returns a list of classes in the system
        /// </summary>
        /// <example>
        /// GET api/CourseData/ListCourses
        /// </example>
        /// <returns>A list of class names</returns>

        [HttpGet]
        [Route("api/CourseData/ListCourses")]
        public IEnumerable<Course> ListCourses()
        {
            // Create connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open connection
            Conn.Open();

            // Create command (query)
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL QUERY
            cmd.CommandText = "Select * from Classes";

            // Gather ResultSet of query in a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Create an empty list
            List<Course> Courses = new List<Course> { };

            // Loop through the ResultSet
            while (ResultSet.Read() )
            {
                // Access column information
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassCode = (string)ResultSet["classcode"]; 
                string ClassName = (string)ResultSet["classname"];
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                string startDateString = StartDate.ToShortDateString();
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];
                string finishDateString = FinishDate.ToShortDateString();

                Course NewCourse = new Course();
                NewCourse.ClassId = ClassId;
                NewCourse.ClassCode = ClassCode;
                NewCourse.ClassName = ClassName;
                NewCourse.StartDate = startDateString;
                NewCourse.FinishDate = finishDateString;

                // Add the Class name to list
                Courses.Add(NewCourse);
            }

            // Close connection
            Conn.Close();

            // Return list of class names
            return Courses;
        }



        // This method will return a singular class
        // GET: CourseData/FindCourse/{id}

        [HttpGet]
        public Course FindCourse(int id)
        {
            Course NewCourse = new Course();

            // Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and the database
            Conn.Open();

            // Establish a new command  or query for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL QUERY
            cmd.CommandText = "Select * from Classes where classid = " +id;

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read() )
            {
                // Access column information by the DB column name as an index
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassCode = (string)ResultSet["classcode"];
                string ClassName = (string)ResultSet["classname"];
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                string startDateString = StartDate.ToShortDateString();
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];
                string finishDateString = FinishDate.ToShortDateString();

                NewCourse.ClassId = ClassId;
                NewCourse.ClassCode = ClassCode;
                NewCourse.ClassName = ClassName;
                NewCourse.StartDate = startDateString;
                NewCourse.FinishDate = finishDateString;
            }

            return NewCourse;
        }
    }
}
