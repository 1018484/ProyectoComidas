using Dominio.Modelos;
using Dominio.Repositorios;
using infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infraestructure.Repositorios
{
    public class RepositorioRestauranteEmpleados : IRepositorioRestauranteEmpleados<RestauranteEmpleados, int>
    {
        private Db_Context _context;

        private readonly IHttpClientFactory _httpClient;

        public RepositorioRestauranteEmpleados(Db_Context context, IHttpClientFactory httpClient)
        {   
            _context = context;         
            _httpClient = httpClient;
        }
        public RestauranteEmpleados Agregar(RestauranteEmpleados entidad)
        {
            _context.RestauranteEmpleados.Add(entidad);
            return entidad;
        }

        public void Confirmar()
        {
            _context.SaveChanges();
        }

        public RestauranteEmpleados obtener(int id)
        {
            return _context.RestauranteEmpleados.Where(x=> x.EmpleadoId == id).FirstOrDefault();
        }

        public List<RestauranteEmpleados> ObtenerTodos()
        {
            return _context.RestauranteEmpleados.ToList();
        }

        public async Task<int> GetrestauranteNIT(int id)
        {
            try
            {
                var cliente = _httpClient.CreateClient("Plazoleta");
                var response = await cliente.GetAsync($"/api/Restaurantes/{id}");
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
    }
}
