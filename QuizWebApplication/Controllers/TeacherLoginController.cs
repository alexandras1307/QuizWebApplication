using QuizWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizWebApplication.Controllers
{
    public class TeacherLoginController : Controller
    {
        SqlConnection sqlConnection = new SqlConnection();
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataReader sqlDataReader;

        // GET: TeacherLogin
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        void connectionString()
        {
            sqlConnection.ConnectionString = "Data Source=localhost;Initial Catalog=QuizApplicationDatabase;Integrated Security=True";
        }

        [HttpPost]
        public ActionResult Verify(TeacherLogin teacherLogin)
        {
            connectionString();
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "select TeacherEmail , Password from TeacherProfile where TeacherEmail='" + teacherLogin.TeacherEmail + "' and Password='" + teacherLogin.Password + "' ";
            sqlDataReader = sqlCommand.ExecuteReader();

            if(sqlDataReader.Read())
            {
                sqlConnection.Close();
                return View("Dashboard");
            }
            else
            {
                sqlConnection.Close();
                return View("UnsuccessfulLogin");
            }
        }
    }
}