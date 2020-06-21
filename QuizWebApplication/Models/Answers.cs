using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizWebApplication.Models
{
    public class Answers
    {
        [Key]
        public int AnswerId { get; set; }

        public int QuestionId { get; set; }

        public int StudentId { get; set; }

        public string Answer { get; set; }
    }
}