using Newmes.Models;
using System.Collections.Generic;

namespace Newmes.Api.Models
{
    public class TestSubmitDto
    {
        public string HallTicketNumber { get; set; }
        public string CandidateName { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
