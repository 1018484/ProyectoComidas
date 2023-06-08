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
    public class HU5Test
    {
        [Fact]
        public async Task CrearRestauranteValidaRol()
        {
            try
            {
                RestaurantesDTO restDTO = new RestaurantesDTO()
                {
                    NIT_Id = 32235,
                    Nombre = "la hambugueseria123",
                    Direccion = "carrera33b#28-50",
                    Telefono = "314993783",
                    URLLogo = "mcpollo.img",
                    DocumentoId = 10184841
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

                UsuarioClaims claims = new UsuarioClaims()
                {
                    Rol = "2",
                    Id = "10184841",
                    Correo = "mapu@gmail.com"
                };

                Usuarios usuarios = new Usuarios()
                {
                    DocumentoId = 10184841,
                    Nombre = "liana",
                    Apellido = "fonseca",
                    Celular = "+5712312",
                    Correo = "li@hotmail.com",
                    Clave = "1234",
                    RolesRolId = 2
                };

                Mock<IRepositorioRestaurante<Restaurantes, int>> restaurente = new Mock<IRepositorioRestaurante<Restaurantes, int>>();
                Mock<IRepositorioUsuariosRemoto<Usuarios, int>> Usuarios = new Mock<IRepositorioUsuariosRemoto<Usuarios, int>>();
                Mock<IRoles> Roles = new Mock<IRoles>();
                restaurente.Setup(x => x.Agregar(rest)).Returns(rest);
                Usuarios.Setup(x => x.UsuarioID(rest.DocumentoId)).ReturnsAsync(usuarios);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                RestauranteServicio servicio = new RestauranteServicio(restaurente.Object, Usuarios.Object, Roles.Object);
                var result = servicio.Agregar(restDTO);
            } 
            catch (Exception ex)
            {
                Assert.Equal("usuario no tiene acceso para crear un restaurante", ex.Message);
            }
        }

        [Fact]
        public async Task CrearPlatovalidaRol()
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
            }
            catch (Exception ex)
            {
                Assert.Equal("usuario no tiene acceso para crear un Plato", ex.Message);
            }         
        }

        [Fact]
        public async Task CrearPlatovalidaUsuarioPopietario()
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
                    Id = "20018929",
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
            }
            catch (Exception ex)
            {
                Assert.Equal("El Usuario no puede insertar plato a otro restaurante", ex.Message);
            }
        }
    }
}
