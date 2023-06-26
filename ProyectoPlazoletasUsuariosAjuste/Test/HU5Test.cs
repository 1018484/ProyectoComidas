using Dominio.Modelos;
using Infraestructura.Context;
using Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;
using Moq;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class HU5Test
    {
        private Data data;
        public HU5Test() 
        {
            data = new Data();
        }
        
        public void ValidPassword()
        {
            var userSet = new Mock<DbSet<Usuarios>>();
            var mockContext = new Mock<Db_Context>();
            var userLogin = data.login();
            mockContext.Setup(m => m.Usuarios).Returns(userSet.Object);
            List <Usuarios> user = data.UserList();            
            UsuariosRepository repository = new UsuariosRepository(mockContext.Object);
            var result = repository.ValidPassword(userLogin);
            Assert.NotNull(result);

        }
    }
}
