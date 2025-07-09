using System;
using System.Web.Http;
using Newmes.Models;
using Newmes.Repository;
using System.Web.Http.Cors;

namespace Newmes.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/admitcard")]
    public class AdmitCardController : ApiController
    {
        AdmitCardRepository repo = new AdmitCardRepository();

        [HttpPost]
        [Route("generate")]
        public IHttpActionResult GenerateAdmitCard([FromBody] AdmitCardModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Invalid data.");

                var result = repo.GenerateAdmitCard(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
