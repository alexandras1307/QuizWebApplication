using QuizWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizWebApplication.Controllers
{
    public class TeacherRegistrationController : Controller
    {
        // GET: TeacherRegistration
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(TeacherRegistrationClass teacherRegistrationObject)
        {
            string connection = "Data Source=localhost;Initial Catalog=QuizApplicationDatabase;Integrated Security=True";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                string sqlQuery = "insert into TeacherRegistration(TeacherName, TeacherEmail, Password, ConfirmPassword) values ('" + teacherRegistrationObject.TeacherName + "' , '" + teacherRegistrationObject.TeacherEmail + "' , '" + teacherRegistrationObject.Password + "' , '" + teacherRegistrationObject.ConfirmPassword + "')";
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    if(ModelState.IsValid)
                    {
                        return RedirectToAction("/TeacherRegistration/SuccessfulRegistrationTeacher");
                    }
                    //ViewData["Message"] = "Your new teacher account" + teacherRegistrationObject.TeacherName + "has been created";
                }
            }

            return View(teacherRegistrationObject);
        }

        public ActionResult SuccessfulRegistrationTeacher()
        {
            return View();
        }
    }
}