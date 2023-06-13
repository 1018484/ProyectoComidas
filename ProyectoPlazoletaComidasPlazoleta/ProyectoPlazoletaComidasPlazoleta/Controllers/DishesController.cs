using Aplicacion.Repositorio;
using Dominio.Modelos;
using Infraestructure.Repositorios;
using infrastructure.Context;
using infrastructure.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Dominio.Repositorios;
using Aplicacion.Interfaces;
using Dominio.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlazoletaComidas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly string secretkey;

        private readonly IDishesService _dishService;

        public DishesController(IConfiguration config, IDishesService dishService) 
        {
            secretkey = config.GetSection("Settings").GetSection("SecretKey").ToString();            
            _dishService = dishService;
        } 

        [HttpPost]
        public async Task<IActionResult> CrearPlatoAsync([FromBody] PlatosDTO dish)
        {                    
            await _dishService.AddDish(dish);
            return Ok();                      
        }

        [HttpPut]
        public async Task<IActionResult> EditarPlatoAsync([FromBody] PlatosDTO dish)
        {                              
            await _dishService.EditDish(dish);
            return Ok("");  
        }        
    }
}
