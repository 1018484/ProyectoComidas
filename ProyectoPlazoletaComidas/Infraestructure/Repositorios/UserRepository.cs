using AutoMapper;
using Dominio.Modelos;
using Dominio.Modelos.DTO;
using Dominio.Repositorios;
using Infraestructure.Entity;
using infrastructure.Context;

namespace infrastructure.Repositorios
{
    public class UserRepository : IUserRepository<User, UserDTO, int>
    {
        private Db_Context db_context;

        private readonly IMapper mapper;

        public UserRepository(Db_Context db_context) 
        { 
        
            this.db_context = db_context;
            //this.mapper = mapper;
        }

        public void Add(User entity)
        {
            db_context.User.Add(entity);
            
        }

        public void Confirm()
        {
            db_context.SaveChanges();
        }

        public void Edit(User entity)
        {
            var select = db_context.User.Where(u => u.DocumentId == entity.DocumentId).FirstOrDefault();
            if (select != null)
            {
                select.Name = entity.Name;
                select.LastName = entity.LastName;
                select.Email = entity.Email;
                select.Phone = entity.Phone;                
                select.Email = entity.Name;
                select.RolsRolId = entity.RolsRolId;
                db_context.User.Update(select);
            }
        }

        public void Delete(int id)
        {
            var select = db_context.User.Where(u => u.DocumentId == id).FirstOrDefault();
            if (select != null)
            {
                db_context.User.Remove(select);
            }
        }

        public UserDTO GetAllById(int id)
        {
            return mapper.Map<UserDTO>(db_context.User.Where(u => u.DocumentId == id).FirstOrDefault());
        }

        public List<UserDTO> GetAll()
        {
            return mapper.Map<List<UserDTO>>(db_context.User.ToList()) ;
        }
    }
}
