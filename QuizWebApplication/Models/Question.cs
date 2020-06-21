using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizWebApplication.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Display(Name = "Select Category")]
        [Required(ErrorMessage = "Select Category!")]
        public int CategoryId { get; set; }

        [Display(Name = "Question")]
        [Required(ErrorMessage = "Question!")]
        public string QuestionText { get; set; }

        [Display(Name = "Option1")]
        [Required(ErrorMessage = "Option1!")]
        public string QuestionOption1 { get; set; }

        [Display(Name = "Option2")]
        [Required(ErrorMessage = "Option2!")]
        public string QuestionOption2 { get; set; }

        [Display(Name = "Option3")]
        [Required(ErrorMessage = "Option4!")]
        public string QuestionOption3 { get; set; }

        [Display(Name = "Option4")]
        [Required(ErrorMessage = "Option4!")]
        public string QuestionOption4 { get; set; }

        [Display(Name = "Correct answer")]
        [Required(ErrorMessage = "Correct answer")]
        public string CorrectOption { get; set; }

        [Display(Name = "Select Category")]
        public List<Category> CategoryList { get; set; }
    }
}