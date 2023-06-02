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
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Moq;
using Dominio.Repositorios;
using System.Runtime.ConstrainedExecution;

namespace HU1Test
{
    public class HU5
    {
        class apisubscripcion : WebApplicationFactory<ProgramUser>
        {
            protected override IHost CreateHost(IHostBuilder builder)
            {
                return base.CreateHost(builder);
            }
        }

        [Fact]
        public async Task IniciarSesionCorrecto()
        {
            apisubscripcion apisubscripcion = new apisubscripcion();
            HttpClient client = apisubscripcion.CreateClient();
            UsuarioDTO usuario = new UsuarioDTO()
            {
                Correo = "mapu@gmail.com",
                Contraseña = "1604"
            };

            var respuesta = await client.PostAsJsonAsync(@"/api/Autenticacion/InicioSesion", usuario);
            var contenido = await respuesta.Content.ReadAsStringAsync();
            Assert.NotNull(respuesta);
            Assert.Equal("OK", respuesta.ReasonPhrase.ToString());
        }

        [Fact]
        public async Task IniciarSesionuuUsuarionoexiste()
        {
            apisubscripcion apisubscripcion = new apisubscripcion();
            HttpClient client = apisubscripcion.CreateClient();
            UsuarioDTO usuario = new UsuarioDTO()
            {
                Correo = "mapchea@gmail.com",
                Contraseña = "1604"
            };

            var respuesta = await client.PostAsJsonAsync(@"/api/Autenticacion/InicioSesion", usuario);
            var contenido = await respuesta.Content.ReadAsStringAsync();
            Assert.NotNull(respuesta);
            Assert.Equal("Unauthorized", respuesta.ReasonPhrase.ToString());
        }


        [Fact]
        public async Task InvalidPassword()
        {
            apisubscripcion apisubscripcion = new apisubscripcion();
            HttpClient client = apisubscripcion.CreateClient();
            UsuarioDTO usuario = new UsuarioDTO()
            {
                Correo = "mapu@gmail.com",
                Contraseña = "1603"
            };

            var respuesta = await client.PostAsJsonAsync(@"/api/Autenticacion/InicioSesion", usuario);
            var contenido = await respuesta.Content.ReadAsStringAsync();
            Assert.NotNull(respuesta);
            Assert.Equal("Unauthorized", respuesta.ReasonPhrase.ToString());
        }

        //[Fact]
        public async Task ValidarCrearusuarioPropirtario()
        {
            UsuarioDTO usuariodto = new UsuarioDTO()
            {
                Correo = "mapu@gmail.com",
                Contraseña = "123"
            };

            Usuarios usuario = new Usuarios()
            {
                DocumentoId = 23234,
                Nombre = "Jorge",
                Apellido = "Velazques",
                Celular = "+573132408264",
                Correo = "li@hotmail.com",
                Clave = "1234",                
            };

            UsuarioClaims claims = new UsuarioClaims()
            {
                Rol = "1",
                Id = "10184841",
                Correo = "mapu@gmail.com"
            };
            Mock<IRoles> roles = new Mock<IRoles>();
            Mock<IRepositorioBase<Usuarios, int>> bd = new Mock<IRepositorioBase<Usuarios, int>>();
            roles.Setup(x => x.RolClaims()).Returns(claims.Rol);
            bd.Setup(x => x.Agregar(usuario)).Returns(usuario);
            UsuariosServicio usuariosServicio = new UsuariosServicio(bd.Object, roles.Object);
            var result =  usuariosServicio.AgregarPropietario(usuario);
            Assert.Equal(usuario.Nombre, result.Nombre);      
        }

