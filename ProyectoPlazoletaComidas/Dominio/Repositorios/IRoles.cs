using Dominio.Modelos;
using Dominio.Modelos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio.Repositorios
{
    public interface IRoles
    {
        UsuarioClaims RolClaims();
    }
}
