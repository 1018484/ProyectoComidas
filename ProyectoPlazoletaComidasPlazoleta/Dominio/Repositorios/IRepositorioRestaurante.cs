using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IRepositorioRestaurante<Tentidad, TentidadID>:IAgregar<Tentidad>, IConfirmar, IConsultar<Tentidad, TentidadID>
    {
    }
}
