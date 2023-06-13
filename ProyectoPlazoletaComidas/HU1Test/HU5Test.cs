//using Applicacion.Repositorio;
//using Dominio.Modelos;
//using infrastructure.Context;
//using infrastructure.Repositorios;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.TestHost;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Hosting;
//using Microsoft.VisualStudio.TestPlatform.TestHost;
//using Newtonsoft.Json;
//using System.Net.Http.Json;
//using System.Text;
//using Xunit;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.Extensions.DependencyInjection;
//using System.Net.Http.Headers;
//using Dominio.Modelos.DTO;
//using System.Net.Http;
//using Newtonsoft.Json.Linq;
//using Moq;
//using Dominio.Repositorios;
//using System.Runtime.ConstrainedExecution;
//using System.Security.Claims;

//namespace HU1Test
//{
//    public class HU5Test
//    {
//        class apisubscripcion : WebApplicationFactory<ProgramUser>
//        {
//            protected override IHost CreateHost(IHostBuilder builder)
//            {
//                return base.CreateHost(builder);
//            }
//        }

//        [Fact]
//        public async Task IniciarSesionCorrecto()
//        {
//            UserLogin usuarioDTO = new UserLogin()
//            {
//                Email = "mapu@gmail.com",
//                Password = "1604"
//            };

//            Usuarios usuario = new Usuarios()
//            {
//                DocumentoId = 10184841,
//                Nombre = "Mapu",
//                Apellido = " Osorio",
//                Celular = "12324432",
//                Correo = "mapu@gmail.com",
//                Clave = "$2a$11$7iSEMaVkyPIZpYaUHCbxQuz90oWWULtY6Ued4RhUXCeeYdXNq8AhG",
//                RolesRolId = 2,
//            };

//            Usuarios usuario2 = new Usuarios()
//            {
//                DocumentoId = 856765,
//                Nombre = "flor",
//                Apellido = " Osorio",
//                Celular = "3124123",
//                Correo = "flor@gmail.com",
//                Clave = "$2a$11$6WzbKzFZ3CxWEyGuzkw0CelCqasiMw3sSWmIL4th7IOGxpCAyxmuW",
//                RolesRolId = 2,
//            };

//            List<Usuarios> usuarioslist = new List<Usuarios>();
//            usuarioslist.Add(usuario);
//            usuarioslist.Add(usuario2);

//            Mock<IRolesRepository> roles = new Mock<IRolesRepository>();
//            Mock<IUserRepository<Usuarios, int>> bd = new Mock<IUserRepository<Usuarios, int>>();
            
//            bd.Setup(x => x.GetAll()).Returns(usuarioslist);
//            UserService usuariosServicio = new UserService(bd.Object, roles.Object);
//            var result = usuariosServicio.PasswordValidation(usuarioDTO);
//            Assert.Equal(usuarioDTO.Email, result.Correo);
//        }

//        [Fact]
//        public async Task IniciarSesionuuUsuarionoexiste()
//        {            
//            UserLogin usuarioDTO = new UserLogin()
//            {
//                Email = "rrr@gmail.com",
//                Password = "1604"
//            };

//            Usuarios usuario = new Usuarios()
//            {
//                DocumentoId = 10184841,
//                Nombre = "Mapu",
//                Apellido = " Osorio",
//                Celular = "12324432",
//                Correo = "mapu@gmail.com",
//                Clave = "$2a$11$7iSEMaVkyPIZpYaUHCbxQuz90oWWULtY6Ued4RhUXCeeYdXNq8AhG",
//                RolesRolId = 2,
//            };

//            Usuarios usuario2 = new Usuarios()
//            {
//                DocumentoId = 856765,
//                Nombre = "flor",
//                Apellido = " Osorio",
//                Celular = "3124123",
//                Correo = "flor@gmail.com",
//                Clave = "$2a$11$6WzbKzFZ3CxWEyGuzkw0CelCqasiMw3sSWmIL4th7IOGxpCAyxmuW",
//                RolesRolId = 2,
//            };

