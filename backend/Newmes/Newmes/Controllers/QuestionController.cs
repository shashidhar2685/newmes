using System.Web.Http;
using Newmes.Models;
using Newmes.Repository;
using System.Web.Http.Cors;

namespace Newmes.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/question")]
    public class QuestionController : ApiController
    {
        private readonly QuestionRepository _repo = new QuestionRepository();

        // ✅ GET: All Questions
        [HttpGet]
        [Route("getall")]
        public IHttpActionResult GetAllQuestions()
        {
            var questions = _repo.GetAll();
            return Ok(questions);
        }

        // ✅ GET: Question by ID
        [HttpGet]
        [Route("get/{id}")]
        public IHttpActionResult GetQuestionById(int id)
        {
            var question = _repo.GetById(id);
            if (question == null)
                return NotFound();

            return Ok(question);
        }

        // ✅ POST: Add new question
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddQuestion(Question question)
        {
            bool isSuccess = _repo.Add(question);
            if (isSuccess)
                return Ok("Question added successfully.");
            else
                return BadRequest("Failed to add question.");
        }

        // ✅ POST: Update question
        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateQuestion(Question question)
        {
            bool isSuccess = _repo.Update(question);
            if (isSuccess)
                return Ok("Question updated successfully.");
            else
                return BadRequest("Failed to update question.");
        }

        // ✅ DELETE: Delete question by ID
        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult DeleteQuestion(int id)
        {
            bool isSuccess = _repo.Delete(id);
            if (isSuccess)
                return Ok("Question deleted successfully.");
            else
                return BadRequest("Failed to delete question.");
        }
    }
}
