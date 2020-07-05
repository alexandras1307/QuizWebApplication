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
    public class CatalogController : Controller
    {
        private readonly string DbConnection = ConfigurationManager.ConnectionStrings["QuizApplicationDatabase"].ConnectionString;
        private const string GET_ALL_STUDENTS = "GetAllStudents";
        private const string GET_GRADES = "GetGrades";

        // GET: Catalog
        public ActionResult Index()
        {

            var list = new List<UserProfile>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
                {

                    sqlConnection.Open();

                    using (var sqlCommand = new SqlCommand(GET_ALL_STUDENTS, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        using (var sqlReader = sqlCommand.ExecuteReader())
                        {

                            while (sqlReader.Read())
                            {
                                var student = new UserProfile
                                {
                                    FirstName = sqlReader.GetString(0),
                                    LastName = sqlReader.GetString(1),
                                    Email = sqlReader.GetString(2),
                                    UserId = sqlReader.GetInt32(3)

                                };

                                list.Add(student);
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

        [HttpGet]
        public ActionResult Details(int userId)
        {
            var grades = new List<Grades>();

            using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
            {
                //try
                //{


                using (SqlCommand sqlCommand = new SqlCommand(GET_GRADES, sqlConnection))
                {
                    sqlConnection.Open();

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        while (sqlDataReader.Read())
                        {
                            var grade = new Grades();
                            
                                if (!sqlDataReader.IsDBNull(3))
                                    grade.Grade = Convert.ToDecimal(sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("Grade")));
                                else
                                {
                                    grade.Grade = 0;
                                }

                            if (!sqlDataReader.IsDBNull(4))
                                grade.CategoryName = sqlDataReader["CategoryName"].ToString();
                            else
                            {
                                grade.CategoryName = "No grades yet";
                            };

                            grade.FirstName = sqlDataReader["FirstName"].ToString();
                            grade.LastName = sqlDataReader["LastName"].ToString();
                            grade.UserId = (int)sqlDataReader["UserId"];
                            //if (!sqlDataReader.IsDBNull(3))
                            //{
                            //    grade.CategoryName = "test";
                            //}
                            //if (!sqlDataReader.IsDBNull(4))
                            //{
                            //    grade.Grade
                            //}
                            grades.Add(grade);

                        }

                    }
                };
            }
         return View(grades);
        }
    }
}

    //catch (Exception ex)
    //{
    //    Console.WriteLine(ex.Message);
    //    return View("Error");
    //}

           