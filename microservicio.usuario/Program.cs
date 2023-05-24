using Application.Common;
using Application.Interfaces;
using Application.UseCase.Tarjetas;
using Application.UseCase.Tokens;
using Application.UseCase.Usuarios;
using infraestructure.Persistence;
using Infrastructure.Command;
using Infrastructure.Query;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

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
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0.0",
                    Title = "microservicio.usuario",
                    Description = "microservicio que gestiona los usuarios de la agencia de viajes",
                    Contact = new OpenApiContact
                    {
                        Name = "Gabyx708",
                        Url = new Uri("https://github.com/Gabyx708")
                    }
                });

                //documentar swagger
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });


            //custom
            var connectionString = builder.Configuration["ConnectionString"];
            builder.Services.AddDbContext<MicroservicioUsuarioContext>(options => options.UseSqlServer(connectionString));

            //usuario
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IUsuarioCommand, UsuarioCommand>();
            builder.Services.AddScoped<IUsuarioQuery, UsuarioQuery>();

            //tarjeta
            builder.Services.AddScoped<ITarjetaService, TarjetaService>();
            builder.Services.AddScoped<ITarjetaCommand, TarjetaCommand>();
            builder.Services.AddScoped<ITarjetaQuery, TarjetaQuery>();

            //CORS deshabilitar
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            //JWT CONFIGURATION
            var appSettingsSection = builder.Configuration.GetSection("AppSettings");
            builder.Services.Configure<AppSettings>(appSettingsSection);


            //firma
            var appSettings = appSettingsSection.Get<AppSettings>();
            var firma = appSettings.Secreto;

            builder.Services.AddScoped<ITokenService, TokenService>(ServiceProvider =>
            {
                return new TokenService(firma);
            });


        //agregado servicio de token
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
        {
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(appSettings.Secreto)
                ),
                ValidIssuer = "localhost",
                ValidAudience = "usuarios",
            };
        });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}