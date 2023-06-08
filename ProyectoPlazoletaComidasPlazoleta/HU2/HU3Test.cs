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
using Aplicacion.Repositorio;
using Moq;
using Dominio.Repositorios;
using System;
using System.Runtime.InteropServices.ObjectiveC;

namespace HU2
{
    public class HU3Test
    {
        
        
        [Fact]
        public async Task CrearPlatos()
        {
            PlatosDTO platoDTO = new PlatosDTO()
            {
                NombrePlato = "Pollo sudado",
                Precio = 5000,
                Desacripcion = "Pollo sudado",
                URLImagen = "Pollosudado",
                Activo = true,
                Categoria = "Pollo",
                RestaurantesNIT_Id = 32235
            };

            Platos plato = new Platos()
            {
                NombrePlato = "Pollo sudado",
                Precio = 5000,
                Desacripcion = "Pollo sudado",
                URLImagen = "Pollosudado",
                Activo = true,
                Categoria = "Pollo",
                RestaurantesNIT_Id = 32235
            };

            UsuarioClaims claims = new UsuarioClaims()
            {
                Rol = "2",
                Id = "10184841",
                Correo = "mapu@gmail.com"
            };

            Restaurantes rest = new Restaurantes()
            {
                NIT_Id = 32235,
                Nombre = "la hambugueseria123",
                Direccion = "carrera33b#28-50",
                Telefono = "314993783",
                URLLogo = "mcpollo.img",
                DocumentoId = 10184841
            };

            Mock<IRepositorioPlatos<Platos, string, int>> platos = new Mock<IRepositorioPlatos<Platos, string, int>>();
            Mock<IRepositorioRestaurante<Restaurantes, int>> restaurente = new Mock<IRepositorioRestaurante<Restaurantes, int>>();
            Mock<IRoles> Roles= new Mock<IRoles>();
            platos.Setup(x => x.Agregar(plato)).Returns(plato);
            Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
            restaurente.Setup(x => x.obtener(plato.RestaurantesNIT_Id)).Returns(rest);
            PlatosServicio servicio = new PlatosServicio(platos.Object, restaurente.Object, Roles.Object);           
            var resultado = await servicio.Agregar(platoDTO, 0);
            Assert.Equal(plato.NombrePlato, resultado.NombrePlato);
            Assert.Equal(plato.Activo.ToString(), resultado.Activo.ToString());
        }

        [Fact]
        public async Task ValidaRolPropietario()
        {
            try
            {
                PlatosDTO platoDTO = new PlatosDTO()
                {
                    NombrePlato = "Pollo sudado",
                    Precio = 5000,
                    Desacripcion = "Pollo sudado",
                    URLImagen = "Pollosudado",
                    Activo = true,
                    Categoria = "Pollo",
                    RestaurantesNIT_Id = 32235
                };

                Platos plato = new Platos()
                {
                    NombrePlato = "Pollo sudado",
                    Precio = 5000,
                    Desacripcion = "Pollo sudado",
                    URLImagen = "Pollosudado",
                    Activo = true,
                    Categoria = "Pollo",
                    RestaurantesNIT_Id = 32235
                };

                UsuarioClaims claims = new UsuarioClaims()
                {
                    Rol = "1",
                    Id = "10184841",
                    Correo = "mapu@gmail.com"
                };

                Restaurantes rest = new Restaurantes()
                {
                    NIT_Id = 32235,
                    Nombre = "la hambugueseria123",
                    Direccion = "carrera33b#28-50",
                    Telefono = "314993783",
                    URLLogo = "mcpollo.img",
                    DocumentoId = 10184841
                };

                Mock<IRepositorioPlatos<Platos, string, int>> platos = new Mock<IRepositorioPlatos<Platos, string, int>>();
                Mock<IRepositorioRestaurante<Restaurantes, int>> restaurente = new Mock<IRepositorioRestaurante<Restaurantes, int>>();
                Mock<IRoles> Roles = new Mock<IRoles>();
                platos.Setup(x => x.Agregar(plato)).Returns(plato);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                restaurente.Setup(x => x.obtener(plato.RestaurantesNIT_Id)).Returns(rest);
                PlatosServicio servicio = new PlatosServicio(platos.Object, restaurente.Object, Roles.Object);
                var resultado = await servicio.Agregar(platoDTO, 0);
                Assert.Equal(plato.NombrePlato, resultado.NombrePlato);

            }catch(Exception e)
            {
                Assert.Equal("usuario no tiene acceso para crear un Plato", e.Message);
            }            
        }

