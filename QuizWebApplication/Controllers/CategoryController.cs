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
        CategoryDataAccess categoryDataAccess = new CategoryDataAccess();

        // GET: CreateCategory
        [HttpGet]
        public ActionResult Index()
        {
            List<Models.Category> list = new List<Models.Category>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    string sqlQuery = "select * from QuizCategory";
                    sqlConnection.ConnectionString = "Data Source=localhost;Initial Catalog=QuizApplicationDatabase;Integrated Security=True";

                    DataTable dataTable = new DataTable();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = new SqlCommand(sqlQuery, sqlConnection);

                    sqlDataAdapter.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var category = new Models.Category
                        {
                            CategoryId = row["CategoryId"].GetHashCode(),
                            CategoryName = row["CategoryName"].ToString()
                        };
                        list.Add(category);
                    }
                    sqlDataAdapter.Dispose();
                    sqlConnection.Close();
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
        public ActionResult Create(Category createCategory)
        {
            string connection = "Data Source=localhost;Initial Catalog=QuizApplicationDatabase;Integrated Security=True";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                var sql = $"insert into QuizCategory (CategoryName) values ('{createCategory.CategoryName}')";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Dispose();
                    sqlConnection.Close();
                }
            }
            return RedirectToAction("Index");
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


