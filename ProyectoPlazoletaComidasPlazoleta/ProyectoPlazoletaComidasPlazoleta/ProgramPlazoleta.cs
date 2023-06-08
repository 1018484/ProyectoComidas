using Aplicacion.Interfaces;
using Aplicacion.Repositorio;
using Aplicacion.Servicios;
using Applicacion.Interfaces;
using Applicacion.Repositorio;
using Dominio.Modelos;
using Dominio.Modelos.DTO;
using Dominio.Repositorios;
using Infraestructure.Repositorios;
using infrastructure.Context;
using infrastructure.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Db_Context>(opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL"), b => b.MigrationsAssembly("ProyectoPlazoletaComidasPlazoleta")));
builder.Configuration.AddJsonFile("appsettings.json");
var secretKey = builder.Configuration.GetSection("Settings").GetSection("SecretKey").ToString();
var KeyBytes = Encoding.UTF8.GetBytes(secretKey);
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = true;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(KeyBytes),
        ValidateAudience = false,

    };
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("Usuarios",
    client => client.BaseAddress = new Uri("https://localhost:7191"));
builder.Services.AddScoped<IRepositorioRestaurante<Restaurantes, int>, RestauranteRepositorio>();
builder.Services.AddScoped<IRepositorioUsuariosRemoto<Usuarios, int>, UsuarioRepositorioRemoto>();
builder.Services.AddSingleton<IRoles, RolesRepositorio>();
builder.Services.AddScoped<IRepositorioPlatos<Platos, string, int>, PlatosRepositotio>();
builder.Services.AddScoped<IRepositotioPedidos<Pedidos, string>, PedidosRepositorio>();
builder.Services.AddScoped<IRepositorioPedidosPlatos<PedidosPlatos>, PedidosPlatosRepositorio>();
builder.Services.AddScoped<IClientesServicio, ClienteServicio>();
builder.Services.AddScoped<IPlatosServicio<PlatosDTO, int>, PlatosServicio>();
builder.Services.AddScoped<IRestaruranteServicio, RestauranteServicio>();
builder.Services.AddScoped<IEmpleadosServicio, EmpleadoServicio>();






// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class ProgramPlazoleta
{

}
