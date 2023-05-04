using Application.Interfaces;
using Application.Request;
using Microsoft.AspNetCore.Mvc;

namespace microservicio.usuario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("{usuarioId}")]
        public IActionResult GetUsuarioById(Guid usuarioId)
        {
            var result = _usuarioService.GetUsuarioById(usuarioId);

            if (result == null)
            {
                return NotFound(new { message = "No se encontraron Usuarios" });
            }

            return new JsonResult(result);
        }

        [HttpPost]
        public IActionResult CreateUsuario(UsuarioRequest request)
        {
            var result = _usuarioService.CreateUsuario(request);
            return new JsonResult(result);
        }

        [HttpPut]
        public IActionResult UpdateUsuario(Guid usuarioId, UsuarioRequest request)
        {
            var result = _usuarioService.UpdateUsuario(usuarioId, request);
            return new JsonResult(result);
        }

        [HttpDelete("{usuarioId}")]
        public IActionResult DeleteUsuario(Guid usuarioId)
        {
            var result = _usuarioService.RemoveUsuario(usuarioId);
            return new JsonResult(result);
        }
    }
}
