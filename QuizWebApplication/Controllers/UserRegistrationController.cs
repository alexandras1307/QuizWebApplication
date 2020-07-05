using QuizWebApplication.Models;
using QuizWebApplication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizWebApplication.Controllers
{
    public class UserRegistrationController : Controller
    {
        private const string INSERT_NEW_PROFILE = "InsertUserProfile";
        private const string INSERT_FINAL_GRADE = "InsertFinalGrade";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserProfile profile)
        {
            string connection = "Data Source=localhost;Initial Catalog=QuizApplicationDatabase;Integrated Security=True";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                using (SqlCommand sqlCommand = new SqlCommand(INSERT_NEW_PROFILE, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Email", profile.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", profile.Password);
                    sqlCommand.Parameters.AddWithValue("@FirstName", profile.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", profile.LastName);
                    sqlCommand.Parameters.AddWithValue("@Role", (int)RoleEnumeration.Student);
                    sqlCommand.Parameters.AddWithValue("@Active", true);
                    sqlCommand.ExecuteNonQuery();

                    if (ModelState.IsValid)
                    {
                        return RedirectToAction("SuccessfulRegistrationStudent");
                    }
                    else
                    {
                        return RedirectToAction("UnsuccessfulRegistration");
                    }

                }

                return View(profile);
            }
        }

        public ActionResult SuccessfulRegistrationStudent()
        {
            return View();
        }

        public ActionResult UnsuccessfulRegistration()
        {
            return View();
        }
    }
}