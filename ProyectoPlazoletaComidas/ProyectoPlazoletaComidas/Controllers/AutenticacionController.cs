using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Applicacion.Interfaces;
using Applicacion.Repositorio;
using infrastructure.Context;
using infrastructure.Repositorios;
using Dominio.Modelos;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Dominio.Modelos.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Infraestructure.Repositorios;
using Newtonsoft.Json;
using Dominio.Repositorios;

namespace PlazoletaComidas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IUserService _usuarioServicio;       

        private readonly string secretkey;

        public AutenticacionController(IConfiguration config, IUserService usuarioServicio)        
        {
            secretkey = config.GetSection("Settings").GetSection("SecretKey").ToString();
            _usuarioServicio = usuarioServicio;            
        }       

        [HttpPost]
        [Route("StartSesion")]
        public IActionResult StartSesion([FromBody] UserLogin usuario)
        {            
            var _usuario = _usuarioServicio.PasswordValidation(usuario);
            if (_usuario != null)
            {
                var KeyBytes = Encoding.ASCII.GetBytes(secretkey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim("ID", _usuario.DocumenId.ToString()));
                claims.AddClaim(new Claim("Correo", _usuario.Email.ToString()));
                claims.AddClaim(new Claim(ClaimTypes.Role, _usuario.RolsRolId.ToString()));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(KeyBytes), SecurityAlgorithms.HmacSha256Signature)

                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenconfig = tokenHandler.CreateToken(tokenDescriptor);
                var tokencreado = tokenHandler.WriteToken(tokenconfig);
                return Ok(tokencreado);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }


        [HttpGet("{token}")]        
        public ActionResult<UserClaims> TokenAuthentication(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretkey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                UserClaims claims = new UserClaims()
                {
                    Rol = jwtToken.Claims.First(x => x.Type == "role").Value,
                    Id = jwtToken.Claims.First(x => x.Type == "ID").Value,
                    Email = jwtToken.Claims.First(x => x.Type == "Correo").Value
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
