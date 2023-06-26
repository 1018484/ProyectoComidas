using Aplicacion.Servicios;
using Dominio.Modelos;
using Dominio.DTO;
using Dominio.Repositorios;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Dominio.User_Case;

namespace Test
{
    public class HU9Test:Data
    
    {
        [Fact]
        public void ConsultarRestaurantes()
        {
            int ElementosPorPagina = 4;           
            var restauranteslist = JsonConvert.DeserializeObject<List<Restaurantes>>(Restaurantes);
            Mock<IRestaurantRespository<Restaurantes, int>> restaurente = new Mock<IRestaurantRespository<Restaurantes, int>>();
            Mock<IDishesRepository<Platos, string, int>> platos = new Mock<IDishesRepository<Platos, string, int>>();
            Mock<IOrdersRepository<Pedidos, string> > repoPedidos = new Mock<IOrdersRepository<Pedidos, string>>();
            Mock<IRoles> repoRoles = new Mock<IRoles>();
            Mock<IDishesOrdersRepository<PedidosPlatos, Guid>> repoDishesOrders = new Mock<IDishesOrdersRepository<PedidosPlatos, Guid>>();
            Mock< IClients> useClient = new Mock<IClients>();
            Client useCase = new Client();                    
            restaurente.Setup(x => x.GetAll()).Returns(restauranteslist);
            ClientService servicio = new ClientService(restaurente.Object, platos.Object, repoPedidos.Object, repoRoles.Object, repoDishesOrders.Object, useClient.Object);
            var result = useCase.ListRestaurants(ElementosPorPagina, restauranteslist.Select(x => new RestaurantesfiltradosDTO()
            {
                Nombre = x.Nombre,
                URLLogo = x.URLLogo
            }).ToList()); ;

            Assert.NotNull(result);
            int paginas = (int)restauranteslist.Count() / ElementosPorPagina;
            if (restauranteslist.Count() % ElementosPorPagina != 0)
            {
                paginas += 1;
            }

            Assert.Equal(paginas, result.Count);
        }
    }

}
