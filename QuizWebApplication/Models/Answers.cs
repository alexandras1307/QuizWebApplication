using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizWebApplication.Models
{
    public class Answers
    {
        public int UserId { get; set; }

        public string QuestionText { get; set; }

        public int QuestionId { get; set; }

        public string QuestionOption1 { get; set; }

        public string QuestionOption2 { get; set; }

        public string QuestionOption3 { get; set; }

        public string QuestionOption4 { get; set; }

        public string CorrectOption { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Answer { get; set; }

        public List<Answers> Grade { get; set; }

    }
}