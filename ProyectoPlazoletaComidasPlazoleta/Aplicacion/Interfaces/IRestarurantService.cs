using Dominio.DTO;
using Dominio.Interfaces;
using Dominio.Modelos;
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
