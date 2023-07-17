using Application.Interfaces;
using Application.Request;
using Application.Response;
using Application.UseCase.Tarjetas;
using Application.UseCase.Usuarios;
using Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class TarjetaServiceTest
    {

        [Fact]
        public void TestCreateTarjeta()
        {
            //ARRANGE
            var mockCommand = new Mock<ITarjetaCommand>();
            var mockQuery = new Mock<ITarjetaQuery>();
            var mockUsuarioService = new Mock<IUsuarioService>();
            var services = new TarjetaService(mockCommand.Object, mockQuery.Object, mockUsuarioService.Object);

            var tarjetaAcrear = new TarjetaRequest
            {
                NumeroTarjeta = "4456 4567 2345 4566",
                TipoTarjeta = "credito",
                Vencimiento = new DateTime(2029, 12, 1),
                EntidadTarjeta = "visa",
                usuarioId = new Guid()
            };

            var tarjetaCreada = new Tarjeta
            {
                NumeroTarjeta = tarjetaAcrear.NumeroTarjeta,
                TarjetaId = new Guid(),
                UsuarioId = new Guid(),
                Vencimiento = tarjetaAcrear.Vencimiento,
                EntidadTarjeta = tarjetaAcrear.EntidadTarjeta,

            };

            mockCommand.Setup(c => c.InsertTarjeta(It.IsAny<Tarjeta>())).Returns(tarjetaCreada);

            //ACT
            var result = services.CreateTarjeta(tarjetaAcrear);

            //ASSERT
            Assert.Equal(typeof(TarjetaResponse), result.GetType());
            result.NumeroTarjeta.Should().Be(tarjetaAcrear.NumeroTarjeta);
            result.EntidadTarjeta.Should().Be(tarjetaAcrear.EntidadTarjeta);
            result.UsuarioId.Should().Be(tarjetaAcrear.usuarioId);

        }

        [Fact]
        public void TestGetTarjeta()
        {
            //ARRANGE
            var mockCommand = new Mock<ITarjetaCommand>();
            var mockQuery = new Mock<ITarjetaQuery>();
            var mockUsuarioService = new Mock<IUsuarioService>();
            var services = new TarjetaService(mockCommand.Object, mockQuery.Object, mockUsuarioService.Object);

            var tarjetaId = new Guid();

            var tarjeta = new Tarjeta
            {
                NumeroTarjeta = "4456 4567 2345 4566",
                TipoTarjeta = "credito",
                Vencimiento = new DateTime(2029, 12, 1),
                EntidadTarjeta = "visa",
                UsuarioId = new Guid(),
                TarjetaId = tarjetaId,
            };

            mockQuery.Setup(q => q.GetTarjetaById(It.IsAny<Guid>())).Returns(tarjeta);

            //ACT
            var result = services.GetTarjetaById(tarjetaId);

            //ASSERT
            result.TarjetaId.Should().Be(tarjeta.TarjetaId);
            result.NumeroTarjeta.Should().Be(tarjeta.NumeroTarjeta);
            result.EntidadTarjeta.Should().Be(tarjeta.EntidadTarjeta);
            result.UsuarioId.Should().Be(tarjeta.UsuarioId);

        }


        [Fact]
        public void TestGetTarjetaList()
        {
            //ARRANGE
            var mockCommand = new Mock<ITarjetaCommand>();
            var mockQuery = new Mock<ITarjetaQuery>();
            var mockUsuarioService = new Mock<IUsuarioService>();
            var services = new TarjetaService(mockCommand.Object, mockQuery.Object, mockUsuarioService.Object);

            // Crear una lista de tarjetas ficticios
            var tarjetasLista = new List<Tarjeta>
            {
                 new Tarjeta {NumeroTarjeta="4456 5678 67889 6789",EntidadTarjeta="visa",UsuarioId= new Guid() },
                 new Tarjeta { NumeroTarjeta = "4456 4367 5633 4568", EntidadTarjeta = "mastercard", UsuarioId = new Guid() },
            };

            mockQuery.Setup(m => m.GetTarjetaList()).Returns(tarjetasLista);

            //ACT
            var result = services.GetTarjetaList();

            // ASSERT
            int cantTarjetas = 2;
            Assert.NotNull(result);
            Assert.Equal(typeof(List<Tarjeta>), result.GetType());
            Assert.Equal(cantTarjetas, result.Count());
            Assert.Equal("4456 5678 67889 6789", result[0].NumeroTarjeta);
            Assert.Equal("4456 4367 5633 4568", result[1].NumeroTarjeta);

        }

        [Fact]
        public void TestRemoveTarjeta()
        {
            // ARRANGE
            var mockQuery = new Mock<ITarjetaQuery>();
            var mockCommand = new Mock<ITarjetaCommand>();
            var mockService = new Mock<IUsuarioService>();
            var services = new TarjetaService(mockCommand.Object, mockQuery.Object ,mockService.Object);

            var tarjetaId = new Guid();

            var tarjetaRemovida = new Tarjeta
            {
                TarjetaId = tarjetaId,
                NumeroTarjeta = "4456 4567 2345 4566",
                EntidadTarjeta = "visa"
            };

            mockCommand.Setup(c => c.RemoveTarjeta(tarjetaId)).Returns(tarjetaRemovida);

            // ACT
            var result = services.RemoveTarjeta(tarjetaId);

            // ASSERT
            Assert.NotNull(result);
            Assert.Equal(tarjetaId, result.TarjetaId);
            Assert.Equal(tarjetaRemovida.NumeroTarjeta, result.NumeroTarjeta);
            Assert.Equal(tarjetaRemovida.EntidadTarjeta, result.EntidadTarjeta);
        }

        [Fact]
        public void TestUpdateTarjeta()
        {
            //ARRANGE
            var mockCommand = new Mock<ITarjetaCommand>();
            var mockQuery = new Mock<ITarjetaQuery>();
            var mockUsuarioService = new Mock<IUsuarioService>();
            var services = new TarjetaService(mockCommand.Object, mockQuery.Object, mockUsuarioService.Object);

            var tarjetaId = Guid.NewGuid();
            var usuarioId = Guid.NewGuid();

            var request = new TarjetaRequest
            {
                NumeroTarjeta = "4456 4567 2345 4566",
                TipoTarjeta = "credito",
                Vencimiento = new DateTime(2029, 12, 1),
                EntidadTarjeta = "visa",
                usuarioId = usuarioId
            };

            var tarjetaActualizada = new Tarjeta
            {   
                TarjetaId = tarjetaId,
                NumeroTarjeta = "4456 4567 2345 4566",
                TipoTarjeta = "credito",
                Vencimiento = new DateTime(2029, 12, 1),
                EntidadTarjeta = "master",
                UsuarioId = usuarioId
            };

            mockCommand.Setup(c => c.UpdateTarjeta(tarjetaId, request)).Returns(tarjetaActualizada);

            // ACT
            var result = services.UpdateTarjeta(tarjetaId, request);

            // ASSERT
            Assert.NotNull(result);
            Assert.Equal(tarjetaId, result.TarjetaId);
            Assert.Equal(request.NumeroTarjeta, result.NumeroTarjeta);
            Assert.NotEqual(request.TipoTarjeta, result.EntidadTarjeta);
        }

        [Fact]
        public void TestGetUsuarioTarjetas()
        {
            //ARRANGE
            var mockCommand = new Mock<ITarjetaCommand>();
            var mockQuery = new Mock<ITarjetaQuery>();
            var mockUsuarioService = new Mock<IUsuarioService>();
            var services = new TarjetaService(mockCommand.Object,mockQuery.Object,mockUsuarioService.Object);

            var usuarioId = new Guid();

            var tarjetasMapear = new List<Tarjeta>
            {
                new Tarjeta{UsuarioId = usuarioId,TarjetaId = new Guid(),NumeroTarjeta="4458 4841 1584",TipoTarjeta="debito",Vencimiento=new DateTime(2029,12,1),EntidadTarjeta="visa"},
                new Tarjeta{UsuarioId= usuarioId, TarjetaId = new Guid(),NumeroTarjeta="4448 4711 1507",TipoTarjeta="credito",Vencimiento=new DateTime(2028,10,1),EntidadTarjeta="visa"}
            };

            mockQuery.Setup(q => q.GetTarjetasUser(It.IsAny<Guid>())).Returns(tarjetasMapear);

            //ACT
            var result = services.GetTarjetaById(usuarioId);

        }



    }
}
