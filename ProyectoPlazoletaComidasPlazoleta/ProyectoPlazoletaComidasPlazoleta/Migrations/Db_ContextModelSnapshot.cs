﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using infrastructure.Context;

#nullable disable

namespace ProyectoPlazoletaComidasPlazoleta.Migrations
{
    [DbContext(typeof(Db_Context))]
    partial class Db_ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Dominio.Modelos.EmpleadosRestaurantes", b =>
                {
                    b.Property<int>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<int>("RestauranteNIT")
                        .HasColumnType("int");

                    b.HasKey("EmpleadoId");

                    b.ToTable("EmpleadosRestaurantes");
                });

            modelBuilder.Entity("Dominio.Modelos.Pedidos", b =>
                {
                    b.Property<Guid>("Pedido_Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Chef_Id")
                        .HasColumnType("int");

                    b.Property<string>("Cliente_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("RestaurantesNIT_Id")
                        .HasColumnType("int");

                    b.HasKey("Pedido_Id");

                    b.HasIndex("RestaurantesNIT_Id");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("Dominio.Modelos.PedidosPlatos", b =>
                {
                    b.Property<Guid>("Pedido_Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.HasKey("Pedido_Id", "Id");

                    b.HasIndex("Id");

                    b.ToTable("PedidosPlatos");
                });

            modelBuilder.Entity("Dominio.Modelos.Platos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Desacripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombrePlato")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Precio")
                        .HasColumnType("int");

                    b.Property<int>("RestaurantesNIT_Id")
                        .HasColumnType("int");

                    b.Property<string>("URLImagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantesNIT_Id");

                    b.ToTable("Platos");
                });

            modelBuilder.Entity("Dominio.Modelos.Restaurantes", b =>
                {
                    b.Property<int>("NIT_Id")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DocumentoId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("URLLogo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NIT_Id");

                    b.ToTable("Restaurantes");
                });

            modelBuilder.Entity("Dominio.Modelos.Pedidos", b =>
                {
                    b.HasOne("Dominio.Modelos.Restaurantes", null)
                        .WithMany("pedidos")
                        .HasForeignKey("RestaurantesNIT_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Modelos.PedidosPlatos", b =>
                {
                    b.HasOne("Dominio.Modelos.Platos", "Platos")
                        .WithMany("PedidosPlatos")
                        .HasForeignKey("Id")
                        .IsRequired();

                    b.HasOne("Dominio.Modelos.Pedidos", "Pedidos")
                        .WithMany("PedidosPlatos")
                        .HasForeignKey("Pedido_Id")
                        .IsRequired();

                    b.Navigation("Pedidos");

                    b.Navigation("Platos");
                });

            modelBuilder.Entity("Dominio.Modelos.Platos", b =>
                {
                    b.HasOne("Dominio.Modelos.Restaurantes", null)
                        .WithMany("platos")
                        .HasForeignKey("RestaurantesNIT_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Modelos.Pedidos", b =>
                {
                    b.Navigation("PedidosPlatos");
                });

            modelBuilder.Entity("Dominio.Modelos.Platos", b =>
                {
                    b.Navigation("PedidosPlatos");
                });

            modelBuilder.Entity("Dominio.Modelos.Restaurantes", b =>
                {
                    b.Navigation("pedidos");

                    b.Navigation("platos");
                });
#pragma warning restore 612, 618
        }
    }
}
