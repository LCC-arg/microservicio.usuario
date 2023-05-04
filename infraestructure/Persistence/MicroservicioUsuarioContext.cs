using Domain.Entities;
using infraestructure.Config;
using Microsoft.EntityFrameworkCore;

namespace infraestructure.Persistence
{
    public class MicroservicioUsuarioContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarjeta> Tarjetas { get; set; }

        public MicroservicioUsuarioContext(DbContextOptions<MicroservicioUsuarioContext> options)
        : base(options) { }

        //config.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfig());
            modelBuilder.ApplyConfiguration(new TarjetaConfig());
        }
    }
}
