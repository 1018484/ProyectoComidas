using Dominio.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IUserService
    {
        void AddOwner(UsuarioDTO entityDTO); 

        void AddEmployee(UsuarioDTO entityDTO);

        void AddUser(UsuarioDTO entityDTO);      

        Usuarios GetById(int id);

        Usuarios Password(UserLogin userLoguin);
        
    }
}
