using Application.Interfaces;
using Application.Request;
using Application.Response;
using Application.UseCase.Tokens;
using Domain.Entities;
using microservicio.usuario.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class UsuarioControllerTest
    {
        //test de autenticacion

        [Fact]
        public void LoginAuthOk()
        {
            //ARRANGE
            string firma = "unajproyectodesoftwareclaveparatokenjwt12334estoessolounaprueba2";
            var tokenService = new TokenService(firma);
            var mockUsuarioService = new Mock<IUsuarioService>();
            var controller = new UsuarioController(mockUsuarioService.Object);

            var usuarioLogin = new UsuariLoginRequest
            {
                email = "mariano@hotmail.com",
                password = "secretPassword123*"
            };

            var usuario = new Usuario
            {
                Email = usuarioLogin.email,
                Nombre = "Mario",
                UsuarioId = Guid.NewGuid()
            };

            var usuarioTokenResponse = tokenService.GenerateToken(usuario);
            

            mockUsuarioService.Setup(us => us.Authenticacion(It.IsAny<UsuariLoginRequest>())).Returns(usuarioTokenResponse);

            //ACT
            var result = controller.LoginAuth(usuarioLogin);

            //ASSERT

            Assert.IsType<OkObjectResult>(result);
            var okResponse = result as OkObjectResult;
            var authResponse = okResponse.Value as UsuarioTokenResponse;

            Assert.NotNull(authResponse);
            Assert.Equal(authResponse.Token,usuarioTokenResponse.Token);
            Assert.Equal(authResponse.UsuarioId, usuario.UsuarioId);

            //decoficar token
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(authResponse.Token);

            var uniqueNameClaim = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName);
            Assert.Equal(usuario.UsuarioId.ToString(), uniqueNameClaim?.Value);

            var nameClaim = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name);
            Assert.Equal(usuario.Nombre, nameClaim?.Value);

            var emailClaim = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email);
            Assert.Equal(usuario.Email, emailClaim?.Value);

        }



        [Fact]
        public void LoginAuthNulo()
        {
            //ARRANGE
            string firma = "unajproyectodesoftwareclaveparatokenjwt12334estoessolounaprueba2";
            var tokenService = new TokenService(firma);
            var mockUsuarioService = new Mock<IUsuarioService>();
            var controller = new UsuarioController(mockUsuarioService.Object);

            var usuarioLogin = new UsuariLoginRequest
            {
                email = "mariano@hotmail.com",
                password = "secretPassword123*"
            };

            var expectedMessage = "usuario invalido";

            mockUsuarioService.Setup(us => us.Authenticacion(It.IsAny<UsuariLoginRequest>())).Returns((UsuarioTokenResponse)null);

            //ACT
            var result = controller.LoginAuth(usuarioLogin);

            //ASSERT
            Assert.IsType<BadRequestObjectResult>(result);

            var badRequest = result as BadRequestObjectResult;
            var requestBad = badRequest?.Value as BadRequest;

            Assert.Equal(requestBad?.message, expectedMessage);
                    
        }


        //test de GetUsuarioById
        [Fact]
        public void GetUusarioByIdOk()
        {
            //ARRANGE
            var mockUsuarioService = new Mock<IUsuarioService>();
            var controller = new UsuarioController(mockUsuarioService.Object);

            var idUsuarioValido = Guid.NewGuid().ToString();

            var usuarioFalsoResponse = new UsuarioResponse
            {
                usuarioId = new Guid(idUsuarioValido),
                Nombre = "Mario",
                Apellido = "Riquelme",
                Dni = "34456766"
            };

            mockUsuarioService.Setup(us => us.GetUsuarioById(It.IsAny<Guid>())).Returns(usuarioFalsoResponse);

            //ACT
            var result = controller.GetUsuarioById(idUsuarioValido);

            //ASSERT
            Assert.IsType<JsonResult>(result);

            var jsonResult = result as JsonResult;
            var response = jsonResult?.Value as UsuarioResponse;

            Assert.Equal(response.Nombre,usuarioFalsoResponse.Nombre);
            Assert.Equal(response.Apellido,usuarioFalsoResponse.Apellido);
            Assert.Equal(response.Dni,usuarioFalsoResponse.Dni);
            Assert.Equal(response.usuarioId,usuarioFalsoResponse.usuarioId);

        }
    }
}
