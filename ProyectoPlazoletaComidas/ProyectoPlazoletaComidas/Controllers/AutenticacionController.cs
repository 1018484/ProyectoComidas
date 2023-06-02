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

namespace PlazoletaComidas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly string secretkey;

        public AutenticacionController(IConfiguration config)
        {
            secretkey = config.GetSection("Settings").GetSection("SecretKey").ToString();
        }

        UsuariosServicio CrearUsuarios()
        {
            Db_Context db = new Db_Context();
            UsuariosRepository usuariosRepository = new UsuariosRepository(db);
            RolesRepositorio rolesRepositorio = new RolesRepositorio(HttpContext);
            UsuariosServicio usuariosServicio = new UsuariosServicio(usuariosRepository, rolesRepositorio);
            return usuariosServicio;
        }


        [HttpPost]
        [Route("InicioSesion")]
        public IActionResult InicioSesion([FromBody] UsuarioDTO usuario)
        {
            var _service = CrearUsuarios();
            var _usuarios = _service.ObtenerTodos();
            var _usuario = _usuarios.Where(u => u.Correo == usuario.Correo).FirstOrDefault();
            if (_usuario != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(usuario.Contraseña, _usuario.Clave))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new
                    {
                        token = ""
                    });
                }

                var KeyBytes = Encoding.ASCII.GetBytes(secretkey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim("ID", _usuario.DocumentoId.ToString()));
                claims.AddClaim(new Claim("Correo", _usuario.Correo.ToString()));
                claims.AddClaim(new Claim("Rol", _usuario.RolesRolId.ToString()));
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
                return StatusCode(StatusCodes.Status401Unauthorized, new
                {
                    token = ""
                });
            }
        }


        [HttpGet("{token}")]        
        public ActionResult<UsuarioClaims> ValidadRol(string token)
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
