using QuizWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizWebApplication.Controllers
{
    public class LecturesController : Controller
    {
        private readonly string DbConnection = ConfigurationManager.ConnectionStrings["QuizApplicationDatabase"].ConnectionString;
        private const string GET_ALL_LECTURES = "GetAllLectures";
        private const string GET_ALL_CATEGORIES = "GetAllCategories";
        private const string INSERT_NEW_LECTURE = "InsertLecture";

        public ActionResult Index()
        {
            ViewData["Message"] = Lectures();
            return View();
        }

        public List<Lectures> Lectures()
        {
            var lectures = new List<Lectures>();

            using (var sqlConn = new SqlConnection(DbConnection))
            {
                using (SqlCommand sqlCommand = new SqlCommand(GET_ALL_LECTURES, sqlConn))
                {
                    sqlConn.Open();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            var lecture = new Lectures
                            {
                                // CategoryName = sqlDataReader["CategoryName"].ToString(),
                                Lecture = sqlDataReader["Lecture"].ToString()
                            };

                            lectures.Add(lecture);
                        }
                    }
                }
            }
            return lectures;
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
                                var lecture = new Category
                                {
                                    CategoryId = sqlReader.GetInt32(0),
                                    CategoryName = sqlReader.GetString(1)
                                };

                                list.Add(lecture);
                            };
                        }
                    }
                }

                var model = new Lectures();
                model.CategoryList = list;

                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }


        [HttpPost]
        public ActionResult Create(Lectures lecture, string CategoryList)
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

                    using (var sqlCommand = new SqlCommand(INSERT_NEW_LECTURE, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@CategoryId", CategoryList);

                        sqlCommand.Parameters.AddWithValue("@Lecture", lecture.Lecture);

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