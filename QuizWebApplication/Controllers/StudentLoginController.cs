using QuizWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizWebApplication.Controllers
{
    public class StudentLoginController : Controller
    {
        // GET: StudentLogin
        SqlConnection sqlConnection = new SqlConnection();
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataReader sqlDataReader;

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
        public ActionResult Verify(StudentRegistration studentLogin)
        {
            connectionString();
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "Select StudentEmail, Password from StudentProfile where StudentEmail='" + studentLogin.StudentEmail + "' and Password='" + studentLogin.Password + "' ";
            sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.Read())
            {
                sqlConnection.Close();
                return View("Dashboard");
            }
            else
            {
                sqlConnection.Close();
                return View("Error");
            }
        }
    }
}