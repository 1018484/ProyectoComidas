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

namespace Test
{
    public class HU7Test
    {
        [Fact]
        public async Task ValidaRolEditarPlato()
        {
            try
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
                    Rol = "3",
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
            }catch(Exception e)
            {
                Assert.Equal("usuario no tiene acceso para Editar un Plato", e.Message);
            }          
        }
        [Fact]
        public async Task ValidaPropietarioRestauranteEditarPlato()
        {
            try
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
                    DocumentoId = 101231
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
            }
            catch (Exception e)
            {
                Assert.Equal("El Usuario no puede Editar plato a otro restaurante", e.Message);
            }
        }
    }
}
