using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizWebApplication.Models
{
    public class TeacherLogin
    {
        [Display(Name = "Please enter your email")]
        [Required(ErrorMessage = "Please enter your email!")]
        public string TeacherEmail { get; set; }

        [Display(Name = "Please choose your password")]
        [Required(ErrorMessage = "Please choose your password!")]
        public string Password { get; set; }
    }
}