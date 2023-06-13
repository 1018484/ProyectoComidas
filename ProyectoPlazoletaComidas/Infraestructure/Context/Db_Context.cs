using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infraestructure.Entity;

namespace infrastructure.Context
{
    public class Db_Context : DbContext
    {
        public Db_Context(DbContextOptions<Db_Context> opciones) : base(opciones)
        {

        }
        public DbSet<User> User { get; set; }

        public DbSet<Rols> Roles { get; set; }       
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                       
        }
    }
}
