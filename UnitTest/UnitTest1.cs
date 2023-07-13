using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.UseCase.Usuarios;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void TestCreateUsuario()
        {
            //ARRANGE
            var mockCommand = new Mock<IUsuarioCommand>();
            var mockQuery = new Mock<IUsuarioQuery>();
            var mockTokenService = new Mock<ITokenService>();
            var services = new UsuarioService(mockCommand.Object,mockQuery.Object,mockTokenService.Object);

            var request = new UsuarioRequest
            {
                Nombre = "123",
                Apellido = "123",
                Dni = "123",
                Domicilio = "123",
                Email = "123",
                Nacionalidad = "123",
                FechaNac = DateTime.Now,
                Password = "1233445*",
                Telefono = "123",
            };

            //ACT
            var result = services.CreateUsuario(request);

            //ASSERT
          
            result.Nombre.Should().Be(request.Nombre);
            result.Apellido.Should().Be(request.Apellido);
            result.Dni.Should().Be(request.Dni);
        }

        [Fact]
        public void TestCreateUsuarioFail()
        {
            //ARRANGE
            var mockCommand = new Mock<IUsuarioCommand>();
            var mockQuery = new Mock<IUsuarioQuery>();
            var mockTokenService = new Mock<ITokenService>();
            var services = new UsuarioService(mockCommand.Object, mockQuery.Object, mockTokenService.Object);

            var request = new UsuarioRequest
            {
                Nombre = "123",
                Apellido = "123",
                Dni = "123",
                Domicilio = "123",
                Email = "123",
                Nacionalidad = "123",
                FechaNac = DateTime.Now,
                Password = "1233445*",
                Telefono = "123",
            };

            mockCommand.Setup(c => c.InsertUsuario(It.IsAny<Usuario>())).Throws(new Exception("error al insertar"));

            //ACT
            //ASSERT
            Assert.Throws<Exception>(()=> services.CreateUsuario(request));
        }

        [Fact]
        public void TestUsuarioPassword()
        {
            //ARRANGE
            var mockCommand = new Mock<IUsuarioCommand>();
            var mockQuery = new Mock<IUsuarioQuery>();
            var mockTokenService = new Mock<ITokenService>();
            var services = new UsuarioService(mockCommand.Object, mockQuery.Object, mockTokenService.Object);

            var request = new UsuarioRequest
            {
                Nombre = "123",
                Apellido = "123",
                Dni = "123",
                Domicilio = "123",
                Email = "123",
                Nacionalidad = "123",
                FechaNac = DateTime.Now,
                Password = "123456",
                Telefono = "123",
            };

            //ACT
            //ASSERT
            Assert.Throws<PasswordFormatException>(() => services.CreateUsuario(request));



        }


        [Fact]
        public void TestUsuarioLogin()
        {
            //ARRANGE
            var mockCommand = new Mock<IUsuarioCommand>();
            var mockQuery = new Mock<IUsuarioQuery>();
            var mockTokenService = new Mock<ITokenService>();
            var services = new UsuarioService(mockCommand.Object, mockQuery.Object, mockTokenService.Object);

            var request = new UsuariLoginRequest
            {
                email = "test@gmail.com",
                password = "casa",
            };

            //ACT
            var result = services.Authenticacion(request);

            //ASSERT
            Assert.Equal(typeof(UsuariLoginRequest),result.GetType());

        }
    }
}