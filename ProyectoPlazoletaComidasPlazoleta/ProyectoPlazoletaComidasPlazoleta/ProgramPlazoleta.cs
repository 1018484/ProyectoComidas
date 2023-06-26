using Aplicacion.Interfaces;
using Aplicacion.Repositorio;
using Aplicacion.Servicios;
using Applicacion.Interfaces;
using Applicacion.Repositorio;
using Dominio.Modelos;
using Dominio.DTO;
using Dominio.Repositorios;
using Infraestructure.Repositorios;
using infrastructure.Context;
using infrastructure.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Dominio.User_Case;

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

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("Usuarios",
    client => client.BaseAddress = new Uri("https://localhost:7191"));
builder.Services.AddHttpClient("Mensageria",
    client => client.BaseAddress = new Uri("https://localhost:7218"));
builder.Services.AddScoped<IRestaurantRespository<Restaurantes, int>, RestaurantRepository>();
builder.Services.AddScoped<IUsersRemotoRepository<Usuarios, int>, UserRemotoRepository>();
builder.Services.AddSingleton<IRoles, RolesRepository>();
builder.Services.AddScoped<IDishesRepository<Platos, string, int>, DishesRepository>();
builder.Services.AddScoped<IOrdersRepository<Pedidos, string>, OrdersRepository>();
builder.Services.AddScoped<IDishesOrdersRepository<PedidosPlatos, Guid>, DishesOrdersRepository>();
builder.Services.AddScoped<IEmployeeRestaurantRepository<EmpleadosRestaurantes, int>, EmployeeRestaurantsRepository>();
builder.Services.AddScoped<IMessageRemotoRepository, MessageRemotoRepository>();
builder.Services.AddScoped<IEmployee, Employee>();
builder.Services.AddScoped<IRestaurant, Restaurant>();
builder.Services.AddScoped<IDishes, Dishes>();
builder.Services.AddScoped<IClients, Client>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IDishesService, DishesService>();
builder.Services.AddScoped<IRestarurantService, RestaurantService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();






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
