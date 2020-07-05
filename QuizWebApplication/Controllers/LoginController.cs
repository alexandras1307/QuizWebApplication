using QuizWebApplication.Models;
using QuizWebApplication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizWebApplication.Controllers
{
    public class LoginController : Controller
    {
        // GET: StudentLogin
        private readonly string DbConnection = ConfigurationManager.ConnectionStrings["QuizApplicationDatabase"].ConnectionString;
        private const string USER_LOGIN = "UserLogin";
        // SqlDataReader sqlDataReader;

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verify(UserProfile login)
        {
            using(var sqlConnection = new SqlConnection(DbConnection))
            {
                try
                {

                    sqlConnection.Open();

                    using (var sqlCommand = new SqlCommand(USER_LOGIN, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Email", login.Email);
                        sqlCommand.Parameters.AddWithValue("@Password", login.Password);

                        using (var sqlReader = sqlCommand.ExecuteReader())
                        {

                            if (sqlReader.Read())
                            {
                                Session["UserId"] = sqlReader.GetInt32(0);
                                var role = sqlReader.GetInt32(1);
                                sqlConnection.Close();
                                if(role == (int)RoleEnumeration.Student)
                                {
                                    return View("Dashboard");
                                }
                                else
                                {
                                    //return View("~Views/TeacherLogin/Dashboard");   
                                    return View("../TeacherLogin/Dashboard");
                                }
                            }
                            else
                            {
                                sqlConnection.Close();
                                return View("UnsuccessfulLogin");
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return View("UnsuccessfulLogin");
                }
            }
           
        }

        public ActionResult UnsuccessfulLogin()
        {
            return View();
        }
    }
}