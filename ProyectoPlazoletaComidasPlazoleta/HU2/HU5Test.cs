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
using Dominio.User_Case;
using System.Threading.Tasks;

namespace Test
{
    public class HU5Test
    {
        private Mock<IDishesRepository<Platos, string, int>> repoDish = new Mock<IDishesRepository<Platos, string, int>>();
        private Mock<IRestaurantRespository<Restaurantes, int>> repoRestaurant = new Mock<IRestaurantRespository<Restaurantes, int>>();
        private Mock<IUsersRemotoRepository<Usuarios, int>> repoUsers = new Mock<IUsersRemotoRepository<Usuarios, int>>();
        private Mock<IRoles> Roles = new Mock<IRoles>();
        private Mock<IEmployeeRestaurantRepository<EmpleadosRestaurantes, int>> repoEmplyeeRestaurant = new Mock<IEmployeeRestaurantRepository<EmpleadosRestaurantes, int>>();
        private Mock<IRestaurant> useCaseRest = new Mock<IRestaurant>();
        private Mock<IDishes> useDishes = new Mock<IDishes>();
        private IMapper mapper;
        private Data data;

        public HU5Test()
        {
            data = new Data();
        }


        [Fact]
        public async Task CreteRestaurantValidRol()
        {
            try
            {
                var config = new MapperConfiguration(opts => opts.AddMaps(new[]
            {
                typeof(Mapp),
            }));

                mapper = config.CreateMapper();

                RestaurantesDTO restDTO = data.RestaurantDTO();
                Restaurantes rest = mapper.Map<Restaurantes>(restDTO);
                UsuarioClaims claims = data.UserOwnerClaims();
                Usuarios usuarios = data.Users();
                Task<UsuarioClaims> task2 = Task<UsuarioClaims>.Factory.StartNew(() =>
                {                   
                    return claims;
                });
                repoRestaurant.Setup(x => x.Add(rest)).Returns(rest);
                repoUsers.Setup(x => x.GetUserID(rest.DocumentoId)).ReturnsAsync(usuarios);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                Restaurant useCase = new Restaurant();
                useCase.ValidateRol(task2);
                useCase.ValidateModel(rest);
                useCase.ValidateUser(usuarios);
                RestaurantService service = new RestaurantService(repoRestaurant.Object, repoUsers.Object, Roles.Object, repoEmplyeeRestaurant.Object, mapper, useCaseRest.Object);
                var result = service.AddRestaurant(restDTO);
            }
            catch (Exception ex)
            {
                Assert.Equal("User Not authorized", ex.Message);
            }
        }
        [Fact]
        public async Task CreateDishValidateRol()
        {
            try
            {
                var config = new MapperConfiguration(opts => opts.AddMaps(new[]
                {
                    typeof(Mapp),
                }));

                mapper = config.CreateMapper();
                PlatosDTO DishDTO = data.DishesDTO();
                Platos dish = data.Dish();
                UsuarioClaims claims = data.UserAdminClaims();
                Restaurantes rest = data.Restaurant();
                Task<UsuarioClaims> task2 = Task<UsuarioClaims>.Factory.StartNew(() =>
                {                    
                    return claims;
                });
                repoDish.Setup(x => x.Add(dish)).Returns(dish);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                repoRestaurant.Setup(x => x.GetByID(dish.RestaurantesNIT_Id)).Returns(rest);
                Dishes usecase = new Dishes();
                usecase.ValidateRol(task2);
                usecase.ValidateRestaurant(rest, task2);
                DishesService servicio = new DishesService(repoDish.Object, repoRestaurant.Object, Roles.Object, mapper, useDishes.Object);
                var resultado = await servicio.AddDish(DishDTO);

            }
            catch (Exception ex)
            {
                Assert.Contains("User Not authorized", ex.Message);
            }
        }


        [Fact]
        public async Task CreateDishValidateUser()
        {
            try
            {
                var config = new MapperConfiguration(opts => opts.AddMaps(new[]
                {
                    typeof(Mapp),
                }));

                mapper = config.CreateMapper();
                PlatosDTO DishDTO = data.DishesDTO();
                Platos dish = data.Dish();
                UsuarioClaims claims = data.UserOwnerClaims2();
                Restaurantes rest = data.Restaurant();
                Task<UsuarioClaims> task2 = Task<UsuarioClaims>.Factory.StartNew(() =>
                {                  
                    return claims;
                });
                repoDish.Setup(x => x.Add(dish)).Returns(dish);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                repoRestaurant.Setup(x => x.GetByID(dish.RestaurantesNIT_Id)).Returns(rest);
                Dishes usecase = new Dishes();
                usecase.ValidateRol(task2);
                usecase.ValidateRestaurant(rest, task2);
                DishesService servicio = new DishesService(repoDish.Object, repoRestaurant.Object, Roles.Object, mapper, useDishes.Object);
                var resultado = await servicio.AddDish(DishDTO);

            }
            catch (Exception ex)
            {
                Assert.Contains("The User cannot insert a dish to another restaurant", ex.Message);
            }
        }
    }
}
