using Aplicacion.Servicios;
using Dominio.Modelos;
using Dominio.Repositorios;
using Dominio.User_Case;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class HU10Test
    {
        [Fact]
        public void ConsultarPlatos()
        {
            int ElementosPorPagina = 4;
            string consulta = "[{\"Id\":10,\"NombrePlato\":\"hamburguesa pescado\",\"Precio\":3000,\"Desacripcion\":\"hamburguesa pescado\",\"URLImagen\":\"hamburguesa pescado\",\"Activo\":true,\"Categoria\":\"hamburguesas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":11,\"NombrePlato\":\"hamburguesa de pollo\",\"Precio\":3000,\"Desacripcion\":\"hamburguesa pollo\",\"URLImagen\":\"hamburguesa pollo\",\"Activo\":true,\"Categoria\":\"hamburguesas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":12,\"NombrePlato\":\"AreBurguer\",\"Precio\":3000,\"Desacripcion\":\"AreBurguer\",\"URLImagen\":\"AreBurguer\",\"Activo\":true,\"Categoria\":\"hamburguesas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":13,\"NombrePlato\":\"CangrerBurguer\",\"Precio\":3000,\"Desacripcion\":\"CangrerBurguer\",\"URLImagen\":\"CangrerBurguer\",\"Activo\":true,\"Categoria\":\"hamburguesas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":14,\"NombrePlato\":\"Monster Burguer\",\"Precio\":3000,\"Desacripcion\":\"Monster Burguer\",\"URLImagen\":\"Monster Burguer\",\"Activo\":true,\"Categoria\":\"hamburguesas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":15,\"NombrePlato\":\"Brusca Burguer\",\"Precio\":3000,\"Desacripcion\":\"Brusca Burguer\",\"URLImagen\":\"Brusca Burguer\",\"Activo\":true,\"Categoria\":\"hamburguesas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":16,\"NombrePlato\":\"mapu Burguer\",\"Precio\":3000,\"Desacripcion\":\"mapu Burguer\",\"URLImagen\":\"mapu Burguer\",\"Activo\":true,\"Categoria\":\"hamburguesas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":17,\"NombrePlato\":\"pizza pollo\",\"Precio\":3000,\"Desacripcion\":\"pizza pollo\",\"URLImagen\":\"pizza pollo\",\"Activo\":true,\"Categoria\":\"pizzas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":18,\"NombrePlato\":\"pizza carnes\",\"Precio\":3000,\"Desacripcion\":\"pizza carnes\",\"URLImagen\":\"pizza carnes\",\"Activo\":true,\"Categoria\":\"pizzas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":19,\"NombrePlato\":\"pizza hawaiiana\",\"Precio\":3000,\"Desacripcion\":\"pizza hawaiiana\",\"URLImagen\":\"pizza hawaiiana\",\"Activo\":true,\"Categoria\":\"pizzas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":20,\"NombrePlato\":\"pizza napolitana\",\"Precio\":3000,\"Desacripcion\":\"pizza napolitana\",\"URLImagen\":\"pizza napolitana\",\"Activo\":true,\"Categoria\":\"pizzas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":21,\"NombrePlato\":\"pizza verona\",\"Precio\":3000,\"Desacripcion\":\"pizza verona\",\"URLImagen\":\"pizza verona\",\"Activo\":true,\"Categoria\":\"pizzas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":22,\"NombrePlato\":\"pizza Oibana\",\"Precio\":3000,\"Desacripcion\":\"pizza Oibana\",\"URLImagen\":\"pizza Oibana\",\"Activo\":true,\"Categoria\":\"pizzas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":23,\"NombrePlato\":\"Pasta napolitana\",\"Precio\":3000,\"Desacripcion\":\"Pasta napolitana\",\"URLImagen\":\"Pasta napolitana\",\"Activo\":true,\"Categoria\":\"Pastas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":24,\"NombrePlato\":\"Pasta al pesto\",\"Precio\":3000,\"Desacripcion\":\"Pasta al pesto\",\"URLImagen\":\"Pasta al pesto\",\"Activo\":true,\"Categoria\":\"Pastas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":25,\"NombrePlato\":\"Pasta boloñesa\",\"Precio\":3000,\"Desacripcion\":\"Pasta boloñesa\",\"URLImagen\":\"Pasta boloñesa\",\"Activo\":true,\"Categoria\":\"Pastas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":26,\"NombrePlato\":\"Pasta carbonara\",\"Precio\":3000,\"Desacripcion\":\"Pasta carbonara\",\"URLImagen\":\"Pasta carbonara\",\"Activo\":true,\"Categoria\":\"Pastas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":27,\"NombrePlato\":\"Pasta pollo Cahmpiñonnes\",\"Precio\":3000,\"Desacripcion\":\"Pasta pollo Cahmpiñonnes\",\"URLImagen\":\"Pasta pollo Cahmpiñonnes\",\"Activo\":true,\"Categoria\":\"Pastas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":28,\"NombrePlato\":\"Pasta pollo\",\"Precio\":3000,\"Desacripcion\":\"Pasta pollo\",\"URLImagen\":\"Pasta pollo\",\"Activo\":true,\"Categoria\":\"Pastas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":29,\"NombrePlato\":\"Pasta carne\",\"Precio\":3000,\"Desacripcion\":\"Pasta carne\",\"URLImagen\":\"Pasta carne\",\"Activo\":true,\"Categoria\":\"Pastas\",\"RestaurantesNIT_Id\":55555555},{\"Id\":30,\"NombrePlato\":\"Perro Mexicano\",\"Precio\":5000,\"Desacripcion\":\"Perro Mexicano\",\"URLImagen\":\"Perro Mexicano\",\"Activo\":true,\"Categoria\":\"Perros\",\"RestaurantesNIT_Id\":55555555},{\"Id\":31,\"NombrePlato\":\"Perro tocineta\",\"Precio\":10000,\"Desacripcion\":\"Perro tocineta\",\"URLImagen\":\"Perro tocinetao\",\"Activo\":true,\"Categoria\":\"Perros\",\"RestaurantesNIT_Id\":55555555},{\"Id\":32,\"NombrePlato\":\"Perro criollo\",\"Precio\":8000,\"Desacripcion\":\"Perro criollo\",\"URLImagen\":\"Perro criollo\",\"Activo\":true,\"Categoria\":\"Perros\",\"RestaurantesNIT_Id\":55555555},{\"Id\":33,\"NombrePlato\":\"Perro mantequilla\",\"Precio\":8000,\"Desacripcion\":\"Perro mantequilla\",\"URLImagen\":\"Perro mantequilla\",\"Activo\":true,\"Categoria\":\"Perros\",\"RestaurantesNIT_Id\":6523423},{\"Id\":34,\"NombrePlato\":\"Perro Leon\",\"Precio\":8000,\"Desacripcion\":\"Perro  Leon\",\"URLImagen\":\"Perro  Leon\",\"Activo\":true,\"Categoria\":\"Perros\",\"RestaurantesNIT_Id\":6523423}]";
            var PlatosList = JsonConvert.DeserializeObject<List<Platos>>(consulta);
            Mock<IRestaurantRespository<Restaurantes, int>> restaurente = new Mock<IRestaurantRespository<Restaurantes, int>>();
            Mock<IDishesRepository<Platos, string, int>> platos = new Mock<IDishesRepository<Platos, string, int>>();
            Mock<IOrdersRepository<Pedidos, string>> repoPedidos = new Mock<IOrdersRepository<Pedidos, string>>();
            Mock<IRoles> repoRoles = new Mock<IRoles>();
            Mock<IDishesOrdersRepository<PedidosPlatos, Guid>> repoDishesOrders = new Mock<IDishesOrdersRepository<PedidosPlatos, Guid>>();
            Mock<IClients> useClient = new Mock<IClients>();
            Client useCase = new Client();            
            ClientService servicio = new ClientService(restaurente.Object, platos.Object, repoPedidos.Object, repoRoles.Object, repoDishesOrders.Object, useClient.Object);
            useClient.Setup(x => x.ListDishes(ElementosPorPagina, PlatosList.GroupBy(x => x.RestaurantesNIT_Id))).Returns(useCase.ListDishes(ElementosPorPagina, PlatosList.GroupBy(x => x.RestaurantesNIT_Id)));
            platos.Setup(x => x.GetAll()).Returns(PlatosList);           
            var resultado = useCase.ListDishes(ElementosPorPagina, PlatosList.GroupBy(x => x.RestaurantesNIT_Id));
            Assert.NotEmpty(resultado);

        }
    }
}
