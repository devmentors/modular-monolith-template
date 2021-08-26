using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Modular.Modules.Second.Api.Controllers
{
    [Route("[controller]")]
    internal class SecondController : Controller
    {
        [SwaggerOperation("Get second")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<string> Get() => Ok("Second module");
    }
}