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

        private HttpClient _httpClient;        

        public UsuarioRepositorioRemoto(HttpClient httpClient)
        {            
            _httpClient = httpClient;
        }      
        public async Task<Usuarios> UsuarioID(int id)
        {
            try
            {               
                //string token = await Autenticacion(dto);
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);                
                var response = await _httpClient.GetAsync($"/api/Usuarios/{id}");
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

        //private async Task<string> Autenticacion(UsuarioDTO dto)
        //{
        //    var respuesta = await _httpClient.PostAsJsonAsync(@"/api/Autenticacion/InicioSesion", dto);
        //    return await respuesta.Content.ReadFromJsonAsync<string>();

        //}      


    }
}
