using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;
using Dominio.Modelos;
using Dominio.Modelos.DTO;

namespace Applicacion.Interfaces
{
    public interface IUsuarioServicio
    {
        Usuarios AgregarPropietario(Usuarios entidad);

        Task<Usuarios> AgregarEmpleado(Usuarios entidad);

        Usuarios AgregarUsuario(Usuarios entidad);

        List<Usuarios> ObtenerTodos();

        Usuarios obtener(int id);

        Usuarios ValidaUsusarioContraseña(UsuarioDTO usuarioDTO);

        int ObtenerRestauranteNIT(int id);

    }
}
