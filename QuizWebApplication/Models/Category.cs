using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QuizWebApplication.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "Category name")]
        [Required(ErrorMessage = "Category name!")]
        public string CategoryName { get; set; }

        string connectionString = "Put Your Connection string here";

        //public IEnumerable<Category> GetAllCategories()
        //{
        //    List<Category> categories = new List<Category>();

        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("spGetAllEmployees", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {
        //            Category category = new Category();

        //            category.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
        //            category.CategoryName = rdr["CategoryName"].ToString();

        //            categories.Add(category);
        //        }
        //        con.Close();
        //    }
        //    return categories;
        //}      

        ////To Update the records of a particluar employee  
        //public void UpdateCategory(Category category)
        //{
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("spUpdateCategory", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@CategoryId", category.CategoryId);
        //        cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}

        ////Get the details of a particular employee  
        //public Category GetCategoryDetails(string categoryId)
        //{
        //    QuizCategoryQuestionClass category = new QuizCategoryQuestionClass();

        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        string sqlQuery = $"SELECT * FROM QuizQuestion QQ JOIN QuizCategory QC ON QQ.CategoryId = QC.CategoryId WHERE QQ.CategoryId = { categoryId }";
        //        SqlCommand cmd = new SqlCommand(sqlQuery, con);

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {
        //            category.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
        //            category.CategoryName = rdr["CategoryName"].ToString();
        //            category.QuestionText = rdr["QuestionText"].ToString();
        //        }
        //    }
        //    return category;
        //}

        ////To Delete the record on a particular employee  
        //public void DeleteCategory(string categoryId)
        //{

        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("spDeleteCategory", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@CategoryId", categoryId);

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}
    }
}