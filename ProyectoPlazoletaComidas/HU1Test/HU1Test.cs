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

namespace HU1Test
{
    public class HU1Test 
    {        
        class apisubscripcion: WebApplicationFactory<Program> 
        {            
            protected override IHost CreateHost(IHostBuilder builder)
            {                
                return base.CreateHost(builder);
            }          
        }


        [Fact]
        public async Task PruebaInserciondeUsuarios()
        {
            apisubscripcion apisubscripcion = new apisubscripcion();
            HttpClient client = apisubscripcion.CreateClient();         
            Usuarios usuarios = new Usuarios()
            {
                DocumentoId = 121212121,
                Nombre ="liana",
                Apellido ="fonseca",
                Celular ="+5712312",
                Correo = "li@hotmail.com",
                Clave = "1234",
                RolesRolId = 2                
            };

            var usuario = JsonConvert.SerializeObject(usuarios);
            var buffer = System.Text.Encoding.UTF8.GetBytes(usuario);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PostAsync("api/Usuarios/Propietario", byteContent);
            var contenido = await result.Content.ReadAsStringAsync();
                       
            Assert.NotNull(result);
            Assert.Equal("Se ingreso correctamente", contenido.ToString());     
        }

        [Fact]
        public async Task PruebaValidacionondeCorreo()
        {
            apisubscripcion apisubscripcion = new apisubscripcion();
            HttpClient client = apisubscripcion.CreateClient();
            Usuarios usuarios = new Usuarios()
                {
                    DocumentoId = 1918519,
                    Nombre = "Hector",
                    Apellido = "Rojas",
                    Celular = "+573132408264",
                    Correo = "Hector@gmail.c9m",
                    Clave = "1234",
                    RolesRolId = 2
                };

            var usuario = JsonConvert.SerializeObject(usuarios);
            var buffer = System.Text.Encoding.UTF8.GetBytes(usuario);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PostAsync("api/Usuarios/Propietario", byteContent);
            var contenido = await result.Content.ReadAsStringAsync();
            Assert.Equal("Correo Invalido", contenido.ToString());          
                    
        }

        [Fact]
        public async Task PruebaValidacionondeTelefono()
        {
            apisubscripcion apisubscripcion = new apisubscripcion();
            HttpClient client = apisubscripcion.CreateClient();            
            Usuarios usuarios = new Usuarios()
            {
                DocumentoId = 1918219,
                Nombre = "Hector",
                Apellido = "pereira",
                Celular = "312t5fd312",
                Correo = "Hector@gmail.com",
                Clave = "1234",
                RolesRolId = 2
            };

            var usuario = JsonConvert.SerializeObject(usuarios);
            var buffer = System.Text.Encoding.UTF8.GetBytes(usuario);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PostAsync("api/Usuarios/Propietario", byteContent);
            var contenido = await result.Content.ReadAsStringAsync();
            Assert.Equal("Numero telefonico Invalido", contenido.ToString());            
        }


        [Fact]
        public async Task PruebaCamposvacios()
        {
            apisubscripcion apisubscripcion = new apisubscripcion();
            HttpClient client = apisubscripcion.CreateClient();
            Usuarios usuarios = new Usuarios()
            {
                DocumentoId = 1918219,
                Nombre = "Hector",
                Apellido = "Rojas",
                Celular = "",
                Correo = "Hector@gmail.com",
                Clave = "1234",
                RolesRolId = 2
            };

            var usuario = JsonConvert.SerializeObject(usuarios);
            var buffer = System.Text.Encoding.UTF8.GetBytes(usuario);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PostAsync("api/Usuarios/Propietario", byteContent);
            var contenido = await result.Content.ReadAsStringAsync();
            Assert.Contains("field is required", contenido);
        }
    }
}