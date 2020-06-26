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

        public QuizCategoryQuestionClass Question { get; set;  }

        // public List<Answers> Grade { get; set; }

    }
}