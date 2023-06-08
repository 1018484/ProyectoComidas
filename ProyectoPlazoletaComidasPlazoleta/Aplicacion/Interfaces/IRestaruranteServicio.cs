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
    public interface IRestaruranteServicio
    {
        Task<Restaurantes> Agregar(RestaurantesDTO entidaDTO);

        Restaurantes ObtenerRestauranteNIt_ID(int IdPropietario);
        
        
    }
}
