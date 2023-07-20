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

namespace UnitTest
{
    public class TarjetaControllerTest
    {

        [Fact]
        public void TestCreateTarjetaInService()
        {
            // Arrange
            var mockCommand = new Mock<ITarjetaCommand>();
            var mockQuery = new Mock<ITarjetaQuery>();
            var mockUsuarioService = new Mock<IUsuarioService>();
            var tarjetaService = new TarjetaService(mockCommand.Object, mockQuery.Object, mockUsuarioService.Object);

            var controller = new TarjetaController(tarjetaService);

            var usuarioId = Guid.NewGuid();
            var tarjetaId = Guid.NewGuid();

            var request = new TarjetaRequest
            {
                NumeroTarjeta = "4458 4810 1584 1256",
                TipoTarjeta = "visa",
                Vencimiento = new DateTime(2029, 10, 1),
                usuarioId = usuarioId
            };

            var tarjetaFalsa = new Tarjeta
            {
                NumeroTarjeta = request.NumeroTarjeta,
                EntidadTarjeta = request.EntidadTarjeta,
                TarjetaId = tarjetaId,
                UsuarioId = usuarioId
            };

            mockCommand.Setup(c => c.InsertTarjeta(It.IsAny<Tarjeta>())).Returns(tarjetaFalsa);
            mockUsuarioService.Setup(us => us.GetUsuarioById(It.IsAny<Guid>())).Returns(new UsuarioResponse());

            // Act
            var result = controller.CreateTarjeta(request);

            // Assert
            Assert.NotNull(result);
        }

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
            var badRequest = jsonResult.Value as BadRequest;
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
        public void TestGetTarjetaUsuario()
        {
            // ARRANGE
            var mockTarjetaService = new Mock<ITarjetaService>();
            var controller = new TarjetaController(mockTarjetaService.Object);

            var usuarioId = Guid.NewGuid();
            var tarjetasUsuarioResponse = new TarjetasUsuarioResponse { /* Proporciona los datos necesarios para el objeto TarjetasUsuarioResponse */ };
            mockTarjetaService.Setup(ts => ts.GetUsuarioTarjetas(usuarioId)).Returns(tarjetasUsuarioResponse);

            // ACT
            var result = controller.GetTarjetaUsuario(usuarioId.ToString());

            // ASSERT
            Assert.IsType<JsonResult>(result);
            var jsonResult = result as JsonResult;
            Assert.Equal(tarjetasUsuarioResponse, jsonResult.Value);
        }



    }
}
