using Application.Interfaces;
using Domain.Entities;
using infraestructure.Persistence;

namespace Infrastructure.Query
{
    public class UsuarioQuery : IUsuarioQuery
    {
        private readonly MicroservicioUsuarioContext _context;

        public UsuarioQuery(MicroservicioUsuarioContext context)
        {
            _context = context;
        }

        public Usuario GetUsuarioById(Guid usuarioId)
        {
            var usuario = _context.Usuarios.FirstOrDefault(x => x.UsuarioId == usuarioId);
            return usuario;
        }

        public List<Usuario> GetUsuarioList()
        {
            List<Usuario> usuarioList = _context.Usuarios.ToList();
            return usuarioList;
        }
    }
}
