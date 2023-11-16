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
    public class StudentDataController : ApiController
    {
        // Access School database through access context class
        private SchoolDbContext School = new SchoolDbContext();

        // This API Controller will access the students table of the school database
        /// <summary>
        /// Returns a list of students
        /// </summary>
        /// <example>
        /// GET api/StudentData/ListStudents
        /// </example>
        /// <returns>A list of student names (first names and last names)</returns>

        [HttpGet]
        [Route("api/StudentData/ListStudents/{SearchKey?}")]
        public IEnumerable<Student> ListStudents(string SearchKey=null)
        {
            // Create connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open connection
            Conn.Open();

            // Create command
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "Select * from Students where lower(studentfname) like lower('%"+SearchKey+"%') or lower(studentlname) like lower('%"+SearchKey+"%') or concat (studentfname,' ',studentlname) like lower('%"+SearchKey+"%')";

            //cmd.Parameters.AddWithValue("key", "%" + SearchKey + "%");
            //cmd.Prepare();

            // Gather ResultSet of query in a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Create an empty list
            List<Student> Students = new List<Student>();

            // Loop through ResultSet
            while (ResultSet.Read())
            {
                // Access column information
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = (string)ResultSet["studentfname"]; 
                string StudentLname = (string)ResultSet["studentlname"];
                string StudentNumber = (string)ResultSet["studentnumber"];
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];
                string enrolDateString = EnrolDate.ToShortDateString();

                Student NewStudent = new Student(); 
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = enrolDateString;


                // Add the Student name to the list
                Students.Add(NewStudent);
            }
            // Close connection
            Conn.Close();

            // Return list of students
            return Students;
        }



        // This method will return a singular student
        // GET: StudentData/FindStudent/{id}

        [HttpGet]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            // Create connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open connection
            Conn.Open();

            // Create command
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "Select * from Students where studentid =" + id;

            // Gather ResultSet of query in a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Loop through ResultSet
            while (ResultSet.Read())
            {
                // Access column information
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = (string)ResultSet["studentfname"];
                string StudentLname = (string)ResultSet["studentlname"];
                string StudentNumber = (string)ResultSet["studentnumber"];
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];
                string enrolDateString = EnrolDate.ToShortDateString();

                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = enrolDateString;
            }

            return NewStudent;
        }
    }
}
