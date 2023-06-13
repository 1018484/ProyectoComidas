using Dominio.DTO;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IClientService
    {
        List<PaginacionRestaurantesDTO> ListRestaurants(int paginacion);

        List<PaginacionPlatosDTO> ListDishes(int paginacion);

        Task AddOrders(SendOrder pedido);
        
    }
}
