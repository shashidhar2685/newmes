using System.Collections.Generic;

namespace Newmes.Models
{
    public class TestResultDto
    {
        public string HallTicket { get; set; }

        // List of answers: { QuestionId, SelectedOption }
        public List<AnswerDto> Answers { get; set; }
    }

    //public class AnswerDto
    //{
    //    public int QuestionId { get; set; }
    //    public string SelectedOption { get; set; }
    //}

    public class TestScoreDto
    {
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public int Score { get; set; }
    }
}
