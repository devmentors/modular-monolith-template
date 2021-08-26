using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Modular.Modules.Third.Api.Controllers
{
    [Route("[controller]")]
    internal class ThirdController : Controller
    {
        [SwaggerOperation("Get third")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<string> Get() => Ok("Third module");
    }
}