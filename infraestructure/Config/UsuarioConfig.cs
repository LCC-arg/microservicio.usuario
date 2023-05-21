using Application.Tools;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;


namespace infraestructure.Config
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> entityBuilder)
        {
            entityBuilder.ToTable("Usuario");
            entityBuilder.HasKey(u => u.UsuarioId);

            entityBuilder.HasMany(m => m.Tarjetas)
           .WithOne(cm => cm.Usuario)
           .HasForeignKey(cm => cm.UsuarioId);



            entityBuilder.HasData(

                new Usuario
                {
                    UsuarioId = Guid.NewGuid(),
                    Nombre = "Mariana",
                    Apellido = "Lopez",
                    Dni = "343434",
                    FechaNac = DateTime.Now,
                    Email = "test@gmail.com",
                    Nacionalidad = "peruano",
                    Telefono = "11234567",
                    Domicilio = "su casa",
                    Password = Encrypt.GetSHA256("casa")
                },
                new Usuario
                {
                    UsuarioId = Guid.NewGuid(),
                    Nombre = "Luca",
                    Apellido = "Cyrus",
                    Dni = "343434",
                    FechaNac = DateTime.Now,
                    Email = "test2@gmail.com",
                    Nacionalidad = "peruano",
                    Telefono = "11234567",
                    Domicilio = "su casa",
                    Password = Encrypt.GetSHA256("bondiola")
                },
                new Usuario
                {
                    UsuarioId = Guid.NewGuid(),
                    Nombre = "Juan",
                    Apellido = "Alba",
                    Dni = "34656676",
                    FechaNac = DateTime.Now,
                    Email = "test3@gmail.com",
                    Nacionalidad = "peruano",
                    Telefono = "11234567",
                    Domicilio = "su casa",
                    Password = Encrypt.GetSHA256("unaj")
                }) ;

            }   

        
    }
}
