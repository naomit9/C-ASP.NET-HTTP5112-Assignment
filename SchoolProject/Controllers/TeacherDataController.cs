using MySql.Data.MySqlClient;
using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web.Http;

namespace SchoolProject.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class allows us to access our MySql School Database
        private SchoolDbContext School = new SchoolDbContext();

        // This API Controller will access the teachers table of the school database
        /// <summary>
        /// Returns a list of Teachers in the system
        /// <example>
        /// GET api/TeacherData/ListTeachers
        /// </example>
        /// </summary>
        /// <returns>A list of teachers (first names and last names)</returns>
        
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            // Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower('%"+SearchKey+"%') or lower(teacherlname) like lower('%"+SearchKey+"%')  or (concat(teacherfname, ' ' , teacherlname)) like lower('%"+SearchKey+"%') ";

            //cmd.Parameters.AddWithValue("key", "%" + SearchKey + "%");   // WHEN I CHANGED IT IT TO '%@key%', THE ENTIRE LIST WAS GONE. I WILL KEEP IT AS ('%"+SearchKey+"%').
            //cmd.Prepare();

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            // Loop through each row the result set
            while (ResultSet.Read())
            {
                // Access column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                DateTime hireDate = (DateTime)ResultSet["hiredate"];
                string hireDateString = hireDate.ToShortDateString();
                decimal Salary = (decimal)ResultSet["salary"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.HireDate = hireDateString;
                NewTeacher.Salary = Salary;

                // Add the Teacher Name to the list
                Teachers.Add(NewTeacher);
            }

            // Close the connection between the web server and database
            Conn.Close();

            // Return the final list of teacher names
            return Teachers;
        }




        // This method will return a singlular teacher
        // GET: TeacherData/FindTeacher/{id}

        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            // Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "Select * from Teachers where teacherid = "+id;

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                // Access column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                DateTime hireDate = (DateTime)ResultSet["hiredate"];
                string hireDateString = hireDate.ToShortDateString();
                decimal Salary = (decimal)ResultSet["salary"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.HireDate = hireDateString;
                NewTeacher.Salary = Salary;
            }

                return NewTeacher;
        }
    }
}
