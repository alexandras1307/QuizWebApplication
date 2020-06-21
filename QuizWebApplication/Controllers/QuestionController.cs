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
        private const string GET_ALL_QUESTIONS = "GetAllQuestions";
        private const string GET_ALL_CATEGORIES = "GetAllCategories";
        private const string INSERT_NEW_QUESTION = "InsertNewQuestion";
        // GET: Question
        public ActionResult Index()
        {
            ViewData["Message"] = Questions();
            return View();
        }

        public List<QuizCategoryQuestionClass> Questions()
        {
            var questions = new List<QuizCategoryQuestionClass>();

            using (var sqlConn = new SqlConnection(DbConnection))
            {
                using (SqlCommand sqlCommand = new SqlCommand(GET_ALL_QUESTIONS, sqlConn))
                {
                    sqlConn.Open();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            var question = new QuizCategoryQuestionClass
                            {
                                CategoryName = sqlDataReader["CategoryName"].ToString(),
                                QuestionText = sqlDataReader["QuestionText"].ToString(),
                                QuestionOption1 = sqlDataReader["QuestionOption1"].ToString(),
                                QuestionOption2 = sqlDataReader["QuestionOption2"].ToString(),
                                QuestionOption3 = sqlDataReader["QuestionOption3"].ToString(),
                                QuestionOption4 = sqlDataReader["QuestionOption4"].ToString(),
                                CorrectOption = sqlDataReader["CorrectOption"].ToString()
                            };

                            questions.Add(question);
                        }
                    }
                }
            }
            return questions;
        }

        public ActionResult Create()
        {

            try
            {
                var list = new List<Category>();
                using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
                {

                    sqlConnection.Open();

                    using (var sqlCommand = new SqlCommand(GET_ALL_CATEGORIES, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        using (var sqlReader = sqlCommand.ExecuteReader())
                        {

                            while (sqlReader.Read())
                            {
                                var cat = new Category
                                {
                                    CategoryId = sqlReader.GetInt32(0),
                                    CategoryName = sqlReader.GetString(1)
                                };

                                list.Add(cat);
                            };
                        }
                    }
                }

                var model = new Question();
                model.CategoryList = list;

                return View(model);
            }
            catch
            {
                return View("Error");
            }

            
        }

        [HttpPost]
        public ActionResult Create(Question question, string CategoryList)
        {
            var categoryId = 0;
            if (!int.TryParse(CategoryList, out categoryId))
            {
                return View("Error");
            }


            using (var sqlConnection = new SqlConnection(DbConnection))
            {
                try
                {

                    sqlConnection.Open();

                    using (var sqlCommand = new SqlCommand(INSERT_NEW_QUESTION, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@QuestionText", question.QuestionText);
                        sqlCommand.Parameters.AddWithValue("@QuestionOption1", question.QuestionOption1);
                        sqlCommand.Parameters.AddWithValue("@QuestionOption2", question.QuestionOption2);
                        sqlCommand.Parameters.AddWithValue("@QuestionOption3", question.QuestionOption3);
                        sqlCommand.Parameters.AddWithValue("@QuestionOption4", question.QuestionOption4);
                        sqlCommand.Parameters.AddWithValue("@CorrectOption", question.CorrectOption);
                        sqlCommand.Parameters.AddWithValue("@CategoryId", categoryId);

                        sqlCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return View("Error");
                }
            }

            return RedirectToAction("Index");
        }
    }
}