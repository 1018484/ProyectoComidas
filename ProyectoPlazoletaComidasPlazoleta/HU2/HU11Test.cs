using Aplicacion.Servicios;
using Dominio.DTO;
using Dominio.Modelos;
using Dominio.Repositorios;
using Dominio.User_Case;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class HU11Test
    {
        Mock<IRestaurantRespository<Restaurantes, int>> repoRestaurant = new Mock<IRestaurantRespository<Restaurantes, int>> ();
        Mock<IDishesRepository<Platos, string, int>> repoDishes = new Mock<IDishesRepository<Platos, string, int>> ();
        Mock<IOrdersRepository<Pedidos, string>> repoOrders = new Mock<IOrdersRepository<Pedidos, string>> ();
        Mock<IRoles> repoRoles = new Mock<IRoles> ();
        Mock<IDishesOrdersRepository<PedidosPlatos, Guid>> repoDishesOrders = new Mock<IDishesOrdersRepository<PedidosPlatos, Guid>> ();
        Mock<IClients> userCase = new Mock<IClients> ();
        Data data;

        public HU11Test()
        {
            data = new Data ();
        }

        [Fact]
        public void CrearOrden()
        {            
            var orderDeserilized = JsonConvert.DeserializeObject<SendOrder>(data.pedido);
            UsuarioClaims claims = data.UserClietnClaims();
            Pedidos pedido = new Pedidos ();
            pedido = null;
            Task<UsuarioClaims> task2 = Task<UsuarioClaims>.Factory.StartNew(() =>
            {
                return claims;
            });
            Client clientCase = new Client ();
            clientCase.ValidateRol(task2);
            userCase.Setup(x=> x.ValidateRol(task2));            
            clientCase.ValidOrders(pedido);
            ClientService service = new ClientService(repoRestaurant.Object, repoDishes.Object, repoOrders.Object, repoRoles.Object, repoDishesOrders.Object, userCase.Object );
            service.AddOrders(orderDeserilized);
        }


        [Fact]
        public void CrearOrdenValidRol()
        {
            try
            {                
                var orderDeserilized = JsonConvert.DeserializeObject<SendOrder>(data.pedido);
                UsuarioClaims claims = data.UserOwnerClaims2();
                Pedidos pedido = new Pedidos();
                pedido = null;
                Task<UsuarioClaims> task2 = Task<UsuarioClaims>.Factory.StartNew(() =>
                {
                    return claims;
                });
                Client clientCase = new Client();
                clientCase.ValidateRol(task2);
                userCase.Setup(x => x.ValidateRol(task2));
                clientCase.ValidOrders(pedido);
                ClientService service = new ClientService(repoRestaurant.Object, repoDishes.Object, repoOrders.Object, repoRoles.Object, repoDishesOrders.Object, userCase.Object);
                service.AddOrders(orderDeserilized);

            }
            catch (Exception ex)
            {
                Assert.Contains("User Not authorized", ex.Message);
            }
           
        }


        [Fact]
        public void ValidteOrdeerInProcces()
        {
            try
            {                
                var orderDeserilized = JsonConvert.DeserializeObject<SendOrder>(data.pedido);
                UsuarioClaims claims = data.UserClietnClaims();
                Pedidos pedido = data.order();                
                Task<UsuarioClaims> task2 = Task<UsuarioClaims>.Factory.StartNew(() =>
                {                   
                    return claims;
                });

                Client clientCase = new Client();
                clientCase.ValidateRol(task2);
                userCase.Setup(x => x.ValidateRol(task2));
                clientCase.ValidOrders(pedido);
                ClientService service = new ClientService(repoRestaurant.Object, repoDishes.Object, repoOrders.Object, repoRoles.Object, repoDishesOrders.Object, userCase.Object);
                service.AddOrders(orderDeserilized);

            }
            catch (Exception ex)
            {
                Assert.Contains("The User cannot create another order until their queued orders are finished", ex.Message);
            }

        }
    }
}
