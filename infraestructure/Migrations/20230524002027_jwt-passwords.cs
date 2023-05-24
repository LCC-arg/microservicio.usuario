using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class jwtpasswords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "UsuarioId",
                keyValue: new Guid("56aee871-f6fe-4fd2-90c4-8b557c412fe2"));

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "UsuarioId",
                keyValue: new Guid("c768cb9e-84fe-411c-b33d-096ec177893f"));

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "UsuarioId",
                keyValue: new Guid("d910b3b4-cb10-416e-8a43-999c2a674427"));

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "UsuarioId", "Apellido", "Dni", "Domicilio", "Email", "FechaNac", "Nacionalidad", "Nombre", "Password", "Telefono" },
                values: new object[,]
                {
                    { new Guid("6421d372-9d90-4b13-8e62-1022f1f3feca"), "Cyrus", "343434", "su casa", "test2@gmail.com", new DateTime(2023, 5, 23, 21, 20, 27, 170, DateTimeKind.Local).AddTicks(9212), "peruano", "Luca", "e33b8c45db60319d8182c160e4892d6a84686bc5e329cc93eb2297fbe4a6e8c9", "11234567" },
                    { new Guid("87f04cb9-2213-4601-9204-308f5c02156c"), "Alba", "34656676", "su casa", "test3@gmail.com", new DateTime(2023, 5, 23, 21, 20, 27, 170, DateTimeKind.Local).AddTicks(9282), "peruano", "Juan", "4f6b05b9eb6c3705f159c373c4a8151b13fdbcbcee3e0adeb99c95702e80d91e", "11234567" },
                    { new Guid("ef6f1334-c8f8-49c5-b166-1a978836eabf"), "Lopez", "343434", "su casa", "test@gmail.com", new DateTime(2023, 5, 23, 21, 20, 27, 170, DateTimeKind.Local).AddTicks(8992), "peruano", "Mariana", "b3813027ed2150ec3449f0716cf53c5d4a632486136365bd23e19c372884553f", "11234567" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "UsuarioId",
                keyValue: new Guid("6421d372-9d90-4b13-8e62-1022f1f3feca"));

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "UsuarioId",
                keyValue: new Guid("87f04cb9-2213-4601-9204-308f5c02156c"));

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "UsuarioId",
                keyValue: new Guid("ef6f1334-c8f8-49c5-b166-1a978836eabf"));

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "UsuarioId", "Apellido", "Dni", "Domicilio", "Email", "FechaNac", "Nacionalidad", "Nombre", "Password", "Telefono" },
                values: new object[,]
                {
                    { new Guid("56aee871-f6fe-4fd2-90c4-8b557c412fe2"), "Cyrus", "343434", "su casa", "test2@gmail.com", new DateTime(2023, 5, 21, 10, 50, 33, 718, DateTimeKind.Local).AddTicks(791), "peruano", "Luca", "02a68f9d9195dd53eb799f866429ce06e93be4ddf8b1b41a3d926dcf7d4f535f", "11234567" },
                    { new Guid("c768cb9e-84fe-411c-b33d-096ec177893f"), "Lopez", "343434", "su casa", "test@gmail.com", new DateTime(2023, 5, 21, 10, 50, 33, 718, DateTimeKind.Local).AddTicks(778), "peruano", "Mariana", "02a68f9d9195dd53eb799f866429ce06e93be4ddf8b1b41a3d926dcf7d4f535f", "11234567" },
                    { new Guid("d910b3b4-cb10-416e-8a43-999c2a674427"), "Alba", "34656676", "su casa", "test3@gmail.com", new DateTime(2023, 5, 21, 10, 50, 33, 718, DateTimeKind.Local).AddTicks(794), "peruano", "Juan", "02a68f9d9195dd53eb799f866429ce06e93be4ddf8b1b41a3d926dcf7d4f535f", "11234567" }
                });
        }
    }
}
