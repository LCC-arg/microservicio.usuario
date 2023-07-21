using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Application.UseCase.Tokens;
using Domain.Entities;
using microservicio.usuario.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.IdentityModel.Tokens.Jwt;

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
            Assert.Equal(authResponse.Token, usuarioTokenResponse.Token);
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

            Assert.Equal(response.Nombre, usuarioFalsoResponse.Nombre);
            Assert.Equal(response.Apellido, usuarioFalsoResponse.Apellido);
            Assert.Equal(response.Dni, usuarioFalsoResponse.Dni);
            Assert.Equal(response.usuarioId, usuarioFalsoResponse.usuarioId);
        }


        [Fact]
        public void GetUusuarioByIdNulo()
        {
            //ARRANGE
            var mockUsuarioService = new Mock<IUsuarioService>();
            var controller = new UsuarioController(mockUsuarioService.Object);

            var idUsuarioValido = Guid.NewGuid().ToString();
            var expectedMessage = "No se encontraron Usuarios";

            mockUsuarioService.Setup(us => us.GetUsuarioById(It.IsAny<Guid>())).Returns((UsuarioResponse)null);

            //ACT
            var result = controller.GetUsuarioById(idUsuarioValido);

            //ASSERT
            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundObject = result as NotFoundObjectResult;
            var response = notFoundObject.Value as BadRequest;

            Assert.Equal(expectedMessage, response.message);

        }


        [Fact]
        public void GetUsuarioByIdFormatoInvalido()
        {
            //ARRANGE
            var mockUsuarioService = new Mock<IUsuarioService>();
            var controller = new UsuarioController(mockUsuarioService.Object);

            var idUsuarioInvalido = "este id es invalido";
            var expectedMessage = "el formato del id no es valido";

            //ACT
            var result = controller.GetUsuarioById(idUsuarioInvalido);

            //ASSERT
            Assert.IsType<JsonResult>(result);
            var jsonResult = result as JsonResult;
            var response = jsonResult.Value as BadRequest;

            Assert.Equal(expectedMessage, response.message);
        }

        //test de CreateUsuario

        [Fact]
        public void CreateUsuarioOk()
        {
            //ARRANGE
            var mockUsuarioService = new Mock<IUsuarioService>();
            var controller = new UsuarioController(mockUsuarioService.Object);

            var usuarioRequest = new UsuarioRequest
            {
                Nombre = "Mario",
                Apellido = "Correa",
                Dni = "34456432",
                Domicilio = "nueva delhi 1234 , zeballos",
                Email = "mario@gmail.com",
                FechaNac = new DateTime(1994, 11, 3),
                Nacionalidad = "paraguay",
                Password = "secretPassword123*",
                Telefono = "11 4565 4556"
            };

            var usuario = new UsuarioResponse
            {
                Nombre = usuarioRequest.Nombre,
                Apellido = usuarioRequest.Apellido,
                Dni = usuarioRequest.Dni,
                usuarioId = Guid.NewGuid(),
            };

            mockUsuarioService.Setup(us => us.CreateUsuario(It.IsAny<UsuarioRequest>())).Returns(usuario);

            //ACT
            var result = controller.CreateUsuario(usuarioRequest);

            //ASSERT
            Assert.IsType<JsonResult>(result);
            var jsonResult = result as JsonResult;
            var response = jsonResult.Value as UsuarioResponse;

            Assert.Equal(response.Nombre, usuarioRequest.Nombre);
            Assert.Equal(response.Apellido, usuarioRequest.Apellido);
            Assert.Equal(response.Dni, usuarioRequest.Dni);
            Assert.IsType<Guid>(response.usuarioId);
        }

        [Fact]
        public void CreateUsuarioFormatoInvalido()
        {
            //ARRANGE
            var mockUsuarioService = new Mock<IUsuarioService>();
            var controller = new UsuarioController(mockUsuarioService.Object);

            var usuarioInvalido = new UsuarioRequest();
            var expectedCode = 400;
            var expectedMessage = "puede que existan campos invalidos";

            mockUsuarioService.Setup(us => us.CreateUsuario(It.IsAny<UsuarioRequest>())).Throws(new Exception());

            //ACT
            var result = controller.CreateUsuario(usuarioInvalido);

            //ASSERT
            Assert.IsType<JsonResult>(result);

            var jsonResult = result as JsonResult;
            var badRequest = jsonResult?.Value as BadRequest;

            Assert.Equal(jsonResult.StatusCode, expectedCode);
            Assert.Equal(badRequest.message, expectedMessage);
        }

        [Fact]
        public void CreateUsusarioPasswordFormat()
        {
            //ARRANGE
            var mockUsuarioService = new Mock<IUsuarioService>();
            var controller = new UsuarioController(mockUsuarioService.Object);

            var usuarioInvalido = new UsuarioRequest();
            var expectedCode = 409;
            var expectedMessage = "la password requiere al menos un caracter especial";

            mockUsuarioService.Setup(us => us.CreateUsuario(It.IsAny<UsuarioRequest>())).Throws(new PasswordFormatException(expectedMessage));

            //ACT
            var result = controller.CreateUsuario(usuarioInvalido);

            //ASSERT
            Assert.IsType<JsonResult>(result);

            var jsonResult = result as JsonResult;
            var badRequest = jsonResult?.Value as BadRequest;

            Assert.Equal(jsonResult.StatusCode, expectedCode);
            Assert.Equal(badRequest.message, expectedMessage);
        }

        [Fact]
        public void CreateUsuarioEmailExistente()
        {
            //ARRANGE
            var mockUsuarioService = new Mock<IUsuarioService>();
            var controller = new UsuarioController(mockUsuarioService.Object);

            var usuarioInvalido = new UsuarioRequest();
            var expectedCode = 409;
            var expectedMessage = "ese mail ya ah sido utilizado";

            mockUsuarioService.Setup(us => us.CreateUsuario(It.IsAny<UsuarioRequest>())).Throws(new ExistingMailException(expectedMessage));

            //ACT
            var result = controller.CreateUsuario(usuarioInvalido);

            //ASSERT
            Assert.IsType<JsonResult>(result);

            var jsonResult = result as JsonResult;
            var badRequest = jsonResult?.Value as BadRequest;

            Assert.Equal(jsonResult.StatusCode, expectedCode);
            Assert.Equal(badRequest.message, expectedMessage);
        }


        //test deleteUsuario

        [Fact]
        public void DeleteUsuarioOk()
        {
            //ARRANGE
            var mockUsuarioService = new Mock<IUsuarioService>();
            var controller = new UsuarioController(mockUsuarioService.Object);

  
            var usuarioId = Guid.NewGuid();
            var usuarioExiste = new UsuarioResponse
            {
                usuarioId = usuarioId,
                Nombre = "Miguel",
                Apellido = "Correa",
                Dni = "23869487"
            };

            var expectedStatusCode = 200;

            mockUsuarioService.Setup(us => us.GetUsuarioById(usuarioId)).Returns(usuarioExiste);
            mockUsuarioService.Setup(us => us.RemoveUsuario(usuarioId)).Returns(usuarioExiste);

            //ACT
            var result = controller.DeleteUsuario(usuarioId);

            //ASSERT
            Assert.IsType<JsonResult>(result);

            var jsonResult = result as JsonResult;
            var response = jsonResult?.Value as UsuarioResponse;

            Assert.Equal(usuarioExiste.Nombre, response.Nombre);
            Assert.Equal(usuarioExiste.Apellido, response.Apellido);
            Assert.Equal(usuarioExiste.Dni, response.Dni);

            Assert.Equal(expectedStatusCode, jsonResult.StatusCode);
        }


        [Fact]
        public void DeleteUsuarioNoExiste()
        {
            //ARRANGE
            var mockUsuarioService = new Mock<IUsuarioService>();
            var controller = new UsuarioController(mockUsuarioService.Object);


            var usuarioId = Guid.NewGuid();
            var usuarioExiste = new UsuarioResponse
            {
                usuarioId = usuarioId,
                Nombre = "Miguel",
                Apellido = "Correa",
                Dni = "23869487"
            };

            var expectedMessage = "ese usuario no existe";


            mockUsuarioService.Setup(us => us.GetUsuarioById(usuarioId)).Returns((UsuarioResponse)null);

            //ACT
            var result = controller.DeleteUsuario(usuarioId);

            //ASSERT
            Assert.IsType<JsonResult>(result);

            var jsonResult = result as JsonResult;
            var response = jsonResult?.Value as BadRequest;

            Assert.Equal(expectedMessage, response.message);
        }

        //test updateUsuario

        [Fact]
        public void TestUpdateUsuarioFail()
        {
            //ARRANGE
            var mockUsuarioService = new Mock<IUsuarioService>();
            var controller = new UsuarioController(mockUsuarioService.Object);

            var usuarioId = Guid.NewGuid();
            var expectedStatusCode = 400;
            var expectedMessage = "algun dato es incorrecto";

            mockUsuarioService.Setup(us => us.UpdateUsuario(It.IsAny<Guid>(), It.IsAny<UsuarioRequest>())).Throws(new InvalidDataException());

            //ACT
            var result = controller.UpdateUsuario(usuarioId, new UsuarioRequest());

            //ASSERT
            Assert.IsType<JsonResult>(result);

            var jsonResult = result as JsonResult;
            var response = jsonResult.Value as BadRequest;

            Assert.Equal(jsonResult.StatusCode, expectedStatusCode);
            Assert.Equal(response.message, expectedMessage);
        }


        [Fact]
        public void TestUpdateUsuarioOK()
        {
            //ARRANGE
            var mockUsuarioService = new Mock<IUsuarioService>();
            var controller = new UsuarioController(mockUsuarioService.Object);

            var usuarioId = Guid.NewGuid();
            var usuarioRequest = new UsuarioRequest
            {
                Nombre = "Mario",
                Apellido = "Gomez",
                Dni = "15678324"
            };

            var usuarioModificado = new UsuarioResponse
            {
                usuarioId = usuarioId,
                Nombre = "Mario",
                Apellido = "Gomez",
                Dni = "15678324"
            };

            var expectedStatusCode = 200;

            mockUsuarioService.Setup(us => us.UpdateUsuario(usuarioId, usuarioRequest)).Returns(usuarioModificado);

            //ACT
            var result = controller.UpdateUsuario(usuarioId, usuarioRequest);

            //ASSERT
            Assert.IsType<JsonResult>(result);

            var jsonResult = result as JsonResult;
            var response = jsonResult.Value as UsuarioResponse;

            Assert.Equal(jsonResult.StatusCode, expectedStatusCode);

            Assert.Equal(response.Nombre, usuarioModificado.Nombre);
            Assert.Equal(response.Apellido, usuarioModificado.Apellido);
            Assert.Equal(response.Dni, usuarioModificado.Dni);
            Assert.Equal(response.usuarioId, usuarioModificado.usuarioId);
        }
    }
}
