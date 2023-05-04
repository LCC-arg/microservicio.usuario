using Application.Interfaces;
using Application.UseCase.Usuarios;
using infraestructure.Persistence;
using Infrastructure.Command;
using Infrastructure.Query;
using Microsoft.EntityFrameworkCore;

namespace microservicio.usuario
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //custom
            var connectionString = builder.Configuration["ConnectionString"];
            builder.Services.AddDbContext<MicroservicioUsuarioContext>(options => options.UseSqlServer(connectionString));

            //usuario
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IUsuarioCommand, UsuarioCommand>();
            builder.Services.AddScoped<IUsuarioQuery, UsuarioQuery>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}