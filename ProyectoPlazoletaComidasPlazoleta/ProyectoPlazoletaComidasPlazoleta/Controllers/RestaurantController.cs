using Applicacion.Interfaces;
using Dominio.DTO;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Plazoleta.Controllers
{
    /// <summary>
    /// Restaurant Controller
    /// </summary> 
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        /// <summary>
        /// Restaurant service
        /// </summary>
        private readonly IRestarurantService _restarurantService;

        /// <summary>
        /// initialize Controller
        /// </summary>        
        /// <param name="config">Config Service</param>
        /// <param name="restaurantService">Restaurant Service</param>
        public RestaurantController(IConfiguration config, IRestarurantService restaurantService)
        {                     
            _restarurantService = restaurantService;
        }

        /// <summary>
        /// Add Restaurant
        /// </summary>
        /// <param name="rest">RestaurantDto</param> 
        [HttpPost]
        public async Task<IActionResult> CrearRestauranteAsync([FromBody] RestaurantesDTO rest)
        {
            try
            {
                await _restarurantService.AddRestaurant(rest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Add Restaurant Employee
        /// </summary>
        /// <param name="EmployeeId">Employee ID</param>
        [HttpPost("{EmpleadoId}")]
        public async Task<IActionResult> CrearEmpleadoRestauranteAsync(int EmployeeId)
        {
            try
            {
                await _restarurantService.AddEmployeeRestaurant(EmployeeId);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
