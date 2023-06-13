using AutoMapper;
using Dominio.Modelos;
using Dominio.Modelos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Mapp
{
    public class MappUserDTO_UserModel:Profile
    {
        public MappUserDTO_UserModel() 
        {
            CreateMap<UserDTO,  UserModel>();
        }
    }
}
