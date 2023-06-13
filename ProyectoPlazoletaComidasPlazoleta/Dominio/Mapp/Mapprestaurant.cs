using AutoMapper;
using Dominio.Modelos;
using Dominio.Modelos.DTO;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Mapp
{
    public class Mapprestaurant:Profile
    {
       public Mapprestaurant() 
       {
            CreateMap<RestaurantesDTO, Restaurantes>();
            CreateMap<PlatosDTO, Platos>();
       }
    }
}
