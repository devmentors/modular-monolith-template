using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Modular.Modules.First.Api.Controllers
{
    [Route("[controller]")]
    internal class FirstController : Controller
    {
        [SwaggerOperation("Get first")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<string> Get() => Ok("First module");
    }
}