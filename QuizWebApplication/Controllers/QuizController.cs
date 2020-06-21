using QuizWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizWebApplication.Controllers
{
    public class QuizController : Controller
    {
        private readonly string DbConnection = ConfigurationManager.ConnectionStrings["QuizApplicationDatabase"].ConnectionString;

        public ActionResult Index(string CategoryList)
        {

            if (!string.IsNullOrWhiteSpace(CategoryList))
            {
                return RedirectToAction("Take", new { categoryList = CategoryList });
            }

            var catList = new List<Category>();
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
                            catList.Add(new Category
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
            model.CategoryList = catList;
            //model.CategoryDropDown = new SelectList(categoryList, "CategoryId", "CategoryName");

            return View(model);
        }


        public ActionResult Take(string categoryList)
        {
            var categoryId = 0;
            if (!int.TryParse(categoryList, out categoryId))
            {
                return View("Error");
            }

            List<QuizCategoryQuestionClass> quiz = new List<QuizCategoryQuestionClass>();

            string connection = "Data Source=localhost;Initial Catalog=QuizApplicationDatabase;Integrated Security=True";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                var sqlQuery = $"SELECT [QuestionText] ,[QuestionOption1] ,[QuestionOption2] ,[QuestionOption3] ,[QuestionOption4],[CategoryName] ,[CorrectOption] FROM QuizQuestion INNER JOIN QuizCategory ON QuizCategory.CategoryId = QuizQuestion.CategoryId where QuizQuestion.CategoryId = { categoryList }";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        QuizCategoryQuestionClass quizCategoryQuestion = new QuizCategoryQuestionClass();
                        quizCategoryQuestion.CategoryId = int.Parse(categoryList);
                        quizCategoryQuestion.CategoryName = sqlDataReader["CategoryName"].ToString();
                        quizCategoryQuestion.QuestionText = sqlDataReader["QuestionText"].ToString();
                        quizCategoryQuestion.QuestionOption1 = sqlDataReader["QuestionOption1"].ToString();
                        quizCategoryQuestion.QuestionOption2 = sqlDataReader["QuestionOption2"].ToString();
                        quizCategoryQuestion.QuestionOption3 = sqlDataReader["QuestionOption3"].ToString();
                        quizCategoryQuestion.QuestionOption4 = sqlDataReader["QuestionOption4"].ToString();
                        quiz.Add(quizCategoryQuestion);
                    }

                    sqlCommand.Dispose();
                    sqlConnection.Close();
                }
            }
            // return RedirectToAction("Take");
            return View(quiz);
        }

        [HttpPost]
        public ActionResult Take(Answers submitAnswer)
        {
            string connection = "Data Source=localhost;Initial Catalog=QuizApplicationDatabase;Integrated Security=True";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                var sqlQuery = $"insert into Answers (StudentId, QuestionId, Answer) values ('{submitAnswer.StudentId}', '{submitAnswer.QuestionId}', '{submitAnswer.Answer}')";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Dispose();
                    sqlConnection.Close();
                }
            }
            return RedirectToAction("Done");
        }

        public ActionResult Done()
        {
            return View();
        }
    }
}