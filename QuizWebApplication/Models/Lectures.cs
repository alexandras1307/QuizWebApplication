using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizWebApplication.Models
{
    public class Lectures
    {
        [Key]
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Lecture { get; set; }

        [Display(Name = "Select Category")]
        public List<Category> CategoryList { get; set; }

    }
}