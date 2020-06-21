﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizWebApplication.Models
{
    public class TeacherRegistrationClass
    {
        [Key]
        public int TeacherId { get; set; }

        [Display(Name = "Please enter your name")]
        [Required(ErrorMessage = "Please enter your name!")]
        public string TeacherName { get; set; }

        [Display(Name = "Please enter your email")]
        [Required(ErrorMessage = "Please enter your email!")]
        public string TeacherEmail { get; set; }

        [Display(Name = "Please choose your password")]
        [Required(ErrorMessage = "Please choose your password!")]
        public string Password { get; set; }

        [Display(Name = "Please re-enter your password")]
        [Required(ErrorMessage = "Please re-enter your password!")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        //[Display(Name = "Please enter your name")]
        //[Required(ErrorMessage = "Please enter your name!")]
        public int IsActive { get; set; }
    }
}