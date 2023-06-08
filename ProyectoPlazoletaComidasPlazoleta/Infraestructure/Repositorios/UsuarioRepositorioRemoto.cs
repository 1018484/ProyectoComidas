using Dominio.Modelos;
using Dominio.Modelos.DTO;
using Dominio.Repositorios;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;


namespace Infraestructure.Repositorios
{
    public class UsuarioRepositorioRemoto : IRepositorioUsuariosRemoto<Usuarios, int>
    {

        private readonly IHttpClientFactory _httpClient;

        public UsuarioRepositorioRemoto(IHttpClientFactory httpClient)
        {            
            _httpClient = httpClient;
        }

        public async Task<int> ObtenerEmpleado(int id)
        {
            try
            {
                var cliente = _httpClient.CreateClient("Usuarios");
                var response = await cliente.GetAsync($"/api/Usuarios/ObtenerRestauranteNIT/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var conteenido = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(conteenido))
                    {
                        return 0;
                    }
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    var resultado = JsonSerializer.Deserialize<int>(conteenido, options);
                    return resultado;
                }

                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Usuarios> UsuarioID(int id)
        {
            try
            {                               
                var cliente = _httpClient.CreateClient("Usuarios");
                var response = await cliente.GetAsync($"/api/Usuarios/{id}");
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
