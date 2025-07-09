using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newmes.Models
{
    /// <summary>
    /// DTO returned to the candidate during an online test.
    /// Does NOT expose CorrectAnswer or any admin‑only fields.
    /// </summary>
    public class QuestionForTestDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
    }
}