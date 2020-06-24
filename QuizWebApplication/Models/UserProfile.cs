using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizWebApplication.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter your name!")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter your name!")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter your email!")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please choose your password!")]
        public string Password { get; set; }

        [Display(Name = "Please re-enter your password")]
        [Required(ErrorMessage = "Please re-enter your password!")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        //[Display(Name = "Please enter your name")]
        //[Required(ErrorMessage = "Please enter your name!")]
        public bool Active { get; set; }
    }
}