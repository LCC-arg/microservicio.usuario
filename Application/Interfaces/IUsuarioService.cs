using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioResponse Authenticacion(UsuariLoginRequest request);
        UsuarioResponse CreateUsuario(UsuarioRequest request);
        UsuarioResponse RemoveUsuario(Guid usuarioId);
        UsuarioResponse UpdateUsuario(Guid usuarioId, UsuarioRequest request);
        List<Usuario> GetUsuarioList();
        UsuarioResponse GetUsuarioById(Guid usuarioId);
    }
}
