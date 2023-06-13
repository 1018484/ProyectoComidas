using Applicacion.Repositorio;
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

namespace Test
{
    public class HU5Test
    {
        private Mock<IDishesRepository<Platos, string, int>> repoDish = new Mock<IDishesRepository<Platos, string, int>>();
        private Mock<IRestaurantRespository<Restaurantes, int>> repoRestaurant = new Mock<IRestaurantRespository<Restaurantes, int>>();
        private Mock<IUsersRemotoRepository<Usuarios, int>> repoUsers = new Mock<IUsersRemotoRepository<Usuarios, int>>();
        private Mock<IRoles> Roles = new Mock<IRoles>();
        private Mock<IEmployeeRestaurantRepository<EmpleadosRestaurantes, int>> repoEmplyeeRestaurant = new Mock<IEmployeeRestaurantRepository<EmpleadosRestaurantes, int>>();
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
                typeof(Mapprestaurant),
            }));

                mapper = config.CreateMapper();

                RestaurantesDTO restDTO = data.RestaurantDTO();
                Restaurantes rest = mapper.Map<Restaurantes>(restDTO);
                UsuarioClaims claims = data.UserOwnerClaims();
                Usuarios usuarios = data.Users();
                repoRestaurant.Setup(x => x.Add(rest)).Returns(rest);
                repoUsers.Setup(x => x.GetUserID(rest.DocumentoId)).ReturnsAsync(usuarios);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                RestaurantService service = new RestaurantService(repoRestaurant.Object, repoUsers.Object, Roles.Object, repoEmplyeeRestaurant.Object, mapper);
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
                    typeof(Mapprestaurant),
                }));

                mapper = config.CreateMapper();
                PlatosDTO DishDTO = data.DishesDTO();
                Platos dish = data.Dish();
                UsuarioClaims claims = data.UserAdminClaims();
                Restaurantes rest = data.Restaurant();
                repoDish.Setup(x => x.Add(dish)).Returns(dish);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                repoRestaurant.Setup(x => x.GetByID(dish.RestaurantesNIT_Id)).Returns(rest);
                DishesService servicio = new DishesService(repoDish.Object, repoRestaurant.Object, Roles.Object, mapper);
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
                    typeof(Mapprestaurant),
                }));

                mapper = config.CreateMapper();
                PlatosDTO DishDTO = data.DishesDTO();
                Platos dish = data.Dish();
                UsuarioClaims claims = data.UserOwnerClaims2();
                Restaurantes rest = data.Restaurant();
                repoDish.Setup(x => x.Add(dish)).Returns(dish);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                repoRestaurant.Setup(x => x.GetByID(dish.RestaurantesNIT_Id)).Returns(rest);
                DishesService servicio = new DishesService(repoDish.Object, repoRestaurant.Object, Roles.Object, mapper);
                var resultado = await servicio.AddDish(DishDTO);

            }
            catch (Exception ex)
            {
                Assert.Contains("The User cannot insert a dish to another restaurant", ex.Message);
            }
        } 
    }
}
