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
        public UsuarioDTO UserAdmin()
        {
            UsuarioDTO result = new UsuarioDTO()
            {
                DocumentoId = 1018484911,
                Nombre = "Jaime",
                Apellido = "Osorio",
                Celular = "+573132408264",
                Correo = "jaime@gmail.com",
                Clave = "1234",
                RolesRolId = 2
            };

            return result;
        }

        public UsuarioDTO WrongEmail()
        {
            UsuarioDTO result = new UsuarioDTO()
            {
                DocumentoId = 1018484911,
                Nombre = "Jaime",
                Apellido = "Osorio",
                Celular = "+573132408264",
                Correo = "jaime@gmail.c9m",
                Clave = "1234",
                RolesRolId = 2
            };

            return result;
        }

        public UsuarioDTO WronPhone()
        {
            UsuarioDTO result = new UsuarioDTO()
            {
                DocumentoId = 1018484911,
                Nombre = "Jaime",
                Apellido = "Osorio",
                Celular = "+57313240as8264",
                Correo = "jaime@gmail.c9m",
                Clave = "1234",
                RolesRolId = 2
            };

            return result;
        }

        public List<Usuarios> UserList() 
        {
            List<Usuarios> result = new List<Usuarios>();

            Usuarios user = new Usuarios()
            {
                DocumentoId = 10184841,
                Nombre = "Mapu",
                Apellido = " Osorio",
                Celular = "12324432",
                Correo = "mapu@gmail.com",
                Clave = "$2a$11$SsVSkHez.OjADrcdsuH1luI4hT5xslnsOGBzSn7fkGcy0DTgelDP2",
                RolesRolId = 2,
            };

            result.Add(user);

            Usuarios user2= new Usuarios()
            {
                DocumentoId = 856765,
                Nombre = "flor",
                Apellido = " Osorio",
                Celular = "3124123",
                Correo = "flor@gmail.com",
                Clave = "$2a$11$6WzbKzFZ3CxWEyGuzkw0CelCqasiMw3sSWmIL4th7IOGxpCAyxmuW",
                RolesRolId = 2,
            };

            result.Add(user2);

            return result; 

        }


        public UserLogin login()
        {
            UserLogin userLogin = new UserLogin()
            {
                Email = "mapu@gmail.com",
                Password = "123"
            };

            return userLogin;
        }

    }
}
