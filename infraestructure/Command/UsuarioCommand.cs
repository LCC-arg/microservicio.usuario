using Application.Interfaces;
using Application.Request;
using Application.Exceptions;
using Domain.Entities;
using infraestructure.Persistence;

namespace Infrastructure.Command
{
    public class UsuarioCommand : IUsuarioCommand
    {
        private readonly MicroservicioUsuarioContext _context;

        public UsuarioCommand(MicroservicioUsuarioContext context)
        {
            _context = context;
        }

        public Usuario InsertUsuario(Usuario usuario)
        {   
            var existMail = _context.Usuarios.FirstOrDefault(u => u.Email == usuario.Email);

            if (existMail != null)
            {
                throw new ExistingMailException("ese mail ya ah sido utilizado");
            }

            _context.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public Usuario RemoveUsuario(Guid usuarioId)
        {
            var removeUsuario = _context.Usuarios.Single(x => x.UsuarioId == usuarioId);
            _context.Remove(removeUsuario);
            _context.SaveChanges();
            return removeUsuario;
        }

        public Usuario UpdateUsuario(Guid usuarioId, UsuarioRequest request)
        {
            var updateUsuario = _context.Usuarios
            .FirstOrDefault(x => x.UsuarioId == usuarioId);

            if (updateUsuario == null)
            {
                throw new InvalidDataException();
            }

            updateUsuario.Nombre = request.Nombre;
            updateUsuario.Apellido = request.Apellido;
            updateUsuario.Dni = request.Dni;
            updateUsuario.FechaNac = request.FechaNac;
            updateUsuario.Email = request.Email;
            updateUsuario.Nacionalidad = request.Nacionalidad;
            updateUsuario.Telefono = request.Telefono;
            updateUsuario.Domicilio = request.Domicilio;

            _context.Update(updateUsuario);
            _context.SaveChanges();


            return updateUsuario;
        }
    }
}
