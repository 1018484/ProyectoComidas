using AutoMapper;
using Dominio.Modelos.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Entity;

namespace Infraestructure.Mapp
{
    public class MappUser_UserDTO:Profile
    {
        public MappUser_UserDTO()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
