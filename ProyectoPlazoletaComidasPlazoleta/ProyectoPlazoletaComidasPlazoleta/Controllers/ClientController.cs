using Aplicacion.Interfaces;
using Aplicacion.Servicios;
using Dominio.DTO;
using Dominio.Modelos;
using Dominio.Repositorios;
using Infraestructure.Repositorios;
using infrastructure.Context;
using infrastructure.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoPlazoletaComidasPlazoleta.Controllers
{
    /// <summary>
    /// Dish Controller
    /// </summary> 
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        /// <summary>
        /// client service
        /// </summary>
        private readonly IClientService _clientService;

        /// <summary>
        /// initialize Controller
        /// </summary>       
        /// <param name="clientService">Client Service</param> 
        public ClientController(IClientService clientService)
        {            
            _clientService = clientService;            
        }

        /// <summary>
        /// list restaurant
        /// </summary>       
        /// <param name="pag">data for page</param> 
        /// <returns>list Restaurants</returns>
        [HttpGet]
        [Route("ListarRestaurantes/{pag}")]
        public ActionResult<List<PaginacionRestaurantesDTO>> ListarRestaurantes(int pag)
        {
            try
            {
                return _clientService.ListRestaurants(pag);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }       
            
        }

        /// <summary>
        /// list Dishes
        /// </summary>       
        /// <param name="pag">data for page</param> 
        /// <returns>list Dishes</returns>
        [HttpGet]
        [Route("ListarPlatos/{pag}")]
        public ActionResult<List<PaginacionPlatosDTO>> ListarPlatos(int pag)
        {
            try
            {
                return _clientService.ListDishes(pag);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add Order
        /// </summary>       
        /// <param name="order">List Dishes</param> 
        /// <returns>list Dishes</returns>
        [HttpPost]
        public async Task<IActionResult> PedidosAsync([FromBody] SendOrder order)
        {
            try
            {
                await _clientService.AddOrders(order);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult CancelOrder(Guid id)
        {
            try
            {
                _clientService.CancelOrder(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }



    }
}
