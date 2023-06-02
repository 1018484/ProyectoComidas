using Microsoft.AspNetCore.Mvc;
using Applicacion.Interfaces;
using Applicacion.Repositorio;
using infrastructure.Context;
using infrastructure.Repositorios;
using Dominio.Modelos;
using Microsoft.AspNetCore.Authorization;
using Aplicacion.Repositorio;
using Infraestructure.Repositorios;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Aplicacion.Validaciones;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;
using Dominio.Modelos.DTO;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Plazoleta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantesController : ControllerBase
    {
        public readonly string secretkey;
        public RestaurantesController(IConfiguration config)
        {
            secretkey = config.GetSection("Settings").GetSection("SecretKey").ToString();
        }
        RestauranteServicio restauranteServicio()
        {
            Db_Context db = new Db_Context();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7191");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            RestauranteRepositorio restauranteRepositorio = new RestauranteRepositorio(db);
            UsuarioRepositorioRemoto RepoUsuarioRemo = new UsuarioRepositorioRemoto(httpClient);
            RolesRepositorio rolesRepositorio = new RolesRepositorio(HttpContext, httpClient);
            RestauranteServicio serciciorestaurante = new RestauranteServicio(restauranteRepositorio, RepoUsuarioRemo, rolesRepositorio);
            return serciciorestaurante;
        }

        [HttpPost]
        public async Task<IActionResult> CrearRestauranteAsync([FromBody] RestaurantesDTO restaurante)
        {
            try
            {              

                var servicio = restauranteServicio();
                await servicio.Agregar(restaurante);
                return Ok("Restaurante agregado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }       
    }
}
