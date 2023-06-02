using Aplicacion.Repositorio;
using Dominio.Modelos;
using Infraestructure.Repositorios;
using infrastructure.Context;
using infrastructure.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Dominio.Modelos.DTO;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlazoletaComidas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosController : ControllerBase
    {
        public readonly string secretkey;
        public PlatosController(IConfiguration config) 
        {
            secretkey = config.GetSection("Settings").GetSection("SecretKey").ToString();
        }
       
        PlatosServicio PlatoServicio()
        {
            Db_Context db = new Db_Context();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7191");
            httpClient.DefaultRequestHeaders.Clear();
            PlatosRepositotio platosRepositotio = new PlatosRepositotio(db);
            RestauranteRepositorio restauranteRepositorio = new RestauranteRepositorio(db);
            RolesRepositorio rolesRepositorio = new RolesRepositorio(HttpContext, httpClient);
            PlatosServicio servicio = new PlatosServicio(platosRepositotio, restauranteRepositorio, rolesRepositorio);            
            return servicio;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPlatoAsync([FromBody] Platos plato)
        {
            try
            {              
                var servicio = PlatoServicio();
                await servicio.Agregar(plato, 0);
                return Ok("El plato se ingreso correctamente");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }            
        }

        [HttpPut]
        public async Task<IActionResult> EditarPlatoAsync([FromBody] Platos plato)
        {
            try
            {              
                var servicio = PlatoServicio();
                await servicio.EditarAsync(plato, 0);
                return Ok("El plato se actualizo correctamente");

            } catch(Exception e)
            {
               return BadRequest(e.Message);
            }   
        }        
    }
}
