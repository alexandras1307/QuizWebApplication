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
    }
}