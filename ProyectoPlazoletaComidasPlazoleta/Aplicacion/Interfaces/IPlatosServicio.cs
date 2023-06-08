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
    public interface IPlatosServicio<Tentidad, IDusuario>
    {
        Task<Platos> Agregar(PlatosDTO entidadDTO, int  IDusuario);

        Task<Platos> EditarAsync(PlatosDTO entidadDTO, int IDusuario);


    }
}
