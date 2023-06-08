using Microsoft.AspNetCore.Mvc;
using Applicacion.Interfaces;
using Applicacion.Repositorio;
using infrastructure.Context;
using infrastructure.Repositorios;
using Dominio.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Aplicacion.Validaciones;
using Dominio.Repositorios;
using Infraestructure.Repositorios;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlazoletaComidas.Controllers
{
    /// <summary>
    /// Esta es el controlador encargado de gestionar la insercion y consulta de Usuarios    
    /// <list type="bullet">
    /// <item>
    /// <term>ListarUsuarios</term>
    /// <description>Listado de usuarios</description>
    /// </item>
    /// <item>
    /// <term>ListarUsuariosPorID</term>
    /// <description>Devuelve un la consulta de un usuario por ID</description>
    /// </item>
    /// <item>
    /// <term>ObtenerEmpleado</term>
    /// <description>Devuelve el restaurante donde labora el empleado</description>
    /// </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// Esta clase sirve solo para los alumnos pero comparte el metodo
    /// MostrarNotas con los profesores.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]    
    public class UsuariosController : ControllerBase    
    {
        private readonly IUsuarioServicio _usuarioServicio;        

        public UsuariosController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;            
        }       

        [HttpGet]        
        public ActionResult<List<Usuarios>> ListarUsuarios()
        {
            return _usuarioServicio.ObtenerTodos();            
        }
        
        [HttpGet("{id}")]
        public ActionResult<Usuarios> ListarUsuariosPorID(int id)
        {            
            return _usuarioServicio.obtener(id);
        }

        [HttpGet("ObtenerRestauranteNIT/{id}")]
        public ActionResult<int> ObtenerRestauranteNIT(int id)
        {
            return _usuarioServicio.ObtenerRestauranteNIT(id);
        }


        [HttpPost]
        [Route("Propietario")]        
        public IActionResult CrearUsuarioPropietario([FromBody] Usuarios usuario)
        {
            try
            {                            
                _usuarioServicio.AgregarPropietario(usuario);
                return Ok("Se ingreso correctamente");
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }            
        }
        
        [HttpPost]
        [Route("Empleados")]        
        public async Task<IActionResult> CrearUsuarioEmpleadoAsync([FromBody] Usuarios usuario)
        {
            try
            {                                         
                await _usuarioServicio.AgregarEmpleado(usuario);
                return Ok("Se ingreso correctamente");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }

        [HttpPost]
        [Route("Cliente")]        
        public IActionResult CrearUsuarioCliente([FromBody] Usuarios usuario)
        {
            try
            {                            
                _usuarioServicio.AgregarUsuario(usuario);
                return Ok("Se ingreso correctamente");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }           
        }
    }
}
