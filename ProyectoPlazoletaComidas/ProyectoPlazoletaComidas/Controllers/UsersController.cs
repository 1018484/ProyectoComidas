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
using Dominio.Modelos.DTO;


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

        [HttpGet]        
        public ActionResult<List<UserDTO>> ListUsers()
        {
            return _userService.GetAll();            
        }
        
        [HttpGet("{id}")]
        public ActionResult<UserDTO> ListUsesByID(int id)
        {            
            return _userService.GetById(id);
        }

        [HttpPost]
        [Route("Owner")]
        [Authorize(Roles = "1")]
        public IActionResult CreateOwner([FromBody] UserDTO user)
        {                           
            _userService.AddOwner(user);
            return Ok();                       
        }
        
        [HttpPost]
        [Route("Employee")]
        [Authorize(Roles = "2")]
        public  IActionResult CreateEmployee([FromBody] UserDTO user)
        {            
            _userService.AddEmployee(user);
            return Ok();            
        }

        [HttpPost]
        [Route("Client")]        
        public IActionResult CreateClient([FromBody] UserDTO user)
        {                                       
            _userService.AddUser(user);
            return Ok();                    
        }
    }
}
