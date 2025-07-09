using System.Web.Http;
using System.Web.Http.Cors;
using Newmes.Repository;

namespace Newmes.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly CandidateRepository _repo = new CandidateRepository();

        // POST api/auth/verifylogin
        [HttpPost]
        [Route("verifylogin")]
        public IHttpActionResult VerifyLogin(LoginRequestDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.HallTicketNo) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Hall‑ticket and password required.");

            var candidate = _repo.VerifyLogin(dto.HallTicketNo, dto.Password);

            if (candidate == null)
                return Unauthorized();          // HTTP 401

            return Ok(new
            {
                fullName = candidate.FullName,      // lower‑case f for React
                hallTicketNo = candidate.HallTicketNo
            });
        }
    }

    public class LoginRequestDto
    {
        public string HallTicketNo { get; set; }
        public string Password { get; set; }
    }
}
