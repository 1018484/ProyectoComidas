using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Applicacion.Interfaces;
using Dominio.Modelos;
using Dominio.Repositorios;
using System.Text.RegularExpressions;
using Aplicacion.Validaciones;
using AutoMapper;
using Dominio.DTO;

namespace Applicacion.Repositorio
{
    public class RestaurantService : IRestarurantService
    {
        private readonly IRestaurantRespository<Restaurantes, int> repoRestaurant;
        private readonly IUsersRemotoRepository<Usuarios, int> repoUerRemoto;
        private readonly IRoles repoRoles;
        private readonly IEmployeeRestaurantRepository<EmpleadosRestaurantes, int> repoEmployeeRestaurant;
        private readonly IMapper mapper;
        private Validation Validation;
        private Task<UsuarioClaims> getClaims;

        public  RestaurantService(IRestaurantRespository<Restaurantes, int> repoRestaurante, IUsersRemotoRepository<Usuarios, int> repoUsuario, IRoles repoRoles, IEmployeeRestaurantRepository<EmpleadosRestaurantes, int> empleadosRestaurantes, IMapper mapper)
        {
            this.repoRestaurant = repoRestaurante;
            this.repoUerRemoto = repoUsuario;
            Validation = new Validation();
            this.repoRoles = repoRoles;
            this.mapper = mapper;
            this.repoEmployeeRestaurant = empleadosRestaurantes;            
            this.getClaims =  this.repoRoles.getToken();           
        }
        

        public async Task<Restaurantes> AddRestaurant(RestaurantesDTO entityDTO)
        {                         
            if (int.Parse(getClaims.Result.Rol) != (int)EnumRoles.Administrador)
            {
                throw new Exception("User Not authorized");
            }

            Restaurantes restaurant = mapper.Map<Restaurantes>(entityDTO);
            Validation.PhoneValidation(restaurant.Telefono);
            Validation.NumValidation(restaurant.Nombre);      

            var usuario = await repoUerRemoto.GetUserID(restaurant.DocumentoId);
            if (usuario == null)
            {
                throw new Exception("You are trying to add a restaurant to an unregistered User");
            }

            if (usuario.RolesRolId != (int)EnumRoles.Propietario)
            {
                throw new Exception("You are trying to add a restaurant to a User who does not have permissions to have one");
            }

            var result = repoRestaurant.Add(restaurant);
            this.repoRestaurant.Confirm();
            return result;
        }

        public async Task AddEmployeeRestaurant(int IdPropietario)
        {          
            if (int.Parse(getClaims.Result.Rol) != (int)EnumRoles.Propietario)
            {
                throw new Exception("User Not authorized");
            }

            EmpleadosRestaurantes employeerestaurant = new EmpleadosRestaurantes()
            {
                EmpleadoId = IdPropietario,
                RestauranteNIT = repoRestaurant.ObtenerById(int.Parse(getClaims.Result.Id)).NIT_Id 
            };

            repoEmployeeRestaurant.Add(employeerestaurant);
            repoEmployeeRestaurant.Confirm();
        }        
    }
}
