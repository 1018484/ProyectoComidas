using Microsoft.AspNetCore.Mvc;
using Dominio.Modelos;
using Microsoft.AspNetCore.Authorization;
using Aplicacion.Interfaces;
using Dominio.DTO;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlazoletaComidas.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }      

        [HttpGet("{id}")]
        public ActionResult<Usuarios> ListUserByID(int id)
        {
            return _userService.GetById(id);        
        }

        [HttpPost]
        [Route("Propietario")]
        [Authorize(Roles = "1")]
        public IActionResult CreateOwner([FromBody] UsuarioDTO usuario)
        {           
            _userService.AddOwner(usuario);
            return Ok("");           
        }

        [HttpPost]
        [Route("Empleados")]
        [Authorize(Roles = "2")]
        public IActionResult CreateEmployee([FromBody] UsuarioDTO usuario)
        {            
             _userService.AddEmployee(usuario);
            return Ok("");           
        }

        [HttpPost]
        [Route("Cliente")]
        public IActionResult CreateClient([FromBody] UsuarioDTO usuario)
        {           
            _userService.AddUser(usuario);
            return Ok("");            
            
        }
    }
}