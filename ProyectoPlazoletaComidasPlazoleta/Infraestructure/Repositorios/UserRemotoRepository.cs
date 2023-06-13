using Dominio.Modelos;
using Dominio.DTO;
using Dominio.Repositorios;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;


namespace Infraestructure.Repositorios
{
    public class UserRemotoRepository : IUsersRemotoRepository<Usuarios, int>
    {

        private readonly IHttpClientFactory _httpClient;

        public UserRemotoRepository(IHttpClientFactory httpClient)
        {            
            _httpClient = httpClient;
        }       

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
