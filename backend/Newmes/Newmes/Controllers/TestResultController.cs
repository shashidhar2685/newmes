using System.Web.Http;
using System.Web.Http.Cors;
using Newmes.Api.Repositories;

namespace Newmes.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/testresult")]
    public class TestResultController : ApiController
    {
        private readonly TestResultRepository _repo = new TestResultRepository();

        // ✅ Full result list (you already had this)
        [HttpGet]
        [Route("list")]
        public IHttpActionResult List()
        {
            var rows = _repo.GetAll();
            return Ok(rows);
        }

        // ✅ NEW: Ranking by Score (highest first)
        [HttpGet]
        [Route("ranking")]
        public IHttpActionResult Ranking()
        {
            var rows = _repo.GetRankedResults();
            return Ok(rows);
        }
    }
}
