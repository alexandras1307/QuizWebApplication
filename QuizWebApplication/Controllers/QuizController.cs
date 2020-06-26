﻿using QuizWebApplication.Models;
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
        private const string INSERT_USER_TAKEN_QUIZ = "InsertUserTakenQuiz";
        private const string GET_ANSWERS_BY_STUDENT_CATEGORY = "GetAnswersByStudentAndCategory";
        private const string INSERT_GRADE = "InsertNewGrade";

        public ActionResult Index(string CategoryList)
        {

            if (!string.IsNullOrWhiteSpace(CategoryList))
            {
                return RedirectToAction("Take", new { categoryList = CategoryList });
            }

            var list = new List<Category>();
            using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
            {

                using (var sqlCommand = new SqlCommand(GET_UNSOLVED_QUIZ, sqlConnection))
                {
                    sqlConnection.Open();

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserId", Session["UserId"]);

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
            // var categoryId = 0;
            //if (!int.TryParse(categoryList, out categoryId))
            //{
            //    return View("Error");
            //}

            List<Answers> quizList = new List<Answers>();

            var quiz = new QuizCategoryQuestionClass();
            // quiz.Questions = new List<Answers>();
            // quiz.CategoryId = categoryId;
            using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
            {

                using (SqlCommand sqlCommand = new SqlCommand(GET_ALL_QUESTIONS_BY_CATEGORY, sqlConnection))
                {
                    sqlConnection.Open();

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CategoryId", categoryList);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        while (sqlDataReader.Read())
                        {
                            quizList.Add(
                            new Answers
                            {
                                Question = new QuizCategoryQuestionClass()
                                {
                                    CategoryId = int.Parse(categoryList),
                                    Answer = "",
                                    CategoryName = sqlDataReader["CategoryName"].ToString(),
                                    QuestionId = (int)sqlDataReader["QuestionId"],
                                    QuestionText = sqlDataReader["QuestionText"].ToString(),
                                    QuestionOption1 = sqlDataReader["QuestionOption1"].ToString(),
                                    QuestionOption2 = sqlDataReader["QuestionOption2"].ToString(),
                                    QuestionOption3 = sqlDataReader["QuestionOption3"].ToString(),
                                    QuestionOption4 = sqlDataReader["QuestionOption4"].ToString()
                                }
                            });
                        }

                    }
                }



                using (SqlCommand sqlCommand = new SqlCommand(INSERT_USER_TAKEN_QUIZ, sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", Session["UserId"]);
                    sqlCommand.Parameters.AddWithValue("@CategoryId", categoryList);

                    sqlCommand.ExecuteNonQuery();
                }



            }

            return View(quizList);
        }

        [HttpPost]
        public ActionResult Take(List<Answers> model)
        {

            using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
            {
                sqlConnection.Open();
                foreach (var answer in model)
                {
                    using (SqlCommand sqlCommand = new SqlCommand(INSERT_NEW_ANSWER, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.AddWithValue("@UserId", Session["UserId"]);
                        sqlCommand.Parameters.AddWithValue("@QuestionId", answer.Question.QuestionId);
                        sqlCommand.Parameters.AddWithValue("@Answer", answer.Question.Answer);

                        sqlCommand.ExecuteNonQuery();
                    }

                }
            }

            return RedirectToAction("Done", new { categoryId = model.First().Question.CategoryId, userId = Session["UserId"] });
        }

        public ActionResult Done(int categoryId, int userId)
        {

            var answers = new List<Answers>();
            // List<QuizCategoryQuestionClass> quizList = new List<QuizCategoryQuestionClass>();

            // var quiz = new QuizCategoryQuestionClass();

            using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
            {

                using (SqlCommand sqlCommand = new SqlCommand(GET_ANSWERS_BY_STUDENT_CATEGORY, sqlConnection))
                {
                    sqlConnection.Open();

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CategoryId", categoryId);
                    sqlCommand.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        while (sqlDataReader.Read())
                        {
                            answers.Add(
                            new Answers
                            {
                                UserId = (int)sqlDataReader["UserId"],
                                Question = new QuizCategoryQuestionClass()
                                {
                                    Answer = sqlDataReader["Answer"].ToString(),
                                    QuestionText = sqlDataReader["QuestionText"].ToString(),
                                    CorrectOption = sqlDataReader["CorrectOption"].ToString(),
                                    QuestionId = (int)sqlDataReader["QuestionId"],
                                    CategoryId = (int)sqlDataReader["CategoryId"],
                                    CategoryName = sqlDataReader["CategoryName"].ToString()
                                }
                            });
                        }
                    }
                }
            }

            float correctAnswers = 0;

            foreach (var item in answers)
            {
                if (item.Question.Answer == item.Question.CorrectOption)
                {
                    correctAnswers++;
                }
            }

            var result = new QuizResult()
            {
                Grade = Convert.ToDecimal(correctAnswers / answers.Count() * 100.0),
                Questions = answers
            };

            using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
            {
                sqlConnection.Open();
                foreach (var answer in answers)
                {
                    using (SqlCommand sqlCommand = new SqlCommand(INSERT_GRADE, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.AddWithValue("@UserId", Session["UserId"]);
                        sqlCommand.Parameters.AddWithValue("@CategoryId", answer.Question.QuestionId);
                        sqlCommand.Parameters.AddWithValue("@Grade", result.Grade);

                        sqlCommand.ExecuteNonQuery();
                    }

                }
            }

            return View(result);
        }
    }
}