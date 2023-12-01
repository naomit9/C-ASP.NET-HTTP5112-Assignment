using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web.Http;
using SchoolProject.Models;
using System.Web.Http.Cors;

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

                object hireDateObject = ResultSet["hiredate"];
                string hireDateString;

                if (hireDateObject != null && hireDateObject != DBNull.Value && hireDateObject is DateTime)
                {
                    DateTime hireDate = (DateTime)ResultSet["hiredate"];
                    hireDateString = hireDate.ToShortDateString();
                } else
                {
                    hireDateString = "N/A";
                }

               
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
        // GET: api/TeacherData/FindTeacher/{id}

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

                object hireDateObject = ResultSet["hiredate"];
                string hireDateString;

                if (hireDateObject != null && hireDateObject != DBNull.Value && hireDateObject is DateTime)
                {
                    DateTime hireDate = (DateTime)ResultSet["hiredate"];
                    hireDateString = hireDate.ToShortDateString();
                }
                else
                {
                    hireDateString = "N/A";
                }

                decimal Salary = (decimal)ResultSet["salary"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.HireDate = hireDateString;
                NewTeacher.Salary = Salary;
            }

                return NewTeacher;
        }




        // This method will delete a teacher from the database
        // POST: api/TeacherData/DeleteTeacher/{id}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <example>POST: /api/TeacherData/DeleteTeacher/3</example>
        
        [HttpPost]
        [Route("api/TeacherData/DeleteTeacher/{id}")]
        [EnableCors(origins: "http://127.0.0.1:5500", methods: "DELETE", headers: "Content-Type")]

        public void DeleteTeacher(int id)
        {
            // Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and the database
            Conn.Open();

            // Establish a new comment or query for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            // Execute a non-select statement
            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        [HttpPost]
        public void AddTeacher(Teacher NewTeacher)
        {
            // Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and the database
            Conn.Open();

            // Establish a new comment or query for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, hiredate, salary, employeenumber) values (@TeacherFname,@TeacherLname,CURRENT_DATE(),@Salary,'T400')";
            cmd.Parameters.AddWithValue("TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("Salary", NewTeacher.Salary);

            cmd.Prepare();

            // Execute a non-select statement
            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }
}