        [Fact]
        public async Task ValidarCrearusuarioPropirtarioUsuariosinpermiso()
        {
            try
            {
                UsuarioDTO usuariodto = new UsuarioDTO()
                {
                    Correo = "jaime@gmail.com",
                    Contraseña = "123"
                };

                Usuarios usuario = new Usuarios()
                {
                    DocumentoId = 23234,
                    Nombre = "Jorge",
                    Apellido = "Velazques",
                    Celular = "+573132408264",
                    Correo = "li@hotmail.com",
                    Clave = "1234",
                };

                UsuarioClaims claims = new UsuarioClaims()
                {
                    Rol = "2",
                    Id = "10184841",
                    Correo = "mapu@gmail.com"
                };
                Mock<IRoles> roles = new Mock<IRoles>();
                Mock<IRepositorioBase<Usuarios, int>> bd = new Mock<IRepositorioBase<Usuarios, int>>();
                roles.Setup(x => x.RolClaims()).Returns(claims.Rol);
                bd.Setup(x => x.Agregar(usuario)).Returns(usuario);
                UsuariosServicio usuariosServicio = new UsuariosServicio(bd.Object, roles.Object);
                var result = usuariosServicio.AgregarPropietario(usuario);
                Assert.Equal(usuario.Nombre, result.Nombre);

            }
            catch (Exception ex) 
            {
                Assert.Equal("usuario no tiene acceso para crear un Usuario Propietario", ex.Message);

            }            
            
        }

        [Fact]
        public async Task ValidarCrearusuarioEmpleado()
        {
            UsuarioDTO usuariodto = new UsuarioDTO()
            {
                Correo = "mapu@gmail.com",
                Contraseña = "123"
            };

            Usuarios usuario = new Usuarios()
            {
                DocumentoId = 23234,
                Nombre = "Jorge",
                Apellido = "Velazques",
                Celular = "+573132408264",
                Correo = "li@hotmail.com",
                Clave = "1234",
            };

            UsuarioClaims claims = new UsuarioClaims()
            {
                Rol = "2",
                Id = "10184841",
                Correo = "mapu@gmail.com"
            };
            Mock<IRoles> roles = new Mock<IRoles>();
            Mock<IRepositorioBase<Usuarios, int>> bd = new Mock<IRepositorioBase<Usuarios, int>>();
            roles.Setup(x => x.RolClaims()).Returns(claims.Rol);
            bd.Setup(x => x.Agregar(usuario)).Returns(usuario);
            UsuariosServicio usuariosServicio = new UsuariosServicio(bd.Object, roles.Object);
            var result = usuariosServicio.AgregarEmpleado(usuario);
            Assert.Equal(usuario.Nombre, result.Nombre);
        }

        [Fact]
        public async Task ValidarCrearusuarioEmpleadoSinPermiso()
        {
            try
            {
                UsuarioDTO usuariodto = new UsuarioDTO()
                {
                    Correo = "jaime@gmail.com",
                    Contraseña = "123"
                };

                Usuarios usuario = new Usuarios()
                {
                    DocumentoId = 23234,
                    Nombre = "Jorge",
                    Apellido = "Velazques",
                    Celular = "+573132408264",
                    Correo = "li@hotmail.com",
                    Clave = "1234",
                };

                UsuarioClaims claims = new UsuarioClaims()
                {
                    Rol = "2",
                    Id = "10184841",
                    Correo = "mapu@gmail.com"
                };
                Mock<IRoles> roles = new Mock<IRoles>();
                Mock<IRepositorioBase<Usuarios, int>> bd = new Mock<IRepositorioBase<Usuarios, int>>();
                roles.Setup(x => x.RolClaims()).Returns(claims.Rol);
                bd.Setup(x => x.Agregar(usuario)).Returns(usuario);
                UsuariosServicio usuariosServicio = new UsuariosServicio(bd.Object, roles.Object);
                var result = usuariosServicio.AgregarEmpleado(usuario);
                Assert.Equal(usuario.Nombre, result.Nombre);
            }
            catch (Exception ex)
            {
                Assert.Equal("usuario no tiene acceso para crear un Usuario Propietario", ex.Message);
            }
        }
    }
}
