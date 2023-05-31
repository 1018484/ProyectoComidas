using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IRepositorioUsuariosRemoto<Tentidad, TentidadID>
    {
        Task<Tentidad> UsuarioID(TentidadID id);        
    }
}
