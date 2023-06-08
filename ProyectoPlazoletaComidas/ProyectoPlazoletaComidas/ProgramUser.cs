using Applicacion.Interfaces;
using Applicacion.Repositorio;
using Dominio.Modelos;
using Dominio.Repositorios;
using Infraestructure.Repositorios;
using infrastructure.Context;
using infrastructure.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Db_Context>(opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL"), b => b.MigrationsAssembly("Users")));
builder.Configuration.AddJsonFile("appsettings.json");
var secretKey = builder.Configuration.GetSection("Settings").GetSection("SecretKey").ToString();
var KeyBytes = Encoding.UTF8.GetBytes(secretKey);
var a = builder.Configuration.GetSection("Settings").GetSection("Audiencie").ToString();
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
        ValidateAudience = false,
        ValidateLifetime = true,        
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Settings").GetSection("Issuer").ToString(),
        ValidAudience = builder.Configuration.GetSection("Settings").GetSection("Audiencie").ToString(),
        IssuerSigningKey = new SymmetricSecurityKey(KeyBytes),
    };
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("Plazoleta",
    client => client.BaseAddress = new Uri("https://localhost:7270"));
//builder.Services.AddDbContext<Db_Context>(opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));
builder.Services.AddScoped<IRepositorioBase<Usuarios, int>, UsuariosRepository>();
builder.Services.AddSingleton<IRoles, RolesRepositorio>();
builder.Services.AddScoped<IRepositorioRestauranteEmpleados<RestauranteEmpleados, int>, RepositorioRestauranteEmpleados>();
builder.Services.AddScoped<IUsuarioServicio, UsuariosServicio>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


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

public partial class ProgramUser
{

}
