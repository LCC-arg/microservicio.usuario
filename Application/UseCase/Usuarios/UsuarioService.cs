using Application.Common;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Application.Tools;
using Domain.Entities;
using Microsoft.VisualBasic.FileIO;

namespace Application.UseCase.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioCommand _command;
        private readonly IUsuarioQuery _query;
        private readonly AppSettings _appSettings; //aqui esta el secreto

        public UsuarioService(IUsuarioCommand command, IUsuarioQuery query,IOptions<AppSettings> appSettings)
        {
            _command = command;
            _query = query;
            _appSettings = appSettings;
        }

        public UsuarioResponse Authenticacion(UsuariLoginRequest request)
        {
            UsuarioResponse userLogged = new UsuarioResponse();

            string password = Encrypt.GetSHA256(request.password);
            var usuarioEncontrado = _query.UserLogin(request.email, password);

            if (usuarioEncontrado == null) return null;

            userLogged.usuarioId = usuarioEncontrado.UsuarioId;
            userLogged.Dni = usuarioEncontrado.Dni;
            userLogged.Token = "";

            return userLogged;
        }

        public UsuarioResponse CreateUsuario(UsuarioRequest request)
        {
            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email,
                Domicilio = request.Domicilio,
                FechaNac = request.FechaNac,
                Nacionalidad = request.Nacionalidad,
                Telefono = request.Telefono,
                Dni = request.Dni
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
                    Dni = usuario.Dni
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
