using Aplicacion.Interfaces;
using Aplicacion.Servicios;
using Dominio.Modelos;
using Dominio.Modelos.DTO;
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
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {       
        private readonly IClientService _clientService;       

        public ClientController(IClientService clientService)
        {            
            _clientService = clientService;            
        }

        [HttpGet]
        [Route("ListarRestaurantes/{Paginacion}")]
        public ActionResult<List<PaginacionRestaurantesDTO>> ListarRestaurantes(int Paginacion)
        {            
            return _clientService.ListRestaurants(Paginacion);
        }

        [HttpGet]
        [Route("ListarPlatos/{Paginacion}")]
        public ActionResult<List<PaginacionPlatosDTO>> ListarPlatos(int Paginacion)
        {                        
            return _clientService.ListDishes(Paginacion);
        }

        [HttpPost]
        public async Task<IActionResult> PedidosAsync([FromBody] PedidosDTO pedido)
        {            
            await _clientService.AddOrders(pedido);
            return Ok();
        }

    }
}
