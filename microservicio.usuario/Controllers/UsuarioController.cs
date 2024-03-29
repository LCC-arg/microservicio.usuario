﻿using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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


        [HttpPost("login")]
        [ProducesResponseType(typeof(UsuarioTokenResponse), 200)]
        public IActionResult LoginAuth(UsuariLoginRequest userLogin)
        {
            var userResponse = _usuarioService.Authenticacion(userLogin);

            if(userResponse == null) { return BadRequest(new BadRequest { message="usuario invalido"}); };

            return Ok(userResponse);
        }


        /// <summary>
        /// devuelve un usuario
        /// </summary>
        [Authorize]
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
                return NotFound(new BadRequest { message = "No se encontraron Usuarios" });
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
            catch (ExistingMailException e) 
            {
                return new JsonResult(new BadRequest { message = e.Message }) { StatusCode = 409};
            }catch(PasswordFormatException e)
            {
                return new JsonResult(new BadRequest { message = e.Message }) { StatusCode = 409 };
            }
            catch (Exception e)
            {
                return new JsonResult(new BadRequest { message = "puede que existan campos invalidos" }) { StatusCode = 400};
            }
            
            return new JsonResult(result);
        }

        /// <summary>
        /// modifica un usuario existente
        /// </summary>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(UsuarioResponse), 200)]
        public IActionResult UpdateUsuario(Guid usuarioId, UsuarioRequest request)
        {
            UsuarioResponse result = null;

            try
            {
                result = _usuarioService.UpdateUsuario(usuarioId, request);
            }
            catch (InvalidDataException e)
            {
                return new JsonResult(new BadRequest { message = "algun dato es incorrecto" }) { StatusCode = 400 };
            }
            
            return new JsonResult(result) { StatusCode = 200};
        }

        /// <summary>
        /// elimina un usuario existente
        /// </summary>
        [Authorize]
        [HttpDelete("{usuarioId}")]
        [ProducesResponseType(typeof(UsuarioResponse), 200)]
        public IActionResult DeleteUsuario(Guid usuarioId)
        {
            var exist =  _usuarioService.GetUsuarioById(usuarioId);

            if (exist != null)
            {
                var result = _usuarioService.RemoveUsuario(usuarioId);
                return new JsonResult(result) { StatusCode = 200};
            }

            return new JsonResult(new BadRequest { message = "ese usuario no existe" });
        }
    }
}
