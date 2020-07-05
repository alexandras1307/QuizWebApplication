using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizWebApplication.Models
{
    public class FinalGrades
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal? FinalGrade { get; set; }

        public DateTime TimeStamp { get; set;  }
    }
}