using Dominio.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Data
    {
        public string Restaurantes = "[{\"NIT_Id\":342342,\"Nombre\":\"Maria Jose\",\"Direccion\":\"Facatativa\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/Sopas\",\"DocumentoId\":333323232},{\"NIT_Id\":6523423,\"Nombre\":\"Sopas\",\"Direccion\":\"carrera31#56-69\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/Sopas\",\"DocumentoId\":333323232},{\"NIT_Id\":6654645,\"Nombre\":\"perros y mas\",\"Direccion\":\"carrera31#56-69\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/perros y mas\",\"DocumentoId\":333323232},{\"NIT_Id\":11111111,\"Nombre\":\"La Carniceria\",\"Direccion\":\"carrera31#56-69\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/La Carniceria\",\"DocumentoId\":888888888},{\"NIT_Id\":42354325,\"Nombre\":\"tieaisd\",\"Direccion\":\"Facatativa\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/tieaisd\",\"DocumentoId\":333323232},{\"NIT_Id\":43432423,\"Nombre\":\"Flor Desayunos\",\"Direccion\":\"carrera31#56-69\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/La Carniceria\",\"DocumentoId\":333323232},{\"NIT_Id\":44323412,\"Nombre\":\"pizazas planerts\",\"Direccion\":\"carrera31#56-69\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/pizazas planerts\",\"DocumentoId\":333323232},{\"NIT_Id\":52342342,\"Nombre\":\"trasdf\",\"Direccion\":\"Facatativa\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/asdasd\",\"DocumentoId\":333323232},{\"NIT_Id\":55555555,\"Nombre\":\"MaPu Pizza\",\"Direccion\":\"carrera31#56-69\",\"Telefono\":\"3123123\",\"URLLogo\":\"\\/mapupizza\",\"DocumentoId\":123456789},{\"NIT_Id\":123212321,\"Nombre\":\"la almorceria\",\"Direccion\":\"carrera31#56-69\",\"Telefono\":\"23423\",\"URLLogo\":\"\\/La Carniceria\",\"DocumentoId\":333323232}]";
        
        public string pedido = "{\r\n  \"restauranteNIT\": 1121123123,\r\n  \"platos\": [\r\n    {\r\n      \"idPlato\": 24,\r\n      \"cantidad\": 1\r\n    },  \r\n    {\r\n      \"idPlato\": 26,\r\n      \"cantidad\": 1\r\n    },\r\n    {\r\n      \"idPlato\": 25,\r\n      \"cantidad\": 3\r\n    }     \r\n  ]\r\n}";
        
        public RestaurantesDTO RestaurantDTO()
        {
            RestaurantesDTO restDTO = new RestaurantesDTO()
            {
                NIT_Id = 32235,
                Nombre = "la hambugueseria123",
                Direccion = "carrera33b#28-50",
                Telefono = "314993783",
                URLLogo = "mcpollo.img",
                DocumentoId = 123456789
            };

            return restDTO;
        }

        public Restaurantes Restaurant()
        {
            Restaurantes rest = new Restaurantes()
            {
                NIT_Id = 32235,
                Nombre = "la hambugueseria123",
                Direccion = "carrera33b#28-50",
                Telefono = "314993783",
                URLLogo = "mcpollo.img",
                DocumentoId = 123456789
            };

            return rest;
        }

        public UsuarioClaims UserAdminClaims()
        {

            UsuarioClaims claims = new UsuarioClaims()
            {
                Rol = "1",
                Id = "1018484911",
                Correo = "jaime@gmail.com"
            };

            return claims;
        }

        public UsuarioClaims UserClietnClaims()
        {

            UsuarioClaims claims = new UsuarioClaims()
            {
                Rol = "4",
                Id = "191910921",
                Correo = "Nelson@gmail.com"
            };

            return claims;
        }

        public UsuarioClaims UserOwnerClaims()
        {
            UsuarioClaims claims = new UsuarioClaims()
            {
                Rol = "2",
                Id = "123456789",
                Correo = "mapu@gmail.com"
            };
            return claims;
        }

        public UsuarioClaims UserOwnerClaims2()
        {
            UsuarioClaims claims = new UsuarioClaims()
            {
                Rol = "2",
                Id = "333323232",
                Correo = "flores@gmail.com"
            };
            return claims;
        }

        public Usuarios Users()
        {
            Usuarios users = new Usuarios()
            {
                DocumentoId = 123456789,
                Nombre = "mapu",
                Apellido = "osorio",
                Celular = "3122567829",
                Correo = "mapu@gmail.com",
                Clave = "$2a$11$SsVSkHez.OjADrcdsuH1luI4hT5xslnsOGBzSn7fkGcy0DTgelDP2",
                RolesRolId = 2
            };

            return users;
        }


        public RestaurantesDTO RestaurantWrongPhone()
        {
            RestaurantesDTO restDTO = new RestaurantesDTO()
            {
                NIT_Id = 32235,
                Nombre = "la hambugueseria123",
                Direccion = "carrera33b#28-50",
                Telefono = "3149eqwr93783",
                URLLogo = "mcpollo.img",
                DocumentoId = 123456789
            };

            return restDTO;
        }

        public PlatosDTO DishesDTO()
        {
            PlatosDTO dishDTO = new PlatosDTO()
            {
                NombrePlato = "Pollo sudado",
                Precio = 5000,
                Desacripcion = "Pollo sudado",
                URLImagen = "Pollosudado",
                Activo = true,
                Categoria = "Pollo",
                RestaurantesNIT_Id = 32235
            };

            return dishDTO;
        } 
        public Platos Dish() 
        {
            Platos Dish = new Platos()
            {
                NombrePlato = "Pollo sudado",
                Precio = 5000,
                Desacripcion = "Pollo sudado",
                URLImagen = "Pollosudado",
                Activo = true,
                Categoria = "Pollo",
                RestaurantesNIT_Id = 32235
            };

            return Dish;            
        }     

        public Pedidos order()
        {
            Pedidos pedido = new Pedidos()
            {
                Pedido_Id = Guid.Parse("4AB3958AE8294F4A9BD7971076F6273C"),
                Cliente_Id = 191910921.ToString(),
                Fecha = DateTime.Now,
                Chef_Id = 0,
                RestaurantesNIT_Id = 55555555,
                Estado = 1,
            };

            return pedido;
        }

    }
}
