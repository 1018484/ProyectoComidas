using Aplicacion.Interfaces;
using Dominio.DTO;
using Microsoft.AspNetCore.Mvc;
using ProyectoPlazoletaComidasPlazoleta.Migrations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoPlazoletaComidasPlazoleta.Controllers
{
    /// <summary>
    /// Employee Controller
    /// </summary> 
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        /// <summary>
        /// Employee service
        /// </summary>
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// initialize Controller
        /// </summary>       
        /// <param name="employeeService">Restaurant Service</param>
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Add Restaurant
        /// </summary>
        /// <param name="filter">Filter</param> 
        /// <returns>Orders</returns>
        [HttpGet]
        [Route("ListarPedidos/{Paginacion}")]
        public async Task<List<PaginacionPedidos>> ListarPedidosAsync([FromBody] PedidsoFiltroDTO filter)
        {            
            var resultado = await _employeeService.ListOrders(filter);
            return resultado;         
            
        }

        /// <summary>
        /// assign orders
        /// </summary>
        /// <param name="Orders">Orders to assign</param>         
        [HttpPut]
        public  IActionResult AsignedOrder(List<Guid> Orders)
        {
            _employeeService.AssignOrder(Orders);
            return Ok("");
        }

        /// <summary>
        /// Change Status
        /// </summary>
        /// <param name="dto">Change Status</param>    
        [HttpPut]
        [Route("CambiarEstado")]
        public async Task<IActionResult> CambiarEstado([FromBody] CambiarEstados dto)
        {
            await _employeeService.StatusAsync(dto);
            return Ok("");
        }
    }
}
