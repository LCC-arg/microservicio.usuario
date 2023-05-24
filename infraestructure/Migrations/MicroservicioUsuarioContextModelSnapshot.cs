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
                            UsuarioId = new Guid("ef6f1334-c8f8-49c5-b166-1a978836eabf"),
                            Apellido = "Lopez",
                            Dni = "343434",
                            Domicilio = "su casa",
                            Email = "test@gmail.com",
                            FechaNac = new DateTime(2023, 5, 23, 21, 20, 27, 170, DateTimeKind.Local).AddTicks(8992),
                            Nacionalidad = "peruano",
                            Nombre = "Mariana",
                            Password = "b3813027ed2150ec3449f0716cf53c5d4a632486136365bd23e19c372884553f",
                            Telefono = "11234567"
                        },
                        new
                        {
                            UsuarioId = new Guid("6421d372-9d90-4b13-8e62-1022f1f3feca"),
                            Apellido = "Cyrus",
                            Dni = "343434",
                            Domicilio = "su casa",
                            Email = "test2@gmail.com",
                            FechaNac = new DateTime(2023, 5, 23, 21, 20, 27, 170, DateTimeKind.Local).AddTicks(9212),
                            Nacionalidad = "peruano",
                            Nombre = "Luca",
                            Password = "e33b8c45db60319d8182c160e4892d6a84686bc5e329cc93eb2297fbe4a6e8c9",
                            Telefono = "11234567"
                        },
                        new
                        {
                            UsuarioId = new Guid("87f04cb9-2213-4601-9204-308f5c02156c"),
                            Apellido = "Alba",
                            Dni = "34656676",
                            Domicilio = "su casa",
                            Email = "test3@gmail.com",
                            FechaNac = new DateTime(2023, 5, 23, 21, 20, 27, 170, DateTimeKind.Local).AddTicks(9282),
                            Nacionalidad = "peruano",
                            Nombre = "Juan",
                            Password = "4f6b05b9eb6c3705f159c373c4a8151b13fdbcbcee3e0adeb99c95702e80d91e",
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
