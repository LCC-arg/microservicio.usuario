using Application.Interfaces;
using Application.Request;
using Application.Response;
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
        [ProducesResponseType(typeof(UsuarioResponse), 201)]
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
        [ProducesResponseType(typeof(UsuarioResponse), 201)]
        public IActionResult CreateUsuario(UsuarioRequest request)
        {
            var result = _usuarioService.CreateUsuario(request);
            return new JsonResult(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(UsuarioResponse), 200)]
        public IActionResult UpdateUsuario(Guid usuarioId, UsuarioRequest request)
        {
            var result = _usuarioService.UpdateUsuario(usuarioId, request);
            return new JsonResult(result);
        }

        [HttpDelete("{usuarioId}")]
        [ProducesResponseType(typeof(UsuarioResponse), 200)]
        public IActionResult DeleteUsuario(Guid usuarioId)
        {
            var result = _usuarioService.RemoveUsuario(usuarioId);
            return new JsonResult(result);
        }
    }
}
