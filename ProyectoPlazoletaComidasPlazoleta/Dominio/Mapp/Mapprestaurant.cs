using AutoMapper;
using Dominio.DTO;
using Dominio.Modelos;
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
            CreateMap<Pedidos, PedidosDTO>();
            CreateMap<Platos, PlatosDTO>();   
            //CreateMap<List<PedidosPlatos> , List<PedidosPlatosDTO>>();
            

       }
    }
}
