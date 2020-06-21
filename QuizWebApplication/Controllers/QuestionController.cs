using QuizWebApplication.Models;
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
    public class QuestionController : Controller
    {
        private readonly string DbConnection = ConfigurationManager.ConnectionStrings["QuizApplicationDatabase"].ConnectionString;
        // GET: Question
        public ActionResult Index()
        {
            ViewData["Message"] = Questions();
            return View();
        }

        public static List<QuizCategoryQuestionClass> Questions()
        {
            List<QuizCategoryQuestionClass> questions = new List<QuizCategoryQuestionClass>();
            string sqlConnection = "Data Source=localhost;Initial Catalog=QuizApplicationDatabase;Integrated Security=True";

            using (SqlConnection sqlConn = new SqlConnection(sqlConnection))
            {
                using (SqlCommand sqlCommand = new SqlCommand("QuizCategoryQuestion", sqlConn))
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
                        questions.Add(quizCategoryQuestion);
                    }
                    sqlConn.Close();
                }
                return questions;
            }

        }

        public ActionResult Create()
        {
            var categoryList = new List<Category>();
            using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
            {
                var query = @"SELECT 
                                QC.CategoryId,
                                QC.CategoryName
                             FROM QuizCategory QC";

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            categoryList.Add(new Category
                            {
                                CategoryId = reader.GetInt32(0),
                                CategoryName = reader.GetString(1)
                            });
                        }
                    }
                    reader.Close();
                    command.Dispose();
                    sqlConnection.Close();
                }
            }

            var model = new Question();
            model.CategoryList = categoryList;
            //model.CategoryDropDown = new SelectList(categoryList, "CategoryId", "CategoryName");

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Question createQuestion, string CategoryList)
        {
            var categoryId = 0;
            if(!int.TryParse(CategoryList, out categoryId))
            {
                return View("Error");
            }

            string connection = "Data Source=localhost;Initial Catalog=QuizApplicationDatabase;Integrated Security=True";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                var sqlQuery = $"insert into QuizQuestion (QuestionText, QuestionOption1, QuestionOption2, QuestionOption3, QuestionOption4, CategoryId) values ('{createQuestion.QuestionText}', '{createQuestion.QuestionOption1}', '{createQuestion.QuestionOption2}', '{createQuestion.QuestionOption3}', '{createQuestion.QuestionOption4}', '{categoryId}')";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Dispose();
                    sqlConnection.Close();
                }
            }
            return RedirectToAction("Index");
        }
    }
}