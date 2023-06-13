using Dominio.Interfaces;
using Dominio.Modelos;
using Dominio.Modelos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicacion.Interfaces
{
    public interface IRestarurantService
    {
        Task<Restaurantes> AddRestaurant(RestaurantesDTO entidaDTO);

        Task AddEmployeeRestaurant(int IdPropietario);        
        
    }
}
