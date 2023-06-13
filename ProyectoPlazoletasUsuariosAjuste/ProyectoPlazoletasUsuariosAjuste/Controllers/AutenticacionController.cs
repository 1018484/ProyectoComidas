using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aplicacion.Interfaces;
using Dominio.Modelos;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using Aplicacion.Interfaces;
using Dominio.DTO;

namespace PlazoletaComidas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly string secretkey;

        public AutenticacionController(IConfiguration config, IUserService usuarioServicio)
        {
            secretkey = config.GetSection("Settings").GetSection("SecretKey").ToString();
            _userService = usuarioServicio;
        }

        [HttpPost]
        [Route("StartSesion")]
        public IActionResult StartSesion([FromBody] UserLogin usuario)
        {
            var _usuario = _userService.Password(usuario);
            if (_usuario != null)
            {
                var KeyBytes = Encoding.ASCII.GetBytes(secretkey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim("ID", _usuario.DocumentoId.ToString()));
                claims.AddClaim(new Claim("Correo", _usuario.Correo.ToString()));
                claims.AddClaim(new Claim(ClaimTypes.Role, _usuario.RolesRolId.ToString()));
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
        public ActionResult<UsuarioClaims> TokenAuthentication(string token)
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
                UsuarioClaims claims = new UsuarioClaims()
                {
                    Rol = jwtToken.Claims.First(x => x.Type == "role").Value,
                    Id = jwtToken.Claims.First(x => x.Type == "ID").Value,
                    Email = jwtToken.Claims.First(x => x.Type == "Correo").Value
                };

                return claims;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }

}
