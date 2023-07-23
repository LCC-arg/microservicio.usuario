using Application.Request;
using Application.Response;
using Application.UseCase.Tokens;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Test.UseCaseTest.TokensTest
{
    public class TokenServiceTest
    {
        [Fact]
        public void TestGenerateToken()
        {
            //ARRANGE
            string fakeFirma = "unajproyectodesoftwareclaveparatokenjwt12334estoessolounaprueba2";
            var services = new TokenService(fakeFirma);

            var usuarioId = new Guid();
            var usuarioLogin = new Usuario
            {
                UsuarioId = usuarioId,
                Email = "pepe@gmail.com",
                Nombre = "Pepe"

            };

            //ACT
            var result = services.GenerateToken(usuarioLogin);

            //ASSERT
            Assert.NotEmpty(result.Token);

            //decoficar token
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(result.Token);

            var uniqueNameClaim = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName);
            Assert.Equal(usuarioId.ToString(), uniqueNameClaim.Value);

            var nameClaim = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name);
            Assert.Equal(usuarioLogin.Nombre, nameClaim.Value);

            var emailClaim = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email);
            Assert.Equal(usuarioLogin.Email, emailClaim.Value);
        }
    }
}
