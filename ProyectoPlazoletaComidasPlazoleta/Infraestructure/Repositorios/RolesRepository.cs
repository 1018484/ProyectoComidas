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
using System.Net.Http;
using Dominio.Modelos;
using System.Text.Json;

namespace Infraestructure.Repositorios
{
    public class RolesRepository : IRoles
    {       
        private readonly IHttpContextAccessor httpContext;

        private readonly IHttpClientFactory _cliente;

        public RolesRepository(IHttpContextAccessor httpContext, IHttpClientFactory cliente)
        {
            this.httpContext = httpContext;
            this._cliente = cliente;    
        }

        public async Task<UsuarioClaims> getToken()
        {
            var Token = await httpContext.HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
            if (string.IsNullOrEmpty(Token))
            {
                return null;
            }

            var httpClient =  _cliente.CreateClient("Usuarios");
            var result = await httpClient.GetAsync($"/api/Autenticacion/{Token}");
            if (result.IsSuccessStatusCode)
            {
                var contenido = await result.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(contenido))
                {
                    return null;
                }
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                };

                var resultado = JsonSerializer.Deserialize<UsuarioClaims>(contenido, options);
                return resultado;
            }

            return null;
        }
        
    }
}
