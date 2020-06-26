using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizWebApplication.Models
{
    public class QuizResult
    {
        public List<Answers> Questions { get; set; } 

        public decimal Grade { get; set; }
    }
}