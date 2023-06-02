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
using Dominio.Repositorios;
using Moq;

namespace HU2
{
    public class HU2Test
    {
        class apisubscripcion : WebApplicationFactory<ProgramPlazoleta>
        {
            protected override IHost CreateHost(IHostBuilder builder)
            {
                return base.CreateHost(builder);
            }
        }

        [Fact]
        public async Task CreaRestaurante()
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
                Rol = "1",
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
            var result =  servicio.Agregar(restDTO);           

        }

        [Fact]
        public async Task Usuarionoexiste()
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
                    Rol = "1",
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

                usuarios = null;

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
                Assert.Equal("Se esta intentando agregar un restaurante a un Usuario no registrado", ex.Message);
            }            
        }

        [Fact]
        public async Task validaNumerotelefonico()
        {
            try
            {
                RestaurantesDTO restDTO = new RestaurantesDTO()
                {
                    NIT_Id = 32235,
                    Nombre = "la hambugueseria123",
                    Direccion = "carrera33b#28-50",
                    Telefono = "31ASA1293783",
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
                    Rol = "1",
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
                var result = await servicio.Agregar(restDTO);

            }
            catch (Exception ex)
            {
                Assert.Equal("Numero telefonico Invalido", ex.Message);
            }
        }

        [Fact]
        public async Task validaNumerotelefonicotamaño()
        {
            try
            {
                RestaurantesDTO restDTO = new RestaurantesDTO()
                {
                    NIT_Id = 32235,
                    Nombre = "la hambugueseria123",
                    Direccion = "carrera33b#28-50",
                    Telefono = "+571231231A293783",
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
                    Rol = "1",
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
                var result = await servicio.Agregar(restDTO);

            }
            catch (Exception ex)
            {
                Assert.Equal("Numero telefonico Invalido", ex.Message);
            }
        }

        [Fact]
        public async Task Telefonoaceptaindicador()
        {
           
            RestaurantesDTO restDTO = new RestaurantesDTO()
            {
                NIT_Id = 32235,
                Nombre = "la hambugueseria123",
                Direccion = "carrera33b#28-50",
                Telefono = "+573132408264",
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
                Rol = "1",
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
            var result = await servicio.Agregar(restDTO);
            Assert.Equal(null, result);
            
        }

        [Fact]
        public async Task Validanombrederestaurante()
        {
            try
            {
                RestaurantesDTO restDTO = new RestaurantesDTO()
                {
                    NIT_Id = 32235,
                    Nombre = "12312",
                    Direccion = "carrera33b#28-50",
                    Telefono = "+571234567891234",
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
                    Rol = "1",
                    Id = "10184841",
                    Correo = "mapu@gmail.com"
                };

                Usuarios usuarios = new Usuarios()
                {
                    DocumentoId = 10184841,
                    Nombre = "liana",
                    Apellido = "fonseca",
                    Celular = "+571234567891234",
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
                var result = await servicio.Agregar(restDTO);

            }
            catch (Exception ex)
            {
                Assert.Equal("Nombre de Restaurante Invalido", ex.Message);
            }            
        }

        [Fact]
        public async Task Validacamposnulos()
        {
            try
            {
                RestaurantesDTO restDTO = new RestaurantesDTO()
                {
                    Direccion = "carrera33b#28-50",
                    Telefono = "+571234567891234",
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
                    Rol = "1",
                    Id = "10184841",
                    Correo = "mapu@gmail.com"
                };

                Usuarios usuarios = new Usuarios()
                {
                    DocumentoId = 10184841,
                    Nombre = "liana",
                    Apellido = "fonseca",
                    Celular = "+571234567891234",
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
                var result = await servicio.Agregar(restDTO);
            }
            catch (Exception ex)
            {
                Assert.Equal("Campos nulos", ex.Message);
            }
        }
    }
}