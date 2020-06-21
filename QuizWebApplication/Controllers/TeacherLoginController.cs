using QuizWebApplication.Models;
using QuizWebApplication.Models.Enums;
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
        public ActionResult Verify(UserProfile login)
        {
            connectionString();
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = $"Select Email, Password from UserProfile where Active = 1 and Role = {(int)RoleEnumeration.Teacher} " +
                                    $"and Email= {login.Email} and Password= {login.Password} ";
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