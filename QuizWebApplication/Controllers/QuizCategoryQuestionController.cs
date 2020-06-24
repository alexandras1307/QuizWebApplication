using QuizWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizWebApplication.Controllers
{
    public class QuizCategoryQuestionController : Controller
    {
        private const string GET_ALL_QUESTIONS_BY_CATEGORY = "GetQuestionsByCategory";

        public ActionResult Index()
        {
            ViewData["Message"] = CategoryQuestions();
            return View();
        }

        public static List<QuizCategoryQuestionClass> CategoryQuestions()
        {
            List<QuizCategoryQuestionClass> categoryQuestions = new List<QuizCategoryQuestionClass>();
            string sqlConnection = "Data Source=localhost;Initial Catalog=QuizApplicationDatabase;Integrated Security=True";

            using (SqlConnection sqlConn = new SqlConnection(sqlConnection))
            {
                using (SqlCommand sqlCommand = new SqlCommand("GetQuestionsByCategory", sqlConn))
                {
                    sqlConn.Open();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        QuizCategoryQuestionClass quizCategoryQuestion = new QuizCategoryQuestionClass();
                        quizCategoryQuestion.CategoryName = sqlDataReader["CategoryName"].ToString();
                        quizCategoryQuestion.QuestionText = sqlDataReader["QuestionText"].ToString();
                        quizCategoryQuestion.QuestionOption1 = sqlDataReader["QuestionOption1"].ToString();
                        quizCategoryQuestion.QuestionOption2 = sqlDataReader["QuestionOption2"].ToString();
                        quizCategoryQuestion.QuestionOption3 = sqlDataReader["QuestionOption3"].ToString();
                        quizCategoryQuestion.QuestionOption4 = sqlDataReader["QuestionOption4"].ToString();
                        quizCategoryQuestion.CorrectOption = sqlDataReader["CorrectOption"].ToString();
                        categoryQuestions.Add(quizCategoryQuestion);
                    }
                    sqlConn.Close();
                }
                return categoryQuestions;
            }
            
        }
    }
}