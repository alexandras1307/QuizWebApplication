using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace QuizWebApplication.Models
{
    public class Grades
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string CategoryName { get; set; }

        public decimal? Grade { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        // public List<FinalGrades> Finals {get; set;}

        public decimal? FinalGrade { get; set; }

        public DateTime TimeStamp { get; set; }

    }
}