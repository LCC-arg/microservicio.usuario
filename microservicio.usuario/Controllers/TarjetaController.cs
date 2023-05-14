using Application.Interfaces;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;

namespace agencia_de_viajes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
        private readonly ITarjetaService _tarjetaService;

        public TarjetaController(ITarjetaService tarjetaService)
        {
            _tarjetaService = tarjetaService;
        }


        /// <summary>
        /// crea una tarjeta nueva asociada a un usuario
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TarjetaResponse), 201)]
        [ProducesResponseType(typeof(BadRequest), 404)]     
        public IActionResult CreateTarjeta(TarjetaRequest request)
        {
            TarjetaResponse? result = null;

            try
            {
                result = _tarjetaService.CreateTarjeta(request);
            }
            catch (Exception e)
            {
                return new JsonResult(new BadRequest { message = "error al registrar la tarjeta" }) { StatusCode = 404};
            }
            
            
            if (result == null)
            {
                return new JsonResult(new BadRequest { message = "no se creo la tarjeta" });
            }
            
            return new JsonResult(result);
        }

        /// <summary>
        /// devuelve todas las tarjetas de un usuario especifico
        /// </summary>
        [HttpGet("{usuarioId}")]
        [ProducesResponseType(typeof(TarjetasUsuarioResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public IActionResult GetTarjetaUsuario(string usuarioId)
        {
            Guid usuarioIdBuscar = new Guid();

            if (!Guid.TryParse(usuarioId, out usuarioIdBuscar))
            {
                return new JsonResult(new BadRequest { message = "el formato del id no es valido" });
            }

            TarjetasUsuarioResponse result = _tarjetaService.GetUsuarioTarjetas(usuarioIdBuscar);

            if (result == null)
            {
                return new JsonResult(new BadRequest { message = "no hemos encontrado nada" }) { StatusCode = 404 };
            }

            return new JsonResult(result);
        }
    }
}