//            List<Usuarios> usuarioslist = new List<Usuarios>();
//            usuarioslist.Add(usuario);
//            usuarioslist.Add(usuario2);

//            Mock<IRolesRepository> roles = new Mock<IRolesRepository>();
//            Mock<IUserRepository<Usuarios, int>> bd = new Mock<IUserRepository<Usuarios, int>>();
            
//            bd.Setup(x => x.GetAll()).Returns(usuarioslist);
//            UserService usuariosServicio = new UserService(bd.Object, roles.Object);
//            var result = usuariosServicio.PasswordValidation(usuarioDTO);
//            Assert.Equal(null, result) ;            
//        }


//        [Fact]
//        public async Task InvalidPassword()
//        {
//            UserLogin usuarioDTO = new UserLogin()
//            {
//                Email = "mapu@gmail.com",
//                Password = "1602"
//            };

//            Usuarios usuario = new Usuarios()
//            {
//                DocumentoId = 10184841,
//                Nombre = "Mapu",
//                Apellido = " Osorio",
//                Celular = "12324432",
//                Correo = "mapu@gmail.com",
//                Clave = "$2a$11$7iSEMaVkyPIZpYaUHCbxQuz90oWWULtY6Ued4RhUXCeeYdXNq8AhG",
//                RolesRolId = 2,
//            };

//            Usuarios usuario2 = new Usuarios()
//            {
//                DocumentoId = 856765,
//                Nombre = "flor",
//                Apellido = " Osorio",
//                Celular = "3124123",
//                Correo = "flor@gmail.com",
//                Clave = "$2a$11$6WzbKzFZ3CxWEyGuzkw0CelCqasiMw3sSWmIL4th7IOGxpCAyxmuW",
//                RolesRolId = 2,
//            };

//            List<Usuarios> usuarioslist = new List<Usuarios>();
//            usuarioslist.Add(usuario);
//            usuarioslist.Add(usuario2);

//            Mock<IRolesRepository> roles = new Mock<IRolesRepository>();
//            Mock<IUserRepository<Usuarios, int>> bd = new Mock<IUserRepository<Usuarios, int>>();
            
//            bd.Setup(x => x.GetAll()).Returns(usuarioslist);
//            UserService usuariosServicio = new UserService(bd.Object, roles.Object);
//            var result = usuariosServicio.PasswordValidation(usuarioDTO);
//            Assert.Equal(null, result);
//        }

//        //[Fact]
//        public async Task ValidarCrearusuarioPropirtario()
//        {
//            UserLogin usuariodto = new UserLogin()
//            {
//                Email = "mapu@gmail.com",
//                Password = "123"
//            };

//            Usuarios usuario = new Usuarios()
//            {
//                DocumentoId = 23234,
//                Nombre = "Jorge",
//                Apellido = "Velazques",
//                Celular = "+573132408264",
//                Correo = "li@hotmail.com",
//                Clave = "1234",                
//            };

//            UserClaims claims = new UserClaims()
//            {
//                Rol = "1",
//                Id = "10184841",
//                Email = "mapu@gmail.com"
//            };
//            Mock<IRolesRepository> roles = new Mock<IRolesRepository>();
//            Mock<IUserRepository<Usuarios, int>> bd = new Mock<IUserRepository<Usuarios, int>>();
            
//            roles.Setup(x => x.RolClaims()).Returns(claims);
//            bd.Setup(x => x.Add(usuario)).Returns(usuario);
//            UserService usuariosServicio = new UserService(bd.Object, roles.Object);
//            var result =  usuariosServicio.AddOwner(usuario);
//            Assert.Equal(usuario.Nombre, result.Nombre);      
//        }

//        [Fact]
//        public async Task ValidarCrearusuarioPropirtarioUsuariosinpermiso()
//        {
//            try
//            {
//                UserLogin usuariodto = new UserLogin()
//                {
//                    Email = "jaime@gmail.com",
//                    Password = "123"
//                };

//                Usuarios usuario = new Usuarios()
//                {
//                    DocumentoId = 23234,
//                    Nombre = "Jorge",
//                    Apellido = "Velazques",
//                    Celular = "+573132408264",
//                    Correo = "li@hotmail.com",
//                    Clave = "1234",
//                };

//                UserClaims claims = new UserClaims()
//                {
//                    Rol = "2",
//                    Id = "10184841",
//                    Email = "mapu@gmail.com"
//                };
//                Mock<IRolesRepository> roles = new Mock<IRolesRepository>();
//                Mock<IUserRepository<Usuarios, int>> bd = new Mock<IUserRepository<Usuarios, int>>();
               
//                roles.Setup(x => x.RolClaims()).Returns(claims);
//                bd.Setup(x => x.Add(usuario)).Returns(usuario);
//                UserService usuariosServicio = new UserService(bd.Object, roles.Object);
//                var result = usuariosServicio.AddOwner(usuario);
//                Assert.Equal(usuario.Nombre, result.Nombre);

//            }
//            catch (Exception ex) 
//            {
//                Assert.Equal("usuario no tiene acceso para crear un Usuario Propietario", ex.Message);

//            }            
            
//        }

//        [Fact]
//        public async Task ValidarCrearusuarioEmpleado()
//        {
//            UserLogin usuariodto = new UserLogin()
//            {
//                Email = "mapu@gmail.com",
//                Password = "123"
//            };

//            Usuarios usuario = new Usuarios()
//            {
//                DocumentoId = 23234,
//                Nombre = "Jorge",
//                Apellido = "Velazques",
//                Celular = "+573132408264",
//                Correo = "li@hotmail.com",
//                Clave = "1234",
//            };

//            UserClaims claims = new UserClaims()
//            {
//                Rol = "2",
//                Id = "10184841",
//                Email = "mapu@gmail.com"
//            };
//            Mock<IRolesRepository> roles = new Mock<IRolesRepository>();
//            Mock<IUserRepository<Usuarios, int>> bd = new Mock<IUserRepository<Usuarios, int>>();
            
//            roles.Setup(x => x.RolClaims()).Returns(claims);
//            bd.Setup(x => x.Add(usuario)).Returns(usuario);
//            UserService usuariosServicio = new UserService(bd.Object, roles.Object);
//            var result = await usuariosServicio.AddEmployee(usuario);
//            Assert.Equal(usuario.Nombre, result.Nombre);
//        }

//        [Fact]
//        public async Task ValidarCrearusuarioEmpleadoSinPermiso()
//        {
//            try
//            {
//                UserLogin usuariodto = new UserLogin()
//                {
//                    Email = "jaime@gmail.com",
//                    Password = "123"
//                };

//                Usuarios usuario = new Usuarios()
//                {
//                    DocumentoId = 23234,
//                    Nombre = "Jorge",
//                    Apellido = "Velazques",
//                    Celular = "+573132408264",
//                    Correo = "li@hotmail.com",
//                    Clave = "1234",
//                };

//                UserClaims claims = new UserClaims()
//                {
//                    Rol = "2",
//                    Id = "10184841",
//                    Email = "mapu@gmail.com"
//                };
//                Mock<IRolesRepository> roles = new Mock<IRolesRepository>();
//                Mock<IUserRepository<Usuarios, int>> bd = new Mock<IUserRepository<Usuarios, int>>();
                
//                roles.Setup(x => x.RolClaims()).Returns(claims);
//                bd.Setup(x => x.Add(usuario)).Returns(usuario);
//                UserService usuariosServicio = new UserService(bd.Object, roles.Object);
//                var result = await usuariosServicio.AddEmployee(usuario);
//                Assert.Equal(usuario.Nombre, result.Nombre);
//            }
//            catch (Exception ex)
//            {
//                Assert.Equal("usuario no tiene acceso para crear un Usuario Propietario", ex.Message);
//            }
//        }
//    }
//}
