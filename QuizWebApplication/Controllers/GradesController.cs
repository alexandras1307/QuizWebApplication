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

        //// GET: Grades
        //public ActionResult Index(Answers grades)
        //{
        //    var grade = new Answers();
        //    grade.Answer = new List<Gr>();
        //    using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
        //    {

        //        using (SqlCommand sqlCommand = new SqlCommand(GET_GRADES, sqlConnection))
        //        {
        //            sqlConnection.Open();

        //            sqlCommand.CommandType = CommandType.StoredProcedure;
        //            sqlCommand.Parameters.AddWithValue("@UserId", Session["UserId"]);

        //            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
        //            {

        //                while (sqlDataReader.Read())
        //                {
        //                    grade.CategoryName = sqlDataReader["CategoryName"].ToString();
        //                    Session["UserId"] = sqlDataReader.GetInt32(0);

        //                    grade.Answer.Add(
        //                    new Answers
        //                    {
        //                        Answer = sqlDataReader["Answer"].ToString(),
        //                        QuestionId = (int)sqlDataReader["QuestionId"],
        //                        QuestionText = sqlDataReader["QuestionText"].ToString(),
        //                        CategoryId = (int)sqlDataReader["CategoryId"],
        //                        CategoryName = sqlDataReader["CategoryName"].ToString(),
        //                        CorrectOption = sqlDataReader["QuestionOption"].ToString()
        //                    });
        //                }
        //            }
        //        }
        //    }

        //    return View(grade);
        //}
    }
}