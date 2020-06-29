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
    public class GradesController : Controller
    {
        private readonly string DbConnection = ConfigurationManager.ConnectionStrings["QuizApplicationDatabase"].ConnectionString;
        private const string GET_GRADES = "GetGrades";

        // GET: Grades
        public ActionResult Index(int userId)
        {
            var grades = new List<Grades>();

            using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
            {
                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand(GET_GRADES, sqlConnection))
                    {
                        sqlConnection.Open();

                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {

                            while (sqlDataReader.Read())
                            {
                                var grade = new Grades()
                                {
                                    Grade = Convert.ToDecimal(sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("Grade"))),
                                    CategoryName = sqlDataReader["CategoryName"].ToString(),
                                    UserId = (int)sqlDataReader["UserId"],
                                };

                                grades.Add(grade);

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



            return View(grades);
        }
    }
}