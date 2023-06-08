using Aplicacion.Interfaces;
using Dominio.Modelos;
using Dominio.Modelos.DTO;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class EmpleadoServicio : IEmpleadosServicio
    {
        private readonly IRepositotioPedidos<Pedidos, string> repoPedidos;

        private readonly IRepositorioUsuariosRemoto<Usuarios, int> repoUsuuariosRemoto;

        private readonly IRoles repoRoles;

        public EmpleadoServicio(IRepositotioPedidos<Pedidos, string> repoPedidos, IRoles repoRoles, IRepositorioUsuariosRemoto<Usuarios, int> repoUsuuariosRemoto)
        {
            this.repoPedidos = repoPedidos;
            this.repoRoles = repoRoles;
            this.repoUsuuariosRemoto = repoUsuuariosRemoto;
        }
        public async Task<List<Pedidos>>ListarPedidos(PedidsoFiltroDTO filtro)
        {
            int paginas = 0;
            var getClaims = await repoRoles.getToken();
            if (getClaims == null)
            {
                throw new Exception("La sesion caduco o el usuario no iniciado sesion");
            }

            if (Enum.Parse<EnumRoles>(getClaims.Rol) != EnumRoles.Empleado)
            {
                throw new Exception("El Usuario no tiene permiso para solicitar pedidos");
            }

            int restauranteID = await repoUsuuariosRemoto.ObtenerEmpleado(int.Parse(getClaims.Id));            
            List<Pedidos> pedidos = new List<Pedidos>();
            pedidos = repoPedidos.GetPedidos(restauranteID);
            if (filtro.Estado != 0)
            {
                pedidos = pedidos.Where(x=> x.Estado == filtro.Estado).ToList();
            }

            paginas = (int)pedidos.Count() / filtro.ElementosPorPagina;
            if (pedidos.Count() % filtro.ElementosPorPagina != 0)
            {
                paginas += 1;
            }

            for (int i = 1; i<= pedidos.Count(); i++)
            {

            }






            return pedidos;
            
            
            
        }
    }
}
