using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioCommand _command;
        private readonly IUsuarioQuery _query;

        public UsuarioService(IUsuarioCommand command, IUsuarioQuery query)
        {
            _command = command;
            _query = query;
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
                Nombre = usuario.Nombre,
                Dni = usuario.Dni,
                Apellido = usuario.Apellido,
            };
        }
    }
}
