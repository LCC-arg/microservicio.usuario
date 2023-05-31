using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly string _claveFirma;

        public TokenService(string claveFirma)
        {
            _claveFirma = claveFirma;
        }

        public UsuarioTokenResponse GenerateToken(Usuario userLogin)
        {

            var header = new JwtHeader(
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_claveFirma)
                    ),
                    SecurityAlgorithms.HmacSha256)
            );

            var claims = new Claim[]
            {
            new Claim(JwtRegisteredClaimNames.UniqueName, userLogin.UsuarioId.ToString()),
            new Claim(JwtRegisteredClaimNames.Name , userLogin.Nombre),
            new Claim(JwtRegisteredClaimNames.Email, userLogin.Email),
            };

            var payload = new JwtPayload(claims) {
            // Agregar una fecha de expiración de 1 hora desde ahora
                { "exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds() },
                {"aud", "usuarios" },
                {"iss", "localhost" }
            };

            var token = new JwtSecurityToken(header, payload);
            var tokenUsuairo = new JwtSecurityTokenHandler().WriteToken(token);

            return new UsuarioTokenResponse
            {
                Token = tokenUsuairo
            };
        }
    }
}
