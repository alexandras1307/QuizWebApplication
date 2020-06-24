using QuizWebApplication.Models;
using QuizWebApplication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizWebApplication.Controllers
{
    public class ProfileController : Controller
    {
        private readonly string DbConnection = ConfigurationManager.ConnectionStrings["QuizApplicationDatabase"].ConnectionString;
        private const string GET_USER_PROFILE = "GetUserProfile";

        // GET: Profile
        public ActionResult Index(UserProfile profile)
        {
            var users = new List<UserProfile>();

            using (var sqlConnection = new SqlConnection(DbConnection))
            {
                try
                {

                    sqlConnection.Open();

                    using (var sqlCommand = new SqlCommand(GET_USER_PROFILE, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@UserId", Session["UserId"]);

                        using (var sqlReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                var user = new UserProfile
                                {
                                    FirstName = sqlReader["FirstName"].ToString(),
                                    LastName = sqlReader["LastName"].ToString(),
                                    Email = sqlReader["Email"].ToString()
                                };

                                users.Add(user);
                            }
                        }
                    }
            }
                catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }
            return View(users);
        }
    }
}