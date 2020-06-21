using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QuizWebApplication.Models
{
    public class CategoryDataAccess
    {
        string connectionString = "Data Source=localhost;Initial Catalog=QuizApplicationDatabase;Integrated Security=True";

        //To Update a particular category  
        public void UpdateCategory(Category category)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        ////Get the details of a particular category  
        //public Category GetCategoryDetails(int? id)
        //{
        //    QuizCategoryQuestionClass category = new QuizCategoryQuestionClass();

        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("spDetailsCategory", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {
        //            category.CategoryName = rdr["Category"].ToString();
        //            category.QuestionText = rdr["Question"].ToString();
        //            category.QuestionOption1 = rdr["Option1"].ToString();
        //            category.QuestionOption2 = rdr["Option2"].ToString();
        //            category.QuestionOption3 = rdr["Option3"].ToString();
        //            category.QuestionOption4 = rdr["Option4"].ToString();
        //        }
        //    }
        //    return category;
        //}

        //To Delete the record on a particular category  
        public void DeleteCategory(int? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoryId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}