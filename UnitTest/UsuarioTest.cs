using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.UseCase.Usuarios;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace UnitTest
{
    public class UsuarioTest
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
                Nombre = "Mario",
                Apellido = "Gonzales",
                Dni = "43859843",
                Domicilio = "delhi 3456 , Buenos Aires",
                Email = "marios@gmail.com",
                Nacionalidad = "argentino",
                FechaNac = DateTime.Now,
                Password = "1233445*",
                Telefono = "11 67879 5678",
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
                Nombre = "Mario",
                Apellido = "Gonzales",
                Dni = "43859843",
                Domicilio = "delhi 3456 , Buenos Aires",
                Email = "marios@gmail.com",
                Nacionalidad = "argentino",
                FechaNac = DateTime.Now,
                Password = "1233445*",
                Telefono = "11 67879 5678",
            };

            mockCommand.Setup(c => c.InsertUsuario(It.IsAny<Usuario>())).Throws(new Exception("error al insertar"));

            //ACT
            //ASSERT
            Assert.Throws<Exception>(()=> services.CreateUsuario(request));
        }

        [Fact]
        public void TestUsuarioPasswordLongitud()
        {
            //ARRANGE
            var mockCommand = new Mock<IUsuarioCommand>();
            var mockQuery = new Mock<IUsuarioQuery>();
            var mockTokenService = new Mock<ITokenService>();
            var services = new UsuarioService(mockCommand.Object, mockQuery.Object, mockTokenService.Object);

            var request = new UsuarioRequest
            {
                Password = "12*"
            };

            //ACT
            //ASSERT
            var exception = Assert.Throws<PasswordFormatException>(() => services.CreateUsuario(request));
            Assert.Equal("la password requiere al menos 8 caracteres", exception.Message);


        }

        [Fact]
        public void TestUsuarioPasswordCaracteresEspeciales()
        {
            //ARRANGE
            var mockCommand = new Mock<IUsuarioCommand>();
            var mockQuery = new Mock<IUsuarioQuery>();
            var mockTokenService = new Mock<ITokenService>();
            var services = new UsuarioService(mockCommand.Object, mockQuery.Object, mockTokenService.Object);

            var request = new UsuarioRequest
            {

                Password = "12345678",

            };

            //ACT
            //ASSERT
            var exception = Assert.Throws<PasswordFormatException>(() => services.CreateUsuario(request));
            Assert.Equal("la password requiere al menos un caracter especial", exception.Message);


        }


        [Fact]
        public void GetUsuarioNull()
        {

            //ARRANGE
            var mockCommand = new Mock<IUsuarioCommand>();
            var mockQuery = new Mock<IUsuarioQuery>();
            var mockTokenService = new Mock<ITokenService>();
            var services = new UsuarioService(mockCommand.Object, mockQuery.Object, mockTokenService.Object);

            mockQuery.Setup(m => m.GetUsuarioById(It.IsAny<Guid>())).Returns((Usuario)null );

            //ACT
            var result = services.GetUsuarioById(new Guid());

            //ASERT
            Assert.Null(result);

        }

        [Fact]
        public void GetUsuarioById()
        {
            //ARRANGE
            var mockCommand = new Mock<IUsuarioCommand>();
            var mockQuery = new Mock<IUsuarioQuery>();
            var mockTokenService = new Mock<ITokenService>();
            var services = new UsuarioService(mockCommand.Object, mockQuery.Object, mockTokenService.Object);

            //usuario falso para devolver
            var usuario = new Usuario
            {
                UsuarioId = Guid.NewGuid(),
                Nombre = "John",
                Apellido = "Doe",
                Dni = "12345678"
            };

            mockQuery.Setup(m => m.GetUsuarioById(It.IsAny<Guid>())).Returns(usuario);

            //ACT
            var result = services.GetUsuarioById(Guid.NewGuid());

            //ASSERT
            Assert.NotNull(result);
            Assert.Equal(usuario.UsuarioId, result.usuarioId);
            Assert.Equal(usuario.Nombre, result.Nombre);
            Assert.Equal(usuario.Apellido, result.Apellido);
            Assert.Equal(usuario.Dni, result.Dni);
        }

        [Fact]
        public void TestUpdateUsuario()
        {
            // ARRANGE
            var mockCommand = new Mock<IUsuarioCommand>();
            var mockQuery = new Mock<IUsuarioQuery>();
            var mockTokenService = new Mock<ITokenService>();
            var services = new UsuarioService(mockCommand.Object, mockQuery.Object, mockTokenService.Object);

            var usuarioId = Guid.NewGuid();
            var request = new UsuarioRequest
            {
                Nombre = "Mario",
                Apellido = "Lopez",
                Dni = "43859853"
            };

            var usuarioActualizado = new Usuario
            {
                UsuarioId = usuarioId,
                Nombre = "Ramiro",
                Apellido = "Perez",
                Dni = "43859853"
            };

            mockCommand.Setup(m => m.UpdateUsuario(usuarioId, request)).Returns(usuarioActualizado);

            // ACT
            var result = services.UpdateUsuario(usuarioId, request);

            // ASSERT
            Assert.NotNull(result);
            Assert.Equal(usuarioId, result.usuarioId);
            Assert.NotEqual(request.Nombre, result.Nombre);
            Assert.NotEqual(request.Apellido, result.Apellido);
            Assert.Equal(request.Dni, result.Dni);
        }

    }
}