using AutoMapper;
using Dominio.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Mapp
{
    public class MappUserModeltoUserDTO:Profile
    {
        public MappUserModeltoUserDTO()
        {
            CreateMap<UsuarioDTO, Usuarios>();
        }
    }
}
