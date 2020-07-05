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
        private const string GET_ALL_LECTURE_QUIZ = "GetAllLectureQuiz";

        public ActionResult Index()
        {
            var model = GetModules();
            return View(model);
        }

        private List<Module> GetModules()
        {
            var module = new List<Module>();
            string sqlConnection = "Data Source=localhost;Initial Catalog=QuizApplicationDatabase;Integrated Security=True";

            using (SqlConnection sqlConn = new SqlConnection(sqlConnection))
            {
                sqlConn.Open();
                var categories = GetAllCategories(sqlConn);

                foreach (var category in categories)
                {
                    var questions = GetAllQuestionsForCategory(category, sqlConn);
                    var lectures = GetAllLecturesForCategory(category, sqlConn);
                    module.Add(new Module
                    {
                        Category = category,
                        Questions = questions,
                        Lectures = lectures
                    });
                }

                sqlConn.Close();
            }
            return module;
        }

        private List<Lectures> GetAllLecturesForCategory(Category category, SqlConnection sqlConn)
        {
            var result = new List<Lectures>();
            using (SqlCommand sqlCommand = new SqlCommand("GetAllLecturesForCategory", sqlConn))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("CategoryId", category.CategoryId);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                { 
                    while (sqlDataReader.Read())
                    {
                        var question = new Lectures
                        {
                            Id = (int)sqlDataReader["Id"],
                            Lecture = sqlDataReader["Lecture"].ToString()
                        };
                        result.Add(question);

                    }
                }

            }
            return result;
        }

        private List<Question> GetAllQuestionsForCategory(Category category, SqlConnection sqlConn)
        {
            var result = new List<Question>();
            using (SqlCommand sqlCommand = new SqlCommand("GetAllQuestionsForCategory", sqlConn))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("CategoryId", category.CategoryId);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var question = new Question
                        {
                            QuestionId = (int)sqlDataReader["QuestionId"],
                            QuestionText = sqlDataReader["QuestionText"].ToString()
                        };
                        result.Add(question);

                    }
                }
            }
            return result;
        }

        private List<Category> GetAllCategories(SqlConnection sqlConn)
        {
            var result = new List<Category>();
            using (SqlCommand sqlCommand = new SqlCommand("GetAllCategories", sqlConn))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var category = new Category
                        {
                            CategoryId = (int)sqlDataReader["CategoryId"],
                            CategoryName = sqlDataReader["CategoryName"].ToString()
                        };
                        result.Add(category);

                    }
                }
            }
            return result;
        }
    }
}