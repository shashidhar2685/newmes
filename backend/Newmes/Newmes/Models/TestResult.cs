using System;

namespace Newmes.Api.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public string HallTicketNumber { get; set; }
        public string CandidateName { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public int NotAttempted { get; set; }
        public int Score { get; set; }
        public DateTime TestDate { get; set; }
    }
}
