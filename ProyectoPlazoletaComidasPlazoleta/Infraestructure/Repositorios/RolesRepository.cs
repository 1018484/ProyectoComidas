using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dominio.Repositorios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http;
using Dominio.Modelos;
using System.Text.Json;
using Dominio.DTO;

namespace Infraestructure.Repositorios
{
    /// <summary>
    /// Rol and authentication Repository httpClient   
    /// </summary>  
    public class RolesRepository : IRoles
    {
        /// <summary>
        /// httpt context   
        /// </summary> 
        private readonly IHttpContextAccessor httpContext;


        /// <summary>
        /// Http intance  
        /// </summary>  
        private readonly IHttpClientFactory _cliente;

        /// <summary>
        /// Initialize Db_Context
        /// </summary>
        /// <param name="httpContext">httpContext.</param>
        /// <param name="cliente">htttpClient</param>
        public RolesRepository(IHttpContextAccessor httpContext, IHttpClientFactory cliente)
        {
            this.httpContext = httpContext;
            this._cliente = cliente;    
        }

        /// <summary>
        /// session validation and authentication
        /// </summary> 
        /// <returns>User legged in</returns>
        public async Task<UsuarioClaims> getToken()
        {
            try
            {
                var Token = await httpContext.HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
                if (string.IsNullOrEmpty(Token))
                {
                    throw new Exception("token or session not started");
                }

                var httpClient = _cliente.CreateClient("Usuarios");
                var result = await httpClient.GetAsync($"/api/Autenticacion/{Token}");
                if (result.IsSuccessStatusCode)
                {
                    var contenido = await result.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(contenido))
                    {
                        throw new Exception("token or session not started");
                    }
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    var resultado = JsonSerializer.Deserialize<UsuarioClaims>(contenido, options);
                    return resultado;
                }

                throw new Exception("token or session not started");

            }
            catch (Exception ex)
            {
                throw;
            }
           
        }
        
    }
}
