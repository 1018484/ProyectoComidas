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
using Dominio.Repositorios;
using Aplicacion.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlazoletaComidas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosController : ControllerBase
    {
        private readonly string secretkey;

        private readonly IPlatosServicio<PlatosDTO, int> _platosServicio;

        public PlatosController(IConfiguration config, IPlatosServicio<PlatosDTO, int> platosServicio) 
        {
            secretkey = config.GetSection("Settings").GetSection("SecretKey").ToString();            
            _platosServicio = platosServicio;
        } 

        [HttpPost]
        public async Task<IActionResult> CrearPlatoAsync([FromBody] PlatosDTO plato)
        {
            try
            {                     
                await _platosServicio.Agregar(plato, 0);
                return Ok("El plato se ingreso correctamente");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }            
        }

        [HttpPut]
        public async Task<IActionResult> EditarPlatoAsync([FromBody] PlatosDTO plato)
        {
            try
            {                   
                await _platosServicio.EditarAsync(plato, 0);
                return Ok("El plato se actualizo correctamente");

            } catch(Exception e)
            {
               return BadRequest(e.Message);
            }   
        }        
    }
}
