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
using Dominio.Modelos.DTO;
using System.Net;
using System.Runtime.Serialization;
using Dominio.Mapp;
using AutoMapper;

namespace Applicacion.Repositorio
{
    public class UserService : IUserService
    {
        private readonly IUserRepository<UserModel, UserDTO, int> repoUsers;

        private readonly IRolesRepository repoRoles;  
        
        private readonly IMapper mapper;

        private Validation validation;       

        public UserService(IUserRepository<UserModel, UserDTO, int> users, IRolesRepository Roles)
        {
            this.repoUsers = users;
            this.repoRoles = Roles;
            //this.mapper = mapper;
            validation = new Validation();            
        } 

        public void AddOwner(UserDTO entityDTO)
        {                                                  
            if (!validation.EmailValidation(entityDTO.Email))
            {
                throw new Exception("Correo Invalido");
            }
            if (!validation.PhoneValidation(entityDTO.Phone))
            {
                throw new Exception("Numero telefonico Invalido");
            }

            entityDTO.RolsRolId = (int)EnumRoles.Propietario;
            entityDTO.Password = BCrypt.Net.BCrypt.HashPassword(entityDTO.Password);

            repoUsers.Add(mapper.Map<UserModel>(entityDTO));
            repoUsers.Confirm();                              
        }

        public void AddEmployee(UserDTO entityDTO)
        {   
            if (!validation.EmailValidation(entityDTO.Email))
            {
                throw new Exception("Correo Invalido");
            }
            if (!validation.PhoneValidation(entityDTO.Phone))
            {
                throw new Exception("Numero telefonico Invalido");
            }

            entityDTO.Password = BCrypt.Net.BCrypt.HashPassword(entityDTO.Password);
            entityDTO.RolsRolId = (int)EnumRoles.Empleado;
            repoUsers.Add(mapper.Map<UserModel>(entityDTO));
            repoUsers.Confirm();                 
            
        }


        public void  AddUser(UserDTO EntityDTO)
        {
            EntityDTO.RolsRolId = (int)EnumRoles.Cliente;                    
            if (!validation.EmailValidation(EntityDTO.Email))
            {
                throw new Exception("Correo Invalido");
            }
            if (!validation.PhoneValidation(EntityDTO.Phone))
            {
                throw new Exception("Numero telefonico Invalido");
            }

            EntityDTO.Password = BCrypt.Net.BCrypt.HashPassword(EntityDTO.Password);
            repoUsers.Add(mapper.Map<UserModel>(EntityDTO));
            repoUsers.Confirm();            
        }

        public UserDTO GetById(int id)
        {
            return repoUsers.GetAllById(id);
        }

        public List<UserDTO> GetAll()
        {
            return repoUsers.GetAll();
        }
        
        public UserDTO PasswordValidation(UserLogin userLoguin)
        {
            var users = repoUsers.GetAll();
            var user = users.Where(u => u.Email == userLoguin.Email).FirstOrDefault();
            if (user == null || !BCrypt.Net.BCrypt.Verify(userLoguin.Password, user.Password))
            {
                return null;
            }           

            return user;
        }
        
    }
}
