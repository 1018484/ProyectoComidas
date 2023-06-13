using Applicacion.Interfaces;
using Dominio.DTO;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Plazoleta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly string secretkey;        

        private readonly IRestarurantService _restarurantService;

        public RestaurantController(IConfiguration config, IRestarurantService restaurantService)
        {
            secretkey = config.GetSection("Settings").GetSection("SecretKey").ToString();            
            _restarurantService = restaurantService;
        }        

        [HttpPost]
        public async Task<IActionResult> CrearRestauranteAsync([FromBody] RestaurantesDTO rest)
        {                                    
                await _restarurantService.AddRestaurant(rest);
                return Ok();
        }

        [HttpPost("{EmpleadoId}")]
        public async Task<IActionResult> CrearEmpleadoRestauranteAsync(int EmployeeId)
        {
            await _restarurantService.AddEmployeeRestaurant(EmployeeId);
            return Ok();
        }
    }
}
