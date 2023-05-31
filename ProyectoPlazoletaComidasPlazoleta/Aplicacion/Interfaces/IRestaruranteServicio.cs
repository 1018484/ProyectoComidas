using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicacion.Interfaces
{
    public interface IRestaruranteServicio<TentidadDTO>
    {
        Task Agregar(TentidadDTO entidaDTO);
    }
}
