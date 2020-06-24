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
    public class QuizController : Controller
    {
        private readonly string DbConnection = ConfigurationManager.ConnectionStrings["QuizApplicationDatabase"].ConnectionString;
        private const string GET_ALL_QUESTIONS_BY_CATEGORY = "GetQuestionsByCategory";
        private const string GET_ALL_CATEGORIES = "GetAllCategories";
        private const string GET_UNSOLVED_QUIZ = "GetUnsolvedQuiz";
        private const string INSERT_NEW_ANSWER = "InsertNewAnswer";
        private const string GET_ANSWERS_BY_STUDENT_CATEGORY = "GetAnswersByStudentAndCategory";

        public ActionResult Index(string CategoryList)
        {

            if (!string.IsNullOrWhiteSpace(CategoryList))
            {
                return RedirectToAction("Take", new { categoryList = CategoryList });
            }

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


        public ActionResult Take(string categoryList)
        {
            var categoryId = 0;
            if (!int.TryParse(categoryList, out categoryId))
            {
                return View("Error");
            }

            List<QuizCategoryQuestionClass> quizList = new List<QuizCategoryQuestionClass>();

            var quiz = new QuizCategoryQuestionClass();
            quiz.Questions = new List<Answers>();
            quiz.CategoryId = categoryId;
            using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
            {

                using (SqlCommand sqlCommand = new SqlCommand(GET_ALL_QUESTIONS_BY_CATEGORY, sqlConnection))
                {
                    sqlConnection.Open();

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CategoryId", categoryId);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        
                        while (sqlDataReader.Read())
                        {
                            quiz.CategoryName = sqlDataReader["CategoryName"].ToString();
                            quiz.Questions.Add(
                            new Answers
                            {
                                Answer = "",
                                QuestionId = (int)sqlDataReader["QuestionId"],
                                QuestionText = sqlDataReader["QuestionText"].ToString(),
                                QuestionOption1 = sqlDataReader["QuestionOption1"].ToString(),
                                QuestionOption2 = sqlDataReader["QuestionOption2"].ToString(),
                                QuestionOption3 = sqlDataReader["QuestionOption3"].ToString(),
                                QuestionOption4 = sqlDataReader["QuestionOption4"].ToString()
                            });
                        }
                    }               
                }
            }

            return View(quiz);
        }

        [HttpPost]
        public ActionResult Take(QuizCategoryQuestionClass model)
        {

            using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
            {
                sqlConnection.Open();
                foreach (var answer in model.Questions)
                {
                    using (SqlCommand sqlCommand = new SqlCommand(INSERT_NEW_ANSWER, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;


                        sqlCommand.Parameters.AddWithValue("@UserId", Session["UserId"]);
                        sqlCommand.Parameters.AddWithValue("@QuestionId", answer.QuestionId);
                        sqlCommand.Parameters.AddWithValue("@Answer", answer.Answer);

                        sqlCommand.ExecuteNonQuery();
                    }

                }
            }
            TempData["Answers"] = model;
            return RedirectToAction("Done");
        }

        public ActionResult Done()
        {
            var answers = TempData["Answers"] as QuizCategoryQuestionClass;
            List<QuizCategoryQuestionClass> quizList = new List<QuizCategoryQuestionClass>();

            var quiz = new QuizCategoryQuestionClass();
            quiz.Questions = new List<Answers>();
            using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
            {

                using (SqlCommand sqlCommand = new SqlCommand(GET_ANSWERS_BY_STUDENT_CATEGORY, sqlConnection))
                {
                    sqlConnection.Open();

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CategoryId", answers.CategoryId);
                    sqlCommand.Parameters.AddWithValue("@UserId", Session["UserId"]);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        while (sqlDataReader.Read())
                        {
                            quiz.CategoryName = sqlDataReader["CategoryName"].ToString();
                            quiz.Questions.Add(
                            new Answers
                            {
                                UserId = (int)sqlDataReader["UserId"],
                                Answer = sqlDataReader["Answer"].ToString(),
                                QuestionText = sqlDataReader["QuestionText"].ToString(),
                                CategoryName = sqlDataReader["CategoryName"].ToString()
                            });
                        }
                    }
                }
            }

            return View(quiz);
        }
    }
}