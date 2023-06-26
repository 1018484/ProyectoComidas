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
using Dominio.Modelos;
using System.Security.Principal;

namespace Infraestructure.Repositorios
{
    public class RolesRepository : IRolesRepository
    {
        private readonly IHttpContextAccessor httpContext;

        public RolesRepository(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }
        public UserClaims RolClaims()
        {
            var identity = httpContext.HttpContext.User.Identity as ClaimsIdentity;
            UserClaims claims = new UserClaims();
            string rol = "0";
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return null;
                }

                claims.Rol= identity.Claims.FirstOrDefault(x => x.Type == "Rol").Value.ToString();
                claims.Email = identity.Claims.FirstOrDefault(x => x.Type == "Correo").Value.ToString();
                claims.Id = identity.Claims.FirstOrDefault(x => x.Type == "ID").Value.ToString();
                return claims;
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }
    }
}
