using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dominio.Modelos;
using System.Drawing;

namespace infrastructure.Context
{
    public class Db_Context : DbContext
    {
        public Db_Context(DbContextOptions<Db_Context> opciones) : base(opciones)
        {

        }       

        public DbSet<Restaurantes> Restaurantes { get; set; }        

        public DbSet<Platos> Platos { get; set; }

        public DbSet<Pedidos> Pedidos { get; set; }

        public DbSet<PedidosPlatos> PedidosPlatos { get; set; }  


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PedidosPlatos>().HasKey(x => new
            {
                x.Pedido_Id,
                x.Id   
            });

            modelBuilder.Entity<PedidosPlatos>()
            .HasOne<Pedidos>(sc => sc.Pedidos)
            .WithMany(s => s.PedidosPlatos)
            .HasForeignKey(sc => sc.Pedido_Id)
            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<PedidosPlatos>()
            .HasOne<Platos>(sc => sc.Platos)
            .WithMany(s => s.PedidosPlatos)
            .HasForeignKey(sc => sc.Id)
            .OnDelete(DeleteBehavior.ClientSetNull);

            base.OnModelCreating(modelBuilder);

        }
        
    }
}
