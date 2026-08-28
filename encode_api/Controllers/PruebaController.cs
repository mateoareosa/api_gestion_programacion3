using Microsoft.AspNetCore.Mvc;

namespace encode_api.Controllers
{
    [ApiController]
    [Route("api/prueba")]
    public class PruebaController : ControllerBase
    {
        [HttpGet]
        public IActionResult ObtenerMensaje()
        {
            return Ok("La API está funcionando correctamente.");
        }
    }
}
