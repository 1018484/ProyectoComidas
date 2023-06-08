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
using Dominio.Repositorios;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Plazoleta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantesController : ControllerBase
    {
        private readonly string secretkey;        

        private readonly IRestaruranteServicio _restaruranteServicio;

        public RestaurantesController(IConfiguration config, IRestaruranteServicio restaruranteServicio)
        {
            secretkey = config.GetSection("Settings").GetSection("SecretKey").ToString();            
            _restaruranteServicio = restaruranteServicio;
        }        

        [HttpPost]
        public async Task<IActionResult> CrearRestauranteAsync([FromBody] RestaurantesDTO restaurante)
        {
            try
            {                         
                await _restaruranteServicio.Agregar(restaurante);
                return Ok("Restaurante agregado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]

        public ActionResult<int> ObtenerRestauranteNIT(int id)
        {
            var Restaurante =_restaruranteServicio.ObtenerRestauranteNIt_ID(id);
            return Restaurante.NIT_Id;
        }


    }
}
