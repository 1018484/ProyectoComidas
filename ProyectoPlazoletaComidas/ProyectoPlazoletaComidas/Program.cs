using Applicacion.Interfaces;
using Applicacion.Repositorio;
using Dominio.Modelos;
using Dominio.Repositorios;
using infrastructure.Context;
using infrastructure.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


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

////Db_Context db = new Db_Context();
//builder.Services.AddDbContext<Db_Context>(opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));
//builder.Services.AddScoped<IRepositorioBase<Usuarios, int>, UsuariosRepository>();
//builder.Services.AddScoped<IUsuarioServicio, UsuariosServicio>();

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

public partial class Program
{

}
