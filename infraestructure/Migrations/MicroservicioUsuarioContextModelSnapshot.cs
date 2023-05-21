﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using infraestructure.Persistence;

#nullable disable

namespace infraestructure.Migrations
{
    [DbContext(typeof(MicroservicioUsuarioContext))]
    partial class MicroservicioUsuarioContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Tarjeta", b =>
                {
                    b.Property<Guid>("TarjetaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EntidadTarjeta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroTarjeta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoTarjeta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Vencimiento")
                        .HasColumnType("datetime2");

                    b.HasKey("TarjetaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Tarjeta", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Usuario", b =>
                {
                    b.Property<Guid>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Domicilio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNac")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nacionalidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuario", (string)null);

                    b.HasData(
                        new
                        {
                            UsuarioId = new Guid("c768cb9e-84fe-411c-b33d-096ec177893f"),
                            Apellido = "Lopez",
                            Dni = "343434",
                            Domicilio = "su casa",
                            Email = "test@gmail.com",
                            FechaNac = new DateTime(2023, 5, 21, 10, 50, 33, 718, DateTimeKind.Local).AddTicks(778),
                            Nacionalidad = "peruano",
                            Nombre = "Mariana",
                            Password = "02a68f9d9195dd53eb799f866429ce06e93be4ddf8b1b41a3d926dcf7d4f535f",
                            Telefono = "11234567"
                        },
                        new
                        {
                            UsuarioId = new Guid("56aee871-f6fe-4fd2-90c4-8b557c412fe2"),
                            Apellido = "Cyrus",
                            Dni = "343434",
                            Domicilio = "su casa",
                            Email = "test2@gmail.com",
                            FechaNac = new DateTime(2023, 5, 21, 10, 50, 33, 718, DateTimeKind.Local).AddTicks(791),
                            Nacionalidad = "peruano",
                            Nombre = "Luca",
                            Password = "02a68f9d9195dd53eb799f866429ce06e93be4ddf8b1b41a3d926dcf7d4f535f",
                            Telefono = "11234567"
                        },
                        new
                        {
                            UsuarioId = new Guid("d910b3b4-cb10-416e-8a43-999c2a674427"),
                            Apellido = "Alba",
                            Dni = "34656676",
                            Domicilio = "su casa",
                            Email = "test3@gmail.com",
                            FechaNac = new DateTime(2023, 5, 21, 10, 50, 33, 718, DateTimeKind.Local).AddTicks(794),
                            Nacionalidad = "peruano",
                            Nombre = "Juan",
                            Password = "02a68f9d9195dd53eb799f866429ce06e93be4ddf8b1b41a3d926dcf7d4f535f",
                            Telefono = "11234567"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Tarjeta", b =>
                {
                    b.HasOne("Domain.Entities.Usuario", "Usuario")
                        .WithMany("Tarjetas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Entities.Usuario", b =>
                {
                    b.Navigation("Tarjetas");
                });
#pragma warning restore 612, 618
        }
    }
}
