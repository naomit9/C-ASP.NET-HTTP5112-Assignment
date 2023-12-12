using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Org.BouncyCastle.Asn1.Mozilla;
using SchoolProject.Models;
using System.Diagnostics;

namespace SchoolProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: Teacher/List/{teacherfname} or GET: Teacher/List/{teacherl name}
        public ActionResult List(string SearchKey=null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        // GET: Teacher/Show/{id}
        public ActionResult Show(int id) 
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            
            return View(SelectedTeacher);
        }

        // GET: Teacher/DeleteComfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }


        // POST: /Teacher/Delete/{id}
        [HttpPost]
        public   ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        // GET: /Teacher/New/
        public ActionResult New()
        {
            return View();
        }


        // POST: /Teacher/Create/
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, decimal Salary)
        {
            // Identify this method is running
            // Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the Create method");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(Salary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }


        /// <summary>
        /// Routes to a dynamically generated 'Author Update' page. Gathers information from the database
        /// </summary>
        /// <param name="id">ID of the teacher</param>
        /// <returns>A dynamic 'Update Author' webpage which provides the current information of the author and asks the user for new information as part of a form</returns>
        /// <example>GET: /Teacher/Update/{id]</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values. Conveys this information to the API, and redirects to the 'Teacher Show' page of our updated teacher.
        /// </summary>
        /// <param name="id">ID of the teacher</param>
        /// <param name="TeacherFname">First name of the teacher</param>
        /// <param name="TeacherLname">Last name of the teacher</param>
        /// <param name="Salary">Salary of the teacher</param>
        /// <returns>A dynamic webpage which provides the current information of the author</returns>
        /// <example>
        /// POST: /Teacher/Update/{4}
        /// FORM DATE / POST DATE / REQUEST BODY
        /// {
        /// "TeacherFname": "Lauren",
        /// "TeacherLname": "Smith",
        /// "Salary": "74.20"
        /// }
        /// </example>
        /// 
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, decimal Salary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}

