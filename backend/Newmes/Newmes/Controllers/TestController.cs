using System.Web.Http;
using System.Web.Http.Cors;
using Newmes.Api.Models;
using Newmes.Api.Repositories;
using Newmes.Api.Services;   // ✅ Required for EvaluationService

namespace Newmes.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        private readonly TestResultRepository _repo = new TestResultRepository();

        // POST: api/test/submitresult
        [HttpPost]
        [Route("submitresult")]
        public IHttpActionResult SubmitResult(TestSubmitDto dto)
        {
            if (dto == null || dto.Answers == null || dto.Answers.Count == 0)
                return BadRequest("Invalid or empty test submission.");

            // ✅ Evaluate answers (use service)
            var evaluation = EvaluationService.Evaluate(dto);

            var result = new TestResult
            {
                HallTicketNumber = dto.HallTicketNumber,
                CandidateName = dto.CandidateName,
                TotalQuestions = evaluation.Total,
                CorrectAnswers = evaluation.Correct,
                WrongAnswers = evaluation.Wrong,
                NotAttempted = evaluation.NotAttempted,
                Score = evaluation.Score
            };

            // ✅ Save to DB
            _repo.Save(result);

            // ✅ Return result to frontend
            return Ok(result);
        }
    }
}
