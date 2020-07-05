using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizWebApplication.Models
{
    public class Module
    {
        public Category Category { get; set; }
        public List<Question> Questions { get; set; }
        public List<Lectures> Lectures { get; set; }
    }
}