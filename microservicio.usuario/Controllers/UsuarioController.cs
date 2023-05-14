using Application.Interfaces;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

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


        /// <summary>
        /// devuelve un usuario
        /// </summary>
        [HttpGet("{usuarioId}")]
        [ProducesResponseType(typeof(UsuarioResponse), 201)]
        public IActionResult GetUsuarioById(string usuarioId)
        {
            Guid usuarioIdBuscar = new Guid();
            
            if (!Guid.TryParse(usuarioId, out usuarioIdBuscar))
            {
                return new JsonResult(new BadRequest { message = "el formato del id no es valido"});
            }

            var result = _usuarioService.GetUsuarioById(usuarioIdBuscar);

            if (result == null)
            {
                return NotFound(new { message = "No se encontraron Usuarios" });
            }

            return new JsonResult(result);
        }

        /// <summary>
        /// crea un usuario nuevo
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(UsuarioResponse), 201)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public IActionResult CreateUsuario(UsuarioRequest request)
        {
            UsuarioResponse result = null;

            try
            {
                result = _usuarioService.CreateUsuario(request);
            }
            catch (Exception e)
            {
                return new JsonResult(new BadRequest { message = "algun campo no es valido" }) { StatusCode = 400};
            }
            
            return new JsonResult(result);
        }

        /// <summary>
        /// modifica un usuario existente
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(UsuarioResponse), 200)]
        public IActionResult UpdateUsuario(Guid usuarioId, UsuarioRequest request)
        {
            var result = _usuarioService.UpdateUsuario(usuarioId, request);
            return new JsonResult(result);
        }

        /// <summary>
        /// elimina un usuario existente
        /// </summary>
        [HttpDelete("{usuarioId}")]
        [ProducesResponseType(typeof(UsuarioResponse), 200)]
        public IActionResult DeleteUsuario(Guid usuarioId)
        {
            var result = _usuarioService.RemoveUsuario(usuarioId);
            return new JsonResult(result);
        }
    }
}
