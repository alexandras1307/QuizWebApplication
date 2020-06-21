using QuizWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizWebApplication.Controllers
{
    public class StudentRegistrationController : Controller
    {
        // GET: StudentRegistration
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(StudentRegistration studentRegistrationObject)
        {
            string connection = "Data Source=localhost;Initial Catalog=QuizApplicationDatabase;Integrated Security=True";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                string sqlQuery = "insert into StudentRegistration(StudentName, StudentEmail, Password, ConfirmPassword) values ('" + studentRegistrationObject.StudentName + "' , '" + studentRegistrationObject.StudentEmail + "' , '" + studentRegistrationObject.Password + "' , '" + studentRegistrationObject.ConfirmPassword + "')";
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    if (ModelState.IsValid)
                    {
                        using (SqlCommand command = new SqlCommand("CreateStudentProfile", sqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure
                        })
                        {
                            //sqlConnection.Open();
                            command.ExecuteNonQuery();
                        }
                        return RedirectToAction("SuccessfulRegistrationStudent");
                    }
                    sqlConnection.Close();
                }              
                return View(studentRegistrationObject);
            }
        }

        public ActionResult SuccessfulRegistrationStudent()
        {
            return View();
        }

        public ActionResult Details(string studentId)
        {
            List<StudentRegistration> details = new List<StudentRegistration>();

            string connection = "Data Source=localhost;Initial Catalog=QuizApplicationDatabase;Integrated Security=True";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                var sqlQuery = $"SELECT * FROM StudentRegistration where StudentId = { studentId }";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        StudentRegistration student = new StudentRegistration();
                        student.StudentId = int.Parse(studentId);
                        student.StudentName = sqlDataReader["StudentName"].ToString();
                        student.StudentEmail = sqlDataReader["StudentEmail"].ToString();
                        details.Add(student);
                    }

                    sqlCommand.Dispose();
                    sqlConnection.Close();
                }
            }
            // return RedirectToAction("Take");
            return View(details);
        }
    }
}