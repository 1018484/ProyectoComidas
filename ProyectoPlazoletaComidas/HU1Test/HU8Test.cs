using Applicacion.Repositorio;
using Dominio.Modelos.DTO;
using Dominio.Modelos;
using Dominio.Repositorios;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class HU8Test
    {
        [Fact]        
        public async Task PruebaInserciondeClientes()
        {

            Usuarios usuarios = new Usuarios()
            {
                DocumentoId = 121212121,
                Nombre = "liana",
                Apellido = "fonseca",
                Celular = "+5712312",
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
            bd.Setup(x => x.Agregar(usuarios)).Returns(usuarios);
            UsuariosServicio servicio = new UsuariosServicio(bd.Object, Roles.Object, repoRestauranteEmpleado.Object);
            var result = servicio.AgregarUsuario(usuarios);
            Assert.NotNull(result);
            Assert.Equal(usuarios.DocumentoId, result.DocumentoId);
            Assert.Equal("4", result.RolesRolId.ToString());
        }

        [Fact]
        public async Task PruebaCamposNulosInserciondeClientes()
        {
            try
            {

                Usuarios usuarios = new Usuarios()
                {
                    DocumentoId = 121212121,
                    Nombre = "liana",
                    Apellido = "fonseca",
                    Celular = "+5712312",
                    Correo = "li@hotmail.com",                   
                    
                };

                UsuarioClaims claims = new UsuarioClaims()
                {
                    Rol = "1",
                    Id = "10184841",
                    Correo = "mapu@gmail.com"
                };

                Mock<IRoles> Roles = new Mock<IRoles>();
                Mock<IRepositorioBase<Usuarios, int>> bd = new Mock<IRepositorioBase<Usuarios, int>>();
                Roles.Setup(x => x.RolClaims()).Returns(claims);
                bd.Setup(x => x.Agregar(usuarios)).Returns(usuarios);
                Mock<IRepositorioRestauranteEmpleados<RestauranteEmpleados, int>> repoRestauranteEmpleado = new Mock<IRepositorioRestauranteEmpleados<RestauranteEmpleados, int>>();
                UsuariosServicio servicio = new UsuariosServicio(bd.Object, Roles.Object, repoRestauranteEmpleado.Object);
                var result = servicio.AgregarUsuario(usuarios);
            }
            catch(Exception e)
            {
                Assert.Equal("Campos nulos", e.Message);
            }
            
        }
    }
}
