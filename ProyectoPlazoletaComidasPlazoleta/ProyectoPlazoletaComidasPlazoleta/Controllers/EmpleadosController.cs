using Aplicacion.Interfaces;
using Dominio.Modelos.DTO;
using Microsoft.AspNetCore.Mvc;
using ProyectoPlazoletaComidasPlazoleta.Migrations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoPlazoletaComidasPlazoleta.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadosServicio _empleadoServicio;

        public EmpleadosController(IEmpleadosServicio empleadoServicio)
        {
            _empleadoServicio = empleadoServicio;
        }

        [HttpGet]
        [Route("ListarPedidos/{Paginacion}")]
        public async Task<List<Dominio.Modelos.Pedidos>> ListarPedidosAsync([FromBody] PedidsoFiltroDTO filto)
        {
            
            var resultado = await _empleadoServicio.ListarPedidos(filto);
            return resultado;
            
            
            
        }
    }
}
