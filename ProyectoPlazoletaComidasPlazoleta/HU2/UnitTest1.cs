using Applicacion.Repositorio;
using Dominio.Modelos;
using infrastructure.Context;
using infrastructure.Repositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using Dominio.Modelos.DTO;

namespace HU2
{
    public class UnitTest1
    {
        class apisubscripcion : WebApplicationFactory<Program>
        {
            protected override IHost CreateHost(IHostBuilder builder)
            {
                return base.CreateHost(builder);
            }
        }

        [Fact]
        public async Task CreaRestaurante()
        {
            apisubscripcion apisubscripcion = new apisubscripcion();
            HttpClient client = apisubscripcion.CreateClient();
            RestaurantesDTO restaurantesDTO = new RestaurantesDTO()
            {
                NIT_Id = 232,
                Nombre = "la hambugueseria123",
                Direccion = "carrera33b#28-50",
                Telefono = "314993783",
                URLLogo = "mcpollo.img",
                DocumentoId = 9855574
            };

            var restaurante = JsonConvert.SerializeObject(restaurantesDTO);
            var buffer = System.Text.Encoding.UTF8.GetBytes(restaurante);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PostAsync("api/Restaurantes", byteContent);
            var contenido = await result.Content.ReadAsStringAsync();
            Assert.NotNull(result);
            Assert.Equal("Restaurante agregado", contenido.ToString());
        }

        [Fact]
        public async Task Usuarionoexiste()
        {
            apisubscripcion apisubscripcion = new apisubscripcion();
            HttpClient client = apisubscripcion.CreateClient();
            RestaurantesDTO restaurantesDTO = new RestaurantesDTO()
            {
                NIT_Id = 2121,
                Nombre = "los pollos hermanos",
                Direccion = "carrera33b#28-50",
                Telefono = "+573122222",
                URLLogo = "mcpollo.img",
                DocumentoId = 1111
            };

            var restaurante = JsonConvert.SerializeObject(restaurantesDTO);
            var buffer = System.Text.Encoding.UTF8.GetBytes(restaurante);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PostAsync("api/Restaurantes", byteContent);
            var contenido = await result.Content.ReadAsStringAsync();
            Assert.NotNull(result);
            Assert.Equal("Se esta intentando agregar un restaurante a un Usuario no registrado", contenido.ToString());
        }

        [Fact]
        public async Task validaNumerotelefonico()
        {
            apisubscripcion apisubscripcion = new apisubscripcion();
            HttpClient client = apisubscripcion.CreateClient();
            RestaurantesDTO restaurantesDTO = new RestaurantesDTO()
            {
                NIT_Id = 4444,
                Nombre = "McPollo",
                Direccion = "carrera33b#28-50",
                Telefono = "314td993783",
                URLLogo = "mcpollo.img",
                DocumentoId = 1111
            };

            var restaurante = JsonConvert.SerializeObject(restaurantesDTO);
            var buffer = System.Text.Encoding.UTF8.GetBytes(restaurante);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PostAsync("api/Restaurantes", byteContent);
            var contenido = await result.Content.ReadAsStringAsync();
            Assert.NotNull(result);
            Assert.Equal("Numero telefonico Invalido", contenido.ToString());
        }

        [Fact]
        public async Task validaNumerotelefonicotamaño()
        {
            apisubscripcion apisubscripcion = new apisubscripcion();
            HttpClient client = apisubscripcion.CreateClient();
            RestaurantesDTO restaurantesDTO = new RestaurantesDTO()
            {
                NIT_Id = 4444,
                Nombre = "McPollo",
                Direccion = "carrera33b#28-50",
                Telefono = "31499378311112",
                URLLogo = "mcpollo.img",
                DocumentoId = 1111
            };

            var restaurante = JsonConvert.SerializeObject(restaurantesDTO);
            var buffer = System.Text.Encoding.UTF8.GetBytes(restaurante);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PostAsync("api/Restaurantes", byteContent);
            var contenido = await result.Content.ReadAsStringAsync();
            Assert.NotNull(result);
            Assert.Equal("Numero telefonico Invalido", contenido.ToString());
        }

        [Fact]
        public async Task Telefonoaceptaindicador()
        {
            apisubscripcion apisubscripcion = new apisubscripcion();
            HttpClient client = apisubscripcion.CreateClient();
            RestaurantesDTO restaurantesDTO = new RestaurantesDTO()
            {
                NIT_Id = 34343,
                Nombre = "los pollos hermanos",
                Direccion = "carrera33b#28-50",
                Telefono = "+573122222222",
                URLLogo = "mcpollo.img",
                DocumentoId = 121212121
            };

            var restaurante = JsonConvert.SerializeObject(restaurantesDTO);
            var buffer = System.Text.Encoding.UTF8.GetBytes(restaurante);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PostAsync("api/Restaurantes", byteContent);
            var contenido = await result.Content.ReadAsStringAsync();
            Assert.NotNull(result);
            Assert.Equal("Restaurante agregado", contenido.ToString());
        }

        [Fact]
        public async Task Validanombrederestaurante()
        {
            apisubscripcion apisubscripcion = new apisubscripcion();
            HttpClient client = apisubscripcion.CreateClient();
            RestaurantesDTO restaurantesDTO = new RestaurantesDTO()
            {
                NIT_Id = 2121,
                Nombre = "123123",
                Direccion = "carrera33b#28-50",
                Telefono = "+573122222222",
                URLLogo = "mcpollo.img",
                DocumentoId = 121212121
            };

            var restaurante = JsonConvert.SerializeObject(restaurantesDTO);
            var buffer = System.Text.Encoding.UTF8.GetBytes(restaurante);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PostAsync("api/Restaurantes", byteContent);
            var contenido = await result.Content.ReadAsStringAsync();
            Assert.NotNull(result);
            Assert.Equal("Nombre de Restaurante Invalido", contenido.ToString());
        }


    }
}