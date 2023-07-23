using agencia_de_viajes.Controllers;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Application.UseCase.Tarjetas;
using Application.UseCase.Usuarios;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Test.ControllersTest.TarjetasControllerTest
{
    public class TarjetaControllerTest
    {
        //crearTarjeta

        [Fact]
        public void TestCreateTarjetaOk()
        {
            // Arrange
            var mockCommand = new Mock<ITarjetaCommand>();
            var mockQuery = new Mock<ITarjetaQuery>();
            var mockUsuarioService = new Mock<IUsuarioService>();
            var tarjetaService = new TarjetaService(mockCommand.Object, mockQuery.Object, mockUsuarioService.Object);

            var controller = new TarjetaController(tarjetaService);

            var usuarioId = Guid.NewGuid();
            var tarjetaId = Guid.NewGuid();

            var requestTarjeta = new TarjetaRequest
            {
                NumeroTarjeta = "4458 4810 1584 1256",
                TipoTarjeta = "visa",
                Vencimiento = new DateTime(2029, 10, 1),
                EntidadTarjeta = "visa",
                usuarioId = usuarioId
            };

            ;
            var usuarioFalso = new UsuarioResponse
            {
                Nombre = "Miguel",
                Apellido = "Correa",
                Dni = "23869487",
                usuarioId = usuarioId
            };


            mockUsuarioService.Setup(us => us.GetUsuarioById(It.IsAny<Guid>())).Returns(usuarioFalso);


            // Act
            var result = controller.CreateTarjeta(requestTarjeta);

            // Assert
            Assert.NotNull(result);

            var jsonResult = result as JsonResult;
            var response = jsonResult.Value as TarjetaResponse;

            Assert.Equal(response.NumeroTarjeta, requestTarjeta.NumeroTarjeta);
            Assert.Equal(response.EntidadTarjeta, requestTarjeta.EntidadTarjeta);


            Assert.Equal(response.Usuario.Nombre, usuarioFalso.Nombre);
            Assert.Equal(response.Usuario.Apellido, usuarioFalso.Apellido);
            Assert.Equal(response.Usuario.Dni, usuarioFalso.Dni);
            Assert.Equal(response.Usuario.usuarioId, usuarioFalso.usuarioId);
        }


        [Fact]
        public void TestCreateTarjetaExcepcion()
        {
            // Arrange
            var mockTarjetaService = new Mock<ITarjetaService>();

            mockTarjetaService.Setup(ts => ts.CreateTarjeta(It.IsAny<TarjetaRequest>())).Throws(new Exception());

            var controller = new TarjetaController(mockTarjetaService.Object);
            var expectedMessage = "error al registrar la tarjeta";
            var expectedStatusCode = 404;

            //ACT
            var result = controller.CreateTarjeta(new TarjetaRequest());

            //ASSERT
            Assert.IsType<JsonResult>(result);
            var jsonResult = result as JsonResult;

            Assert.NotNull(jsonResult);
            Assert.Equal(expectedStatusCode, jsonResult.StatusCode);

            var badRequest = jsonResult.Value as BadRequest;
            Assert.Equal(expectedMessage, badRequest.message);
        }

        [Fact]
        public void TestCreateTarjetaNull()
        {
            // Arrange
            var mockTarjetaService = new Mock<ITarjetaService>();

            mockTarjetaService.Setup(ts => ts.CreateTarjeta(It.IsAny<TarjetaRequest>())).Returns((TarjetaResponse)null);

            var controller = new TarjetaController(mockTarjetaService.Object);

            var expectedMessage = "no se creo la tarjeta";

            //ACT
            var result = controller.CreateTarjeta(new TarjetaRequest());

            //ASSERT
            Assert.IsType<JsonResult>(result);
            var jsonResult = result as JsonResult;

            Assert.NotNull(jsonResult);
            var badRequest = jsonResult.Value as BadRequest;

            Assert.NotNull(badRequest);
            Assert.Equal(expectedMessage, badRequest.message);
        }





        //Test metodo GetTarjetasUsuario

        [Fact]
        public void TestGetTarjetaUserNulo()
        {
            //ARRANGE
            var mockTarjetaService = new Mock<ITarjetaService>();
            var controller = new TarjetaController(mockTarjetaService.Object);

            var usuarioId = Guid.NewGuid();
            int expectedStatus = 404;
            string expectedMessage = "no hemos encontrado nada";

            mockTarjetaService.Setup(ts => ts.GetUsuarioTarjetas(It.IsAny<Guid>())).Returns((TarjetasUsuarioResponse)null);

            //ACT
            var result = controller.GetTarjetaUsuario(usuarioId.ToString());


            //ASSERT

            Assert.IsType<JsonResult>(result);
            var jsonResult = result as JsonResult;
            var badRequest = jsonResult?.Value as BadRequest;
            Assert.NotNull(badRequest);
            Assert.Equal(expectedStatus, jsonResult.StatusCode);
            Assert.Equal(expectedMessage, badRequest.message);
        }


        [Fact]
        public void TestGetTarjetaUsuarioIdInvalido()
        {
            //ARRANGE
            var mockTarjetaService = new Mock<ITarjetaService>();
            var controller = new TarjetaController(mockTarjetaService.Object);

            string usuarioId = "formato incorrecto";
            string expectedMessage = "el formato del id no es valido";

            //ACT
            var result = controller.GetTarjetaUsuario(usuarioId);

            //ASSERT

            Assert.IsType<JsonResult>(result);
            var jsonResult = result as JsonResult;
            var badRequest = jsonResult.Value as BadRequest;
            Assert.Equal(expectedMessage, badRequest.message);

        }


        [Fact]
        public void TestGetTarjetaUsuarioOk()
        {
            // ARRANGE
            var mockTarjetaService = new Mock<ITarjetaService>();
            var controller = new TarjetaController(mockTarjetaService.Object);

            var usuarioId = Guid.NewGuid();

            var listaTarjetas = new List<TarjetaGetResponse>
            {
                new TarjetaGetResponse{id=Guid.NewGuid(),NumeroTarjeta ="4485 1502 1584 4951",Vencimiento= new DateTime(2029,12,1),EntidadTarjeta="visa"},
                new TarjetaGetResponse{id=Guid.NewGuid(),NumeroTarjeta ="4780 1502 1584 0870",Vencimiento= new DateTime(2027,12,1),EntidadTarjeta="mastercard"}
            };

            var tarjetasUsuarioResponse = new TarjetasUsuarioResponse
            {
                usuarioId = usuarioId,
                nombre = "Mario",
                tarjetasUsuario = listaTarjetas
            };
            mockTarjetaService.Setup(ts => ts.GetUsuarioTarjetas(usuarioId)).Returns(tarjetasUsuarioResponse);

            // ACT
            var result = controller.GetTarjetaUsuario(usuarioId.ToString());

            // ASSERT
            Assert.IsType<JsonResult>(result);

            var jsonResult = result as JsonResult;
            var resultTarjetaResponse = jsonResult?.Value as TarjetasUsuarioResponse;

            Assert.Equal(tarjetasUsuarioResponse.tarjetasUsuario, resultTarjetaResponse?.tarjetasUsuario);
            Assert.Equal(tarjetasUsuarioResponse.nombre, resultTarjetaResponse.nombre);
            Assert.Equal(tarjetasUsuarioResponse.usuarioId, resultTarjetaResponse.usuarioId);

        }



    }
}
