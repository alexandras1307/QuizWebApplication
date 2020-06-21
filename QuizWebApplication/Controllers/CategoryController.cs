using QuizWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace QuizWebApplication.Controllers
{
    public class CategoryController : Controller
    {
        private readonly string DbConnection = ConfigurationManager.ConnectionStrings["QuizApplicationDatabase"].ConnectionString;
        private const string GET_ALL_CATEGORIES = "GetAllCategories";
        private const string INSERT_NEW_CATEGORY = "InsertNewCategory";

        // GET: CreateCategory
        [HttpGet]
        public ActionResult Index()
        {

            var list = new List<Category>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
                {

                    sqlConnection.Open();

                    using (var sqlCommand = new SqlCommand(GET_ALL_CATEGORIES, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        using (var sqlReader = sqlCommand.ExecuteReader())
                        {

                            while(sqlReader.Read())
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

                return View(list);
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Category category)
        {

            using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
            {
                using (SqlCommand sqlCommand = new SqlCommand(INSERT_NEW_CATEGORY, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CategoryName", category.CategoryName);
   
                    sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }

                return RedirectToAction("Index");
            }
        }

        //public ActionResult Details(QuizCategoryQuestionClass createQuestion)
        //{
        //    string connection = "Data Source=localhost;Initial Catalog=QuizApplicationDatabase;Integrated Security=True";

        //    using (SqlConnection sqlConnection = new SqlConnection(connection))
        //    {
        //        //var sqlQuery = $"SELECT * FROM QuizQuestion QQ JOIN QuizCategory QC ON QQ.CategoryId = QC.CategoryId WHERE QQ.CategoryId = { categoryId }";

        //        using SqlCommand sqlCommand = new SqlCommand("spDetailsCategory");
        //        {
        //            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //            sqlCommand.Parameters.Add("@CategoryId", SqlDbType.Text);
        //            sqlConnection.Open();
        //            sqlCommand.ExecuteNonQuery();
        //            sqlCommand.Dispose();
        //            sqlConnection.Close();
        //        }
        //    }
        //    return View();
        //}



        //public static List<QuizCategoryQuestionClass> ListQuestionsCategory()
        //{
        //    List<QuizCategoryQuestionClass> questions = new List<QuizCategoryQuestionClass>();
        //    string sqlConnection = "Data Source=localhost;Initial Catalog=QuizApplicationDatabase;Integrated Security=True";

        //    using (SqlConnection sqlConn = new SqlConnection(sqlConnection))
        //    {
        //        using (SqlCommand sqlCommand = new SqlCommand("spDetailsCategory", sqlConn))
        //        {
        //            sqlConn.Open();
        //            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

        //            var Parameter = new SqlParameter("@CategoryId", SqlDbType.Structured);
        //            Parameter.Value = questions;
        //            Parameter.TypeName = "dbo.QuizApplicationDatabase";
        //            sqlCommand.Parameters.Add(Parameter);

        //            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        //            while (sqlDataReader.Read())
        //            {
        //                QuizCategoryQuestionClass quizCategoryQuestion = new QuizCategoryQuestionClass();
        //                quizCategoryQuestion.CategoryName = sqlDataReader["CategoryName"].ToString();
        //                quizCategoryQuestion.QuestionText = sqlDataReader["QuestionText"].ToString();
        //                quizCategoryQuestion.QuestionOption1 = sqlDataReader["QuestionOption1"].ToString();
        //                quizCategoryQuestion.QuestionOption2 = sqlDataReader["QuestionOption2"].ToString();
        //                quizCategoryQuestion.QuestionOption3 = sqlDataReader["QuestionOption3"].ToString();
        //                quizCategoryQuestion.QuestionOption4 = sqlDataReader["QuestionOption4"].ToString();
        //                quizCategoryQuestion.CorrectOption = sqlDataReader["CorrectOption"].ToString();
        //                questions.Add(quizCategoryQuestion);
        //            }
        //            sqlConn.Close();
        //        }
        //        return questions;
        //    }

        //}

    }
}


