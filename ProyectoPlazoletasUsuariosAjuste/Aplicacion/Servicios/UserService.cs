using Aplicacion.Interfaces;
using Dominio.DTO;
using Dominio.Modelos;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Validaciones;
using AutoMapper;

namespace Aplicacion.Servicios
{
    public class UserService : IUserService    
    {
        private readonly IUserRespository repoUser;

        private readonly IMapper mapper;


        private Aplicacion.Validaciones.Validation  validation;

        public UserService(IUserRespository repoUser, IMapper mapper)
        {
            this.repoUser = repoUser;
            this.mapper = mapper;
            validation = new Aplicacion.Validaciones.Validation();

        }

        public void AddOwner(UsuarioDTO entityDTO)
        {
            var entity = mapper.Map<Usuarios>(entityDTO);
            entity.RolesRolId = (int)EnumRoles.Propietario;
            validation.PhoneValidation(entity.Celular);
            var result = repoUser.Add(entity);
            repoUser.Confirm();            
        }

        public void AddEmployee(UsuarioDTO entityDTO)
        {
            var entity = mapper.Map<Usuarios>(entityDTO);
            entity.RolesRolId = (int)EnumRoles.Empleado;
            validation.PhoneValidation(entity.Celular);
            var result = repoUser.Add(entity);          
            
        }


        public void AddUser(UsuarioDTO entityDTO)
        {
            var entity = mapper.Map<Usuarios>(entityDTO);
            entity.RolesRolId = (int)EnumRoles.Cliente;
            validation.PhoneValidation(entity.Celular);
            var result = repoUser.Add(entity);
            repoUser.Confirm();            
        }

        public Usuarios GetById(int id)
        {
            return repoUser.GetByID(id);
        }


        public Usuarios Password(UserLogin userLoguin)
        {
            return repoUser.ValidPassword(userLoguin);

        }
           
    }
}
