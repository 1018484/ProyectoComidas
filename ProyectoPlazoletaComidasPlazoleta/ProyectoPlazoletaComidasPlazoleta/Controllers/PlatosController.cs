using Aplicacion.Repositorio;
using Dominio.Modelos;
using Infraestructure.Repositorios;
using infrastructure.Context;
using infrastructure.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Dominio.Modelos.DTO;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlazoletaComidas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosController : ControllerBase
    {
        public readonly string secretkey;
        public PlatosController(IConfiguration config) 
        {
            secretkey = config.GetSection("Settings").GetSection("SecretKey").ToString();
        }
       
        PlatosServicio PlatoServicio()
        {
            Db_Context db = new Db_Context();            
            PlatosRepositotio platosRepositotio = new PlatosRepositotio(db);
            RestauranteRepositorio restauranteRepositorio = new RestauranteRepositorio(db);
            PlatosServicio servicio = new PlatosServicio(platosRepositotio, restauranteRepositorio);
            return servicio;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPlatoAsync([FromBody] Platos plato)
        {
            try
            {
                var getClaims = await getToken();
                if (getClaims == null)
                {
                    return BadRequest("El usuario no ha iniciado sesion");
                }

                if (int.Parse(getClaims.Rol) != (int)EnumRoles.Propietario)
                {
                    return Ok("usuario no tiene acceso para crear un Plato");
                }

                var servicio = PlatoServicio();
                servicio.Agregar(plato, int.Parse(getClaims.Id));
                return Ok("Se ingreso correctamente");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }            
        }

        [HttpPut]
        public async Task<IActionResult> EditarPlatoAsync([FromBody] Platos plato)
        {
            try
            {
                var getClaims = await getToken();
                if (getClaims == null)
                {
                    return BadRequest("El usuario no ha iniciado sesion");
                }

                if (int.Parse(getClaims.Rol) != (int)EnumRoles.Propietario)
                {
                    return BadRequest("usuario no tiene acceso para Editar un Plato");
                }

                var servicio = PlatoServicio();
                servicio.Editar(plato, int.Parse(getClaims.Id));
                return Ok("Se ingreso correctamente");

            } catch(Exception e)
            {
               return BadRequest(e.Message);
            }   
        }

        private async Task<UsuarioClaims> getToken()
        {
            var Token = await HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
            if (string.IsNullOrEmpty(Token))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretkey);
            try
            {
                tokenHandler.ValidateToken(Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,                    
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                UsuarioClaims claims = new UsuarioClaims()
                {
                    Rol = jwtToken.Claims.First(x => x.Type == "Rol").Value,
                    Id = jwtToken.Claims.First(x => x.Type == "ID").Value,
                    Correo = jwtToken.Claims.First(x => x.Type == "Correo").Value
                };

                return claims;
            }
            catch
            {
                return null;
            }

        }
    }
}