        [Fact]
        public async Task ExisteRestaurante()
        {
            try
            {
                PlatosDTO platoDTO = new PlatosDTO()
                {
                    NombrePlato = "Pollo sudado",
                    Precio = 5000,
                    Desacripcion = "Pollo sudado",
                    URLImagen = "Pollosudado",
                    Activo = true,
                    Categoria = "Pollo",
                    RestaurantesNIT_Id = 32235
                };

                Platos plato = new Platos()
                {
                    NombrePlato = "Pollo sudado",
                    Precio = 5000,
                    Desacripcion = "Pollo sudado",
                    URLImagen = "Pollosudado",
                    Activo = true,
                    Categoria = "Pollo",
                    RestaurantesNIT_Id = 32235
                };

                UsuarioClaims claims = new UsuarioClaims()
                {
                    Rol = "2",
                    Id = "10184841",
                    Correo = "mapu@gmail.com"
                };

                Restaurantes rest = new Restaurantes()
                {
                    NIT_Id = 32235,
                    Nombre = "la hambugueseria123",
                    Direccion = "carrera33b#28-50",
                    Telefono = "314993783",
                    URLLogo = "mcpollo.img",
                    DocumentoId = 10184841
                };

                rest = null;
                Mock<IRepositorioPlatos<Platos, string, int>> platos = new Mock<IRepositorioPlatos<Platos, string, int>>();
                Mock<IRepositorioRestaurante<Restaurantes, int>> restaurente = new Mock<IRepositorioRestaurante<Restaurantes, int>>();
                Mock<IRoles> Roles = new Mock<IRoles>();
                platos.Setup(x => x.Agregar(plato)).Returns(plato);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                restaurente.Setup(x => x.obtener(plato.RestaurantesNIT_Id)).Returns(rest);
                PlatosServicio servicio = new PlatosServicio(platos.Object, restaurente.Object, Roles.Object);
                var resultado = await servicio.Agregar(platoDTO, 0);
                Assert.Equal(plato.NombrePlato, resultado.NombrePlato);

            }
            catch (Exception e)
            {
                Assert.Equal("El restaurante no existe", e.Message);
            }
        }

        [Fact]
        public async Task EditarPlato()
        {
            PlatosDTO platoDTO = new PlatosDTO()
            {
                NombrePlato = "Pollo sudado",
                Precio = 5000,
                Desacripcion = "Pollo sudado",

                Activo = false,
                RestaurantesNIT_Id = 32235
            };

            Platos plato = new Platos()
            {
                NombrePlato = "Pollo sudado",
                Precio = 5000,
                Desacripcion = "Pollo sudado",
                    
                Activo = false,                    
                RestaurantesNIT_Id = 32235
            };

            Platos platoconsultado = new Platos()
            {
                Id = 5,
                NombrePlato = "Pollo sudado",
                Precio = 5000,
                Desacripcion = "Pollo sudado",
                URLImagen = "Pollosudado",
                Activo = true,
                Categoria = "Pollo",
                RestaurantesNIT_Id = 32235
            };

            UsuarioClaims claims = new UsuarioClaims()
            {
                Rol = "2",
                Id = "10184841",
                Correo = "mapu@gmail.com"
            };

            Restaurantes rest = new Restaurantes()
            {
                NIT_Id = 32235,
                Nombre = "la hambugueseria123",
                Direccion = "carrera33b#28-50",
                Telefono = "314993783",
                URLLogo = "mcpollo.img",
                DocumentoId = 10184841
            };

            Platos original = new Platos()
            {
                Id = 5,
                NombrePlato = "Pollo sudado",
                Precio = 5000,
                Desacripcion = "Pollo sudado",
                URLImagen = "Pollosudado",
                Activo = true,
                Categoria = "Pollo",
                RestaurantesNIT_Id = 32235
            };                       
            Mock<IRepositorioPlatos<Platos, string, int>> platos = new Mock<IRepositorioPlatos<Platos, string, int>>();
            Mock<IRepositorioRestaurante<Restaurantes, int>> restaurente = new Mock<IRepositorioRestaurante<Restaurantes, int>>();
            Mock<IRoles> Roles = new Mock<IRoles>();
            platos.Setup(x => x.Agregar(plato)).Returns(plato);
            platos.Setup(x => x.ConsultarPlatoPorRestaurante(plato.NombrePlato, plato.RestaurantesNIT_Id)).Returns(platoconsultado);
            Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
            restaurente.Setup(x => x.obtener(plato.RestaurantesNIT_Id)).Returns(rest);            
            PlatosServicio servicio = new PlatosServicio(platos.Object, restaurente.Object, Roles.Object);
            var resultado = await servicio.EditarAsync(platoDTO, 0);
            Assert.Equal(plato.NombrePlato, resultado.NombrePlato);
            Assert.NotEqual(original.Activo, resultado.Activo);            
        }
    }
}
