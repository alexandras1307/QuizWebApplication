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
        private const string GET_FINAL_GRADE = "GetFinalGrade";

        // GET: Grades
        public ActionResult Index()
        {
            var grades = new List<Grades>();

            using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
            {
                //try
                //{
                using (SqlCommand sqlCommand = new SqlCommand(GET_GRADES, sqlConnection))
                {
                    sqlConnection.Open();

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserId", Session["UserId"]);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        while (sqlDataReader.Read())
                        {
                            var grade = new Grades();

                            if (!sqlDataReader.IsDBNull(3))
                                grade.Grade = Convert.ToDecimal(sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("Grade")));
                            else
                            {
                                grade.Grade = 0;
                            }
                            if (!sqlDataReader.IsDBNull(4))
                                grade.CategoryName = sqlDataReader["CategoryName"].ToString();
                            else
                            {
                                grade.CategoryName = "No grades yet";
                            };
                            grade.UserId = (int)sqlDataReader["UserId"];

                            grades.Add(grade);

                        }
                    }
                }


                // insert user into Final grade to allow the grade to be updated after each quiz
                using (SqlCommand sqlCommand = new SqlCommand(GET_FINAL_GRADE, sqlConnection))
                {

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserId", Session["UserId"]);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        while (sqlDataReader.Read())
                        {
                            var grade = new Grades()
                            {
                                FinalGrade = Convert.ToDecimal(sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("FinalGrade"))),
                            };

                            grades.Add(grade);

                        }
                    }
                }
            }

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return View("Error");
            //}        

            return View(grades);
    }
}
}