using Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Context
{
    public class Db_Context : DbContext
    {
        public Db_Context(DbContextOptions<Db_Context> opciones) : base(opciones)
        {

        }
        public DbSet<Usuarios> Usuarios { get; set; }

        public DbSet<Roles> Roles { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
