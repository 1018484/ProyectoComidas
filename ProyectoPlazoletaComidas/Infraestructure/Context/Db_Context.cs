using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dominio.Modelos;

namespace infrastructure.Context
{
    public class Db_Context : DbContext
    {
        public Db_Context(DbContextOptions<Db_Context> opciones) : base(opciones)
        {

        }
        public DbSet<Usuarios> Usuarios { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<RestauranteEmpleados> RestauranteEmpleados { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                       
        }
    }
}
