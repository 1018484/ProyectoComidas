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


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlazoletaComidas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class UsuariosController : ControllerBase    
    {
        //private readonly IUsuarioServicio _usuarioServicio;

        //private readonly IRepositorioBase<Usuarios, int> _repositorioBase;

        //private UsuariosController(IUsuarioServicio usuarioServicio, IRepositorioBase<Usuarios, int> repositorioBase)
        //{
        //    _usuarioServicio = usuarioServicio;
        //    _repositorioBase = repositorioBase;
        //}
        UsuariosServicio UsuariosServicios()
        {
            Db_Context db = new Db_Context();
            UsuariosRepository usuariosRepository = new UsuariosRepository(db);
            UsuariosServicio usuariosServicio = new UsuariosServicio(usuariosRepository);
            return usuariosServicio;
        }

        [HttpGet]        
        public ActionResult<List<Usuarios>> ListarUsuarios()
        {
            var servicio = UsuariosServicios();            
            return servicio.ObtenerTodos();
        }
        
        [HttpGet("{id}")]
        public ActionResult<Usuarios> ListarUsuariosPorID(int id)
        {
            var servicio = UsuariosServicios();
            return servicio.obtener(id);
        }

        
        [HttpPost]
        [Route("Propietario")]        
        public IActionResult CrearUsuarioPropietario([FromBody] Usuarios usuario)
        {
            try
            {
                //Validaciones val = new Validaciones();
                //var identity = HttpContext.User.Identity as ClaimsIdentity;
                //var rol = val.validartoken(identity);
                //if (rol != "1")
                //{
                //    return BadRequest("usuario no tiene acceso para crear un Usuario Propietario");
                //}

                usuario.Clave = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);
                var servicio = UsuariosServicios();
                servicio.AgregarPropietario(usuario);
                return Ok("Se ingreso correctamente");
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }            
        }
        
        [HttpPost]
        [Route("Empleados")]        
        public IActionResult CrearUsuarioEmpleado([FromBody] Usuarios usuario)
        {
            try
            {
                Validaciones val = new Validaciones();
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var rol = val.validartoken(identity);
                if (rol != "2")
                {
                    return BadRequest("usuario no tiene acceso para crear un Usuario Empleado");
                }

                usuario.Clave = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);
                var servicio = UsuariosServicios();
                servicio.AgregarEmpleado(usuario);
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
                usuario.Clave = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);
                var servicio = UsuariosServicios();
                servicio.AgregarUsuario(usuario);
                return Ok("Se ingreso correctamente");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }           
        }
    }
}
