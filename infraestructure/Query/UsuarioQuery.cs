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

        public Usuario UserLogin(string UserMail , string UserPassword)
        {
            var usuario = _context.Usuarios.Where(u => u.Email == UserMail && u.Password == UserPassword).FirstOrDefault();
            if (usuario == null) { return null; }

            return usuario;
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
