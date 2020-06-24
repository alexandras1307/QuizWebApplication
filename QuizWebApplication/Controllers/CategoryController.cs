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
    }
}


