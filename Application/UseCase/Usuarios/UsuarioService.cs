using Application.Common;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Application.Tools;
using Domain.Entities;
using Application.Exceptions;
using Microsoft.VisualBasic.FileIO;

namespace Application.UseCase.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioCommand _command;
        private readonly IUsuarioQuery _query;
        private readonly ITokenService _tokenService;

        public UsuarioService(IUsuarioCommand command, IUsuarioQuery query, ITokenService tokenService)
        {
            _command = command;
            _query = query;
            _tokenService = tokenService;
        }

        public UsuarioTokenResponse Authenticacion(UsuariLoginRequest request)
        {
            UsuarioResponse userLogged = new UsuarioResponse();

            string password = Encrypt.GetSHA256(request.password);
            var usuarioEncontrado = _query.UserLogin(request.email, password);

            if (usuarioEncontrado == null) return null;

            return _tokenService.GenerateToken(usuarioEncontrado);
        }

        public UsuarioResponse CreateUsuario(UsuarioRequest request)
        {
            string caracteresEspeciales = "!\"·$%&/()=¿¡?'_:;,|@#€*+.";
            bool existenCaracteresEspeciales = (caracteresEspeciales.Intersect(request.Password).Count() > 0);

            if (!existenCaracteresEspeciales)
            {
                throw new PasswordFormatException("la password requiere al menos un caracter especial");
            }

            if (request.Password.Length < 8)
            {
                throw new PasswordFormatException("la password requiere al menos 8 caracteres");
            }

            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email,
                Domicilio = request.Domicilio,
                FechaNac = request.FechaNac,
                Nacionalidad = request.Nacionalidad,
                Telefono = request.Telefono,
                Dni = request.Dni,
                Password = Encrypt.GetSHA256(request.Password)
            };
 
                _command.InsertUsuario(usuario);

            return new UsuarioResponse
            {   
                usuarioId = usuario.UsuarioId,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Dni = usuario.Dni
            };
        }

        public UsuarioResponse GetUsuarioById(Guid usuarioId)
        {
            var usuario = _query.GetUsuarioById(usuarioId);

            if (usuario != null)
            {
                return new UsuarioResponse
                {   
                    usuarioId = usuario.UsuarioId,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Dni = usuario.Dni,
                    Email = usuario.Email,
                    Telefono = usuario.Telefono,
                    Domicilio = usuario.Domicilio,
                    Nacionalidad = usuario.Nacionalidad
                };
            }
            return null;

        }

        public List<Usuario> GetUsuarioList()
        {
            return _query.GetUsuarioList();
        }

        public UsuarioResponse RemoveUsuario(Guid usuarioId)
        {
            var usuario = _command.RemoveUsuario(usuarioId);

            return new UsuarioResponse
            {
                usuarioId = usuario.UsuarioId,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Dni = usuario.Dni
            };
        }

        public UsuarioResponse UpdateUsuario(Guid usuarioId, UsuarioRequest request)
        {
            var usuario = _command.UpdateUsuario(usuarioId, request);

            return new UsuarioResponse
            {   
                usuarioId = usuario.UsuarioId,
                Nombre = usuario.Nombre,
                Dni = usuario.Dni,
                Apellido = usuario.Apellido,
            };
        }
    }
}
