using Application.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioCommand
    {
        Usuario InsertUsuario(Usuario usuario);

        Usuario RemoveUsuario(Guid usuarioId);

        Usuario UpdateUsuario(Guid usuarioId, UsuarioRequest request);

    }
}
