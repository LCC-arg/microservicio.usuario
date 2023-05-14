using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class dataexample : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "UsuarioId", "Apellido", "Dni", "Domicilio", "Email", "FechaNac", "Nacionalidad", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { new Guid("1ea78b58-99fc-402c-91f7-4da4f9f56d08"), "Cyrus", "343434", "su casa", "test2@gmail.com", new DateTime(2023, 5, 13, 21, 53, 35, 390, DateTimeKind.Local).AddTicks(4099), "peruano", "Luca", "11234567" },
                    { new Guid("5a95785b-3280-4f75-816c-4e6970013bfc"), "Alba", "34656676", "su casa", "test3@gmail.com", new DateTime(2023, 5, 13, 21, 53, 35, 390, DateTimeKind.Local).AddTicks(4103), "peruano", "Juan", "11234567" },
                    { new Guid("db15a628-1320-4bee-83fc-1111f78783db"), "Lopez", "343434", "su casa", "test@gmail.com", new DateTime(2023, 5, 13, 21, 53, 35, 390, DateTimeKind.Local).AddTicks(4088), "peruano", "Mariana", "11234567" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "UsuarioId",
                keyValue: new Guid("1ea78b58-99fc-402c-91f7-4da4f9f56d08"));

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "UsuarioId",
                keyValue: new Guid("5a95785b-3280-4f75-816c-4e6970013bfc"));

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "UsuarioId",
                keyValue: new Guid("db15a628-1320-4bee-83fc-1111f78783db"));
        }
    }
}
