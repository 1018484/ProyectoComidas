using Dominio.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IUserRespository
    {
        Usuarios Add(Usuarios entidad);

        void Confirm();

        Usuarios GetByID(int id);

        Usuarios ValidPassword(UserLogin login);

    }
}
