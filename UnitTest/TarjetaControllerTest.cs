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



    }
}
