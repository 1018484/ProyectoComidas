using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dominio.Modelos.DTO;
using Dominio.Repositorios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Infraestructure.Repositorios
{
    public class RolesRepositorio : IRoles
    {
        private readonly HttpContext httpContext;

        public RolesRepositorio(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }

        public async Task<UsuarioClaims> getToken()
        {
            var Token = await httpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
            if (string.IsNullOrEmpty(Token))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("J4im3OsorioTok3n");
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

        public string RolClaims()
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            string rol = "0";
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return rol;
                }

                rol = identity.Claims.FirstOrDefault(x => x.Type == "Rol").Value.ToString();
                return rol;
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }
    }
}
