using Microsoft.AspNetCore.Mvc;
using Dominio.Modelos;
using Microsoft.AspNetCore.Authorization;
using Aplicacion.Interfaces;
using Dominio.DTO;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlazoletaComidas.Controllers
{
    /// <summary>
    /// Users Controller
    /// </summary> 
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// User Service
        /// </summary> 
        private readonly IUserService _userService;

        /// <summary>
        /// initialize Controller
        /// </summary>        
        /// <param name="userService">user Service</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>User</returns>  
        [HttpGet("{id}")]
        public ActionResult<Usuarios> ListUserByID(int id)
        {
            return _userService.GetById(id);        
        }

        /// <summary>
        /// Add Owner
        /// </summary>
        /// <param name="userDTO">User Dto</param>        
        [HttpPost]
        [Route("Propietario")]
        [Authorize(Roles = "1")]
        public IActionResult CreateOwner([FromBody] UsuarioDTO userDTO)
        {
            try
            {
                _userService.AddOwner(userDTO);
                return Ok("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add Employee
        /// </summary>
        /// <param name="userDTO">User Dto</param>  
        [HttpPost]
        [Route("Empleados")]
        [Authorize(Roles = "2")]
        public IActionResult CreateEmployee([FromBody] UsuarioDTO userDTO)
        {
            try
            {
                _userService.AddEmployee(userDTO);
                return Ok("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add client
        /// </summary>
        /// <param name="userDTO">User Dto</param> 
        [HttpPost]
        [Route("Cliente")]
        public IActionResult CreateClient([FromBody] UsuarioDTO userDTO)
        {
            try
            {
                _userService.AddUser(userDTO);
                return Ok("");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            } 
            
        }
    }
}