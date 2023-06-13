using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;
using Dominio.Modelos;
using Dominio.Modelos.DTO;

namespace Applicacion.Interfaces
{
    public interface IUserService
    {
        void AddOwner(UserDTO entidad);

        void AddEmployee(UserDTO entidad);

        void AddUser(UserDTO entidad);

        List<UserDTO> GetAll();

        UserDTO GetById(int id);

        UserDTO PasswordValidation(UserLogin usuarioDTO);        

    }
}
