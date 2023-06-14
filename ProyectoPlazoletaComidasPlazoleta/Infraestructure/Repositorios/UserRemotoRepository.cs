using Dominio.Modelos;
using Dominio.DTO;
using Dominio.Repositorios;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;


namespace Infraestructure.Repositorios
{
    /// <summary>
    /// UserRemoto Repository httpClient   
    /// </summary>  
    public class UserRemotoRepository : IUsersRemotoRepository<Usuarios, int>
    {
        /// <summary>
        /// Http intance  
        /// </summary>  
        private readonly IHttpClientFactory _httpClient;

        /// <summary>
        /// Initialize Http intance  
        /// </summary>
        /// <param name="httpClient">httpClient.</param>
        public UserRemotoRepository(IHttpClientFactory httpClient)
        {            
            _httpClient = httpClient;
        }

        /// <summary>
        /// Get user  Remoto by id   
        /// </summary>  
        /// <param name="id">userid</param>
        /// <returns>User</returns>
        public async Task<Usuarios> GetUserID(int id)
        {
            try
            {                               
                var cliente = _httpClient.CreateClient("Usuarios");
                var response = await cliente.GetAsync($"/api/Users/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var conteenido = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(conteenido))
                    {
                        return null;
                    }
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                    };                    
                    
                    var resultado = JsonSerializer.Deserialize<Usuarios>(conteenido, options);
                    return resultado;
                }

                return null;               

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }       
    }
}
