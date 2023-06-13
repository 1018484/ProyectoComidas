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




    }
}
