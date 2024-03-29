﻿using Applicacion.Repositorio;
using Dominio.Modelos;
using infrastructure.Context;
using infrastructure.Repositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using Aplicacion.Repositorio;
using Moq;
using Dominio.Repositorios;
using System;
using System.Runtime.InteropServices.ObjectiveC;
using AutoMapper;
using Dominio.Mapp;
using Dominio.DTO;
using System.Threading.Tasks;
using Dominio.User_Case;

namespace Test
{
    public class HU7Test
    {
        private Mock<IRestaurantRespository<Restaurantes, int>> repoRestaurant = new Mock<IRestaurantRespository<Restaurantes, int>>();
        private Mock<IDishesRepository<Platos, string, int>> repoDish = new Mock<IDishesRepository<Platos, string, int>>();
        private Mock<IRoles> Roles = new Mock<IRoles>();
        private Mock<IDishes> useDishes = new Mock<IDishes>();
        private IMapper mapper;
        private Data data;

        public HU7Test()
        {
            data = new Data();
            var config = new MapperConfiguration(opts => opts.AddMaps(new[]
              {
                typeof(Mapp),
            }));

            mapper = config.CreateMapper();
        }

        [Fact]
        public async Task ValidateRolEditDish()
        {
            try
            {
                PlatosDTO DishDTO = data.DishesDTO();
                Platos dish = data.Dish();
                UsuarioClaims claims = data.UserAdminClaims();
                Restaurantes rest = data.Restaurant();
                Task<UsuarioClaims> task2 = Task<UsuarioClaims>.Factory.StartNew(() =>
                {                   
                    return claims;
                });
                repoDish.Setup(x => x.GetByRestaurantNIT(dish.NombrePlato, dish.RestaurantesNIT_Id)).Returns(dish);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                repoRestaurant.Setup(x => x.GetByID(dish.RestaurantesNIT_Id)).Returns(rest);
                Dishes usecase = new Dishes();
                usecase.ValidateRol(task2);
                usecase.ValidateRestaurant(rest, task2);
                usecase.ValidateDish(dish);
                DishesService servicio = new DishesService(repoDish.Object, repoRestaurant.Object, Roles.Object, mapper, useDishes.Object);
                var resultado = await servicio.EditDish(DishDTO);

            }
            catch (Exception ex)
            {
                Assert.Contains("User Not authorized", ex.Message);
            }
        }

        [Fact]
        public async Task ValidaPropietarioRestauranteEditarPlato()
        {
            try
            {
                PlatosDTO platoDTO = new PlatosDTO()
                {
                    NombrePlato = "Pollo sudado",
                    Precio = 5000,
                    Desacripcion = "Pollo sudado",
                    Activo = false,
                    RestaurantesNIT_Id = 32235
                };

                Platos plato = new Platos()
                {
                    NombrePlato = "Pollo sudado",
                    Precio = 5000,
                    Desacripcion = "Pollo sudado",
                    Activo = false,
                    RestaurantesNIT_Id = 32235
                };

                Platos platoconsultado = new Platos()
                {
                    Id = 5,
                    NombrePlato = "Pollo sudado",
                    Precio = 5000,
                    Desacripcion = "Pollo sudado",
                    URLImagen = "Pollosudado",
                    Activo = true,
                    Categoria = "Pollo",
                    RestaurantesNIT_Id = 32235
                };

                UsuarioClaims claims = new UsuarioClaims()
                {
                    Rol = "2",
                    Id = "10184841",
                    Correo = "mapu@gmail.com"
                };

                Restaurantes rest = new Restaurantes()
                {
                    NIT_Id = 32235,
                    Nombre = "la hambugueseria123",
                    Direccion = "carrera33b#28-50",
                    Telefono = "314993783",
                    URLLogo = "mcpollo.img",
                    DocumentoId = 101231
                };

                Platos original = new Platos()
                {
                    Id = 5,
                    NombrePlato = "Pollo sudado",
                    Precio = 5000,
                    Desacripcion = "Pollo sudado",
                    URLImagen = "Pollosudado",
                    Activo = true,
                    Categoria = "Pollo",
                    RestaurantesNIT_Id = 32235
                };

                Task<UsuarioClaims> task2 = Task<UsuarioClaims>.Factory.StartNew(() =>
                {                   
                    return claims;
                });
                repoDish.Setup(x => x.Add(plato)).Returns(plato);
                repoDish.Setup(x => x.GetByRestaurantNIT(plato.NombrePlato, plato.RestaurantesNIT_Id)).Returns(platoconsultado);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                repoRestaurant.Setup(x => x.GetByID(plato.RestaurantesNIT_Id)).Returns(rest);
                Dishes usecase = new Dishes();
                usecase.ValidateRol(task2);
                usecase.ValidateRestaurant(rest, task2);
                usecase.ValidateDish(plato);
                DishesService servicio = new DishesService(repoDish.Object, repoRestaurant.Object, Roles.Object, mapper, useDishes.Object);
                var resultado = await servicio.EditDish(platoDTO);
            }
            catch (Exception e)
            {
                Assert.Equal("The User cannot insert a dish to another restaurant", e.Message);
            }
        }

    }

}
