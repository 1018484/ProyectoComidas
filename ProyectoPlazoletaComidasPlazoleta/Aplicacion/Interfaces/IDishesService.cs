using Dominio.Interfaces;
using Dominio.Modelos;
using Dominio.Modelos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IDishesService
    {
        Task<Platos> AddDish(PlatosDTO entidadDTO);

        Task<Platos> EditDish(PlatosDTO entidadDTO);

    }
}
