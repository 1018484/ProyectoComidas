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
using Test;
using System.ComponentModel.DataAnnotations;
using Dominio.Mapp;
using Dominio.DTO;

namespace HU2
{
    public class HU3Test
    {
        private Mock<IRestaurantRespository<Restaurantes, int>> repoRestaurant = new Mock<IRestaurantRespository<Restaurantes, int>>();
        private Mock<IDishesRepository<Platos, string, int>> repoDish = new Mock<IDishesRepository<Platos, string, int>>();
        private Mock<IRoles> Roles = new Mock<IRoles>();
        private IMapper mapper;
        private Data data;

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        public HU3Test() 
        {
            data = new Data();
        }

        [Fact]
        public async Task CreateDish()
        {
            var config = new MapperConfiguration(opts => opts.AddMaps(new[]
            {
                typeof(Mapprestaurant),
            }));

            mapper = config.CreateMapper();
            PlatosDTO DishDTO = data.DishesDTO();
            Platos dish = data.Dish();            
            UsuarioClaims claims = data.UserOwnerClaims();
            Restaurantes rest = data.Restaurant();            
            repoDish.Setup(x => x.Add(dish)).Returns(dish);
            Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
            repoRestaurant.Setup(x => x.GetByID(dish.RestaurantesNIT_Id)).Returns(rest);
            DishesService servicio = new DishesService(repoDish.Object, repoRestaurant.Object, Roles.Object, mapper);
            var resultado = await servicio.AddDish(DishDTO);            
        }

        [Fact]
        public async Task ValidateRol()
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
        public async Task ExisteRestaurante()
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
                UsuarioClaims claims = data.UserOwnerClaims();
                Restaurantes rest = null;
                repoDish.Setup(x => x.Add(dish)).Returns(dish);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                repoRestaurant.Setup(x => x.GetByID(dish.RestaurantesNIT_Id)).Returns(rest);
                DishesService servicio = new DishesService(repoDish.Object, repoRestaurant.Object, Roles.Object, mapper);
                var resultado = await servicio.AddDish(DishDTO);

            }
            catch (Exception ex)
            {
                Assert.Contains("The restaurant does not exist", ex.Message);
            }
        }

        [Fact]
        public async Task EditarPlato()
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
                UsuarioClaims claims = data.UserOwnerClaims();
                Restaurantes rest = data.Restaurant();
                repoDish.Setup(x => x.GetByRestaurantNIT(dish.NombrePlato, dish.RestaurantesNIT_Id)).Returns(dish);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                repoRestaurant.Setup(x => x.GetByID(dish.RestaurantesNIT_Id)).Returns(rest);
                DishesService servicio = new DishesService(repoDish.Object, repoRestaurant.Object, Roles.Object, mapper);
                var resultado = await servicio.EditDish(DishDTO);

            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }
        }
    }
}
