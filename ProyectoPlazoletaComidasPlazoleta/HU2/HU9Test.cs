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

namespace Test
{
    public class HU9Test
    {
        [Fact]
        public void ConsultarRestaurantes()
        {
            //int ElementosPorPagina = 4;
            //string consulta = "[{\"NIT_Id\":342342,\"Nombre\":\"Maria Jose\",\"Direccion\":\"Facatativa\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/Sopas\",\"DocumentoId\":333323232},{\"NIT_Id\":6523423,\"Nombre\":\"Sopas\",\"Direccion\":\"carrera31#56-69\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/Sopas\",\"DocumentoId\":333323232},{\"NIT_Id\":6654645,\"Nombre\":\"perros y mas\",\"Direccion\":\"carrera31#56-69\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/perros y mas\",\"DocumentoId\":333323232},{\"NIT_Id\":11111111,\"Nombre\":\"La Carniceria\",\"Direccion\":\"carrera31#56-69\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/La Carniceria\",\"DocumentoId\":888888888},{\"NIT_Id\":42354325,\"Nombre\":\"tieaisd\",\"Direccion\":\"Facatativa\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/tieaisd\",\"DocumentoId\":333323232},{\"NIT_Id\":43432423,\"Nombre\":\"Flor Desayunos\",\"Direccion\":\"carrera31#56-69\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/La Carniceria\",\"DocumentoId\":333323232},{\"NIT_Id\":44323412,\"Nombre\":\"pizazas planerts\",\"Direccion\":\"carrera31#56-69\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/pizazas planerts\",\"DocumentoId\":333323232},{\"NIT_Id\":52342342,\"Nombre\":\"trasdf\",\"Direccion\":\"Facatativa\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/asdasd\",\"DocumentoId\":333323232},{\"NIT_Id\":55555555,\"Nombre\":\"MaPu Pizza\",\"Direccion\":\"carrera31#56-69\",\"Telefono\":\"3123123\",\"URLLogo\":\"\\/mapupizza\",\"DocumentoId\":123456789},{\"NIT_Id\":123212321,\"Nombre\":\"la almorceria\",\"Direccion\":\"carrera31#56-69\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/La Carniceria\",\"DocumentoId\":333323232}]";
            //var restauranteslist = JsonConvert.DeserializeObject<List<Restaurantes>>(consulta) ;
            //Mock<IRepositorioRestaurante<Restaurantes, int>> restaurente = new Mock<IRepositorioRestaurante<Restaurantes, int>>();
            //Mock<IRepositorioPlatos<Platos, string, int>> platos = new Mock<IRepositorioPlatos<Platos, string, int>>();
            //Mock<IRepositotioPedidos<Pedidos, Guid>> repoPedidos = new Mock<IRepositotioPedidos<Pedidos, Guid>>();
            //Mock<IRoles> repoRoles = new Mock<IRoles>();
            //restaurente.Setup(x => x.ObtenerTodos()).Returns(restauranteslist);
            //ClienteServicio servicio = new ClienteServicio(restaurente.Object, platos.Object, repoPedidos.Object, repoRoles.Object);
            //var result = servicio.ListarRestaurantes(ElementosPorPagina);
            //Assert.NotNull(result);
            //int paginas = (int)restauranteslist.Count() / ElementosPorPagina;
            //if (restauranteslist.Count() % ElementosPorPagina != 0)
            //{
            //    paginas += 1;
            //}

            //Assert.Equal(paginas, result.Count);
        }
    }

}
