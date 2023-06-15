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
    /// <summary>
    /// Autenticacion Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        /// <summary>
        /// User Service
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        ///  JWT Tokn SecretKey
        /// </summary>
        private readonly string secretkey;

        /// <summary>
        /// initialize Controller
        /// </summary>
        /// <param name="config">config</param>
        /// <param name="userService">user Service</param>
        public AutenticacionController(IConfiguration config, IUserService userService)
        {
            secretkey = config.GetSection("Settings").GetSection("SecretKey").ToString();
            _userService = userService;
        }

        /// <summary>
        /// Start Sesion and create token
        /// </summary>
        /// <param name="userLogin">config</param>
        /// <returns>Token result</returns>     
        [HttpPost]
        [Route("StartSesion")]
        public IActionResult StartSesion([FromBody] UserLogin userLogin)
        {
            var user = _userService.Password(userLogin);
            if (user != null)
            {
                var KeyBytes = Encoding.ASCII.GetBytes(secretkey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim("ID", user.DocumentoId.ToString()));
                claims.AddClaim(new Claim("Correo", user.Correo.ToString()));
                claims.AddClaim(new Claim(ClaimTypes.Role, user.RolesRolId.ToString()));
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

        /// <summary>
        /// Valid Token authentication
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>User logged in</returns>    
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
