using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newmes.Models
{
    public class AnswerDto
    {
        public int QuestionId { get; set; }
        public string SelectedOption { get; set; }   // 0 ⇒ not attempted
    }
}