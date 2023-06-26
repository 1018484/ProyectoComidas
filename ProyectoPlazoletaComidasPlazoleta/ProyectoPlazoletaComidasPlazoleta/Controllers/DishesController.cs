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
    /// <summary>
    /// Dish Controller
    /// </summary> 
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase   
    {
        /// <summary>
        /// Dish service
        /// </summary>
        private readonly IDishesService _dishService;

        /// <summary>
        /// initialize Controller
        /// </summary>       
        /// <param name="dishService">Dish Service</param>      
        public DishesController(IDishesService dishService) 
        {                       
            _dishService = dishService;
        }

        /// <summary>
        /// Add Dish
        /// </summary>       
        /// <param name="dish">DishSTO</param>     
        [HttpPost]
        public async Task<IActionResult> CrearPlatoAsync([FromBody] PlatosDTO dish)
        {
            try
            {
                await _dishService.AddDish(dish);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
                                
        }

        /// <summary>
        /// Edit Dish
        /// </summary>       
        /// <param name="dish">DishSTO</param> 
        [HttpPut]
        public async Task<IActionResult> EditarPlatoAsync([FromBody] PlatosDTO dish)
        {
            try
            {
                await _dishService.EditDish(dish);
                return Ok("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }        
    }
}
