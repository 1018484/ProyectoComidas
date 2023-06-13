using Aplicacion.Interfaces;
using Dominio.Modelos.DTO;
using Microsoft.AspNetCore.Mvc;
using ProyectoPlazoletaComidasPlazoleta.Migrations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoPlazoletaComidasPlazoleta.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("ListarPedidos/{Paginacion}")]
        public async Task<List<Dominio.Modelos.Pedidos>> ListarPedidosAsync([FromBody] PedidsoFiltroDTO filter)
        {            
            var resultado = await _employeeService.ListOrders(filter);
            return resultado;         
            
        }

        [HttpPut]
        public  IActionResult AsignedOrder(List<Guid> Orders)
        {
            _employeeService.AssignOrder(Orders);
            return Ok("");
        }       
    }
}
