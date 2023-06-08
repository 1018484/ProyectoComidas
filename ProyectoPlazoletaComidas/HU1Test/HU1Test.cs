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
using Dominio.Repositorios;
using Moq;
using System.Security.Claims;
using Dominio.Modelos.DTO;

namespace HU1Test
{
    public class HU1Test
    {        

        [Fact]
        public async Task PruebaInserciondeUsuarios()
        {          

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

            UsuarioClaims claims = new UsuarioClaims()
            {
                Rol = "1",
                Id = "10184841",
                Correo = "mapu@gmail.com"
            };

            Mock<IRoles> Roles = new Mock<IRoles>();
            Mock<IRepositorioBase<Usuarios, int>> bd = new Mock<IRepositorioBase<Usuarios, int>>();
            Mock<IRepositorioRestauranteEmpleados<RestauranteEmpleados, int>> repoRestauranteEmpleado = new Mock<IRepositorioRestauranteEmpleados<RestauranteEmpleados, int>>();
            Roles.Setup(x => x.RolClaims()).Returns(claims);
            bd.Setup(x=> x.Agregar(usuarios)).Returns(usuarios);
            UsuariosServicio servicio = new UsuariosServicio(bd.Object , Roles.Object, repoRestauranteEmpleado.Object);
            var result = servicio.AgregarPropietario(usuarios);
            Assert.NotNull(result);
            Assert.Equal(usuarios.DocumentoId, result.DocumentoId);
            Assert.Equal("2", result.RolesRolId.ToString());
        }

        [Fact]
        public async Task PruebaValidacionondeCorreo()
        {
            try
            {
                Usuarios usuarios = new Usuarios()
                {
                    DocumentoId = 121212121,
                    Nombre = "liana",
                    Apellido = "fonseca",
                    Celular = "+5712312",
                    Correo = "li@hotm@ail.c9m",
                    Clave = "1234",
                    RolesRolId = 2
                };

                UsuarioClaims claims = new UsuarioClaims()
                {
                    Rol = "1",
                    Id = "10184841",
                    Correo = "mapu@gmail.com"
                };

                Mock<IRoles> Roles = new Mock<IRoles>();
                Mock<IRepositorioBase<Usuarios, int>> bd = new Mock<IRepositorioBase<Usuarios, int>>();
                Mock<IRepositorioRestauranteEmpleados<RestauranteEmpleados, int>> repoRestauranteEmpleado = new Mock<IRepositorioRestauranteEmpleados<RestauranteEmpleados, int>>();
                Roles.Setup(x => x.RolClaims()).Returns(claims);
                bd.Setup(x => x.Agregar(usuarios)).Returns(usuarios);
                UsuariosServicio servicio = new UsuariosServicio(bd.Object, Roles.Object, repoRestauranteEmpleado.Object);
                var result = servicio.AgregarPropietario(usuarios);

            }
            catch(Exception e)
            {
                Assert.Equal("Correo Invalido", e.Message);
            }           
        }

        [Fact]
        public async Task PruebaValidacionondeTelefono()
        {
            try
            {
                Usuarios usuarios = new Usuarios()
                {
                    DocumentoId = 121212121,
                    Nombre = "liana",
                    Apellido = "fonseca",
                    Celular = "+57712213R4",
                    Correo = "li@gmail.com",
                    Clave = "1234",
                    RolesRolId = 2
                };

                UsuarioClaims claims = new UsuarioClaims()
                {
                    Rol = "1",
                    Id = "10184841",
                    Correo = "mapu@gmail.com"
                };

                Mock<IRoles> Roles = new Mock<IRoles>();
                Mock<IRepositorioBase<Usuarios, int>> bd = new Mock<IRepositorioBase<Usuarios, int>>();
                Mock<IRepositorioRestauranteEmpleados<RestauranteEmpleados, int>> repoRestauranteEmpleado = new Mock<IRepositorioRestauranteEmpleados<RestauranteEmpleados, int>>();
                Roles.Setup(x => x.RolClaims()).Returns(claims);
                bd.Setup(x => x.Agregar(usuarios)).Returns(usuarios);
                UsuariosServicio servicio = new UsuariosServicio(bd.Object, Roles.Object, repoRestauranteEmpleado.Object);
                var result = servicio.AgregarPropietario(usuarios);

            }
            catch (Exception e)
            {
                Assert.Equal("Numero telefonico Invalido", e.Message);
            }                     
        }


        [Fact]
        public async Task PruebaCamposvacios()
        {
            try
            {
                Usuarios usuarios = new Usuarios()
                {
                    DocumentoId = 121212121,
                    Nombre = "liana",
                    Apellido = "",
                    Celular = "+57712213R4",
                    Correo = "li@gmail.com",
                    Clave = "1234",
                    
                };

                UsuarioClaims claims = new UsuarioClaims()
                {
                    Rol = "1",
                    Id = "10184841",
                    Correo = "mapu@gmail.com"
                };

                Mock<IRoles> Roles = new Mock<IRoles>();
                Mock<IRepositorioBase<Usuarios, int>> bd = new Mock<IRepositorioBase<Usuarios, int>>();
                Mock<IRepositorioRestauranteEmpleados<RestauranteEmpleados, int>> repoRestauranteEmpleado = new Mock<IRepositorioRestauranteEmpleados<RestauranteEmpleados, int>>();
                Roles.Setup(x => x.RolClaims()).Returns(claims);
                bd.Setup(x => x.Agregar(usuarios)).Returns(usuarios);
                UsuariosServicio servicio = new UsuariosServicio(bd.Object, Roles.Object, repoRestauranteEmpleado.Object);
                var result = servicio.AgregarPropietario(usuarios);

            }
            catch (Exception e)
            {
                Assert.Equal("Campos nulos", e.Message);
            }
        }
    }
}