//using Applicacion.Repositorio;
//using Dominio.Modelos.DTO;
//using Dominio.Modelos;
//using Dominio.Repositorios;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace Test
//{
//    public class HU8Test
//    {
//        [Fact]        
//        public async Task PruebaInserciondeClientes()
//        {

//            Usuarios usuarios = new Usuarios()
//            {
//                DocumentoId = 121212121,
//                Nombre = "liana",
//                Apellido = "fonseca",
//                Celular = "+5712312",
//                Correo = "li@hotmail.com",
//                Clave = "1234",
//                RolesRolId = 2
//            };

//            UserClaims claims = new UserClaims()
//            {
//                Rol = "1",
//                Id = "10184841",
//                Email = "mapu@gmail.com"
//            };

//            Mock<IRolesRepository> Roles = new Mock<IRolesRepository>();
//            Mock<IUserRepository<Usuarios, int>> bd = new Mock<IUserRepository<Usuarios, int>>();
           
//            Roles.Setup(x => x.RolClaims()).Returns(claims);
//            bd.Setup(x => x.Add(usuarios)).Returns(usuarios);
//            UserService servicio = new UserService(bd.Object, Roles.Object);
//            var result = servicio.AddUser(usuarios);
//            Assert.NotNull(result);
//            Assert.Equal(usuarios.DocumentoId, result.DocumentoId);
//            Assert.Equal("4", result.RolesRolId.ToString());
//        }

//        [Fact]
//        public async Task PruebaCamposNulosInserciondeClientes()
//        {
//            try
//            {

//                Usuarios usuarios = new Usuarios()
//                {
//                    DocumentoId = 121212121,
//                    Nombre = "liana",
//                    Apellido = "fonseca",
//                    Celular = "+5712312",
//                    Correo = "li@hotmail.com",                   
                    
//                };

//                UserClaims claims = new UserClaims()
//                {
//                    Rol = "1",
//                    Id = "10184841",
//                    Email = "mapu@gmail.com"
//                };

//                Mock<IRolesRepository> Roles = new Mock<IRolesRepository>();
//                Mock<IUserRepository<Usuarios, int>> bd = new Mock<IUserRepository<Usuarios, int>>();
//                Roles.Setup(x => x.RolClaims()).Returns(claims);
//                bd.Setup(x => x.Add(usuarios)).Returns(usuarios);
               
//                UserService servicio = new UserService(bd.Object, Roles.Object);
//                var result = servicio.AddUser(usuarios);
//            }
//            catch(Exception e)
//            {
//                Assert.Equal("Campos nulos", e.Message);
//            }
            
//        }
//    }
//}
