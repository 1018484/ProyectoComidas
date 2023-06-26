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
using Dominio.Repositorios;
using Moq;
using AutoMapper;
using Test;
using System.ComponentModel.DataAnnotations;
using Dominio.Mapp;
using Dominio.DTO;
using Dominio.User_Case;

namespace HU2
{
    public class HU2Test
    {
        private Mock<IRestaurantRespository<Restaurantes, int>> repoRestaurant = new Mock<IRestaurantRespository<Restaurantes, int>>();
        private Mock<IUsersRemotoRepository<Usuarios, int>> repoUsers = new Mock<IUsersRemotoRepository<Usuarios, int>>();
        private Mock<IRoles> Roles = new Mock<IRoles>();
        private Mock<IEmployeeRestaurantRepository<EmpleadosRestaurantes, int>> repoEmpleadoRestaurant = new Mock<IEmployeeRestaurantRepository<EmpleadosRestaurantes, int>>();
        private Mock<IRestaurant> useRestaurant = new Mock<IRestaurant>();
        private Mock<IDishes> useDishes = new Mock<IDishes>();
        private IMapper mapper;
        private Data data;

        public HU2Test()
        {
            data = new Data();
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Fact]
        public async Task CreateRestaurant()
        {
            try
            {
                RestaurantesDTO restDTO = data.RestaurantDTO();
                Restaurantes rest = data.Restaurant();
                UsuarioClaims claims = data.UserAdminClaims();
                Usuarios usuarios = data.Users();
                var config = new MapperConfiguration(opts => opts.AddMaps(new[]
                {
                typeof(Mapp),
            }));

                mapper = config.CreateMapper();
                repoRestaurant.Setup(x => x.Add(rest)).Returns(rest);
                repoUsers.Setup(x => x.GetUserID(rest.DocumentoId)).ReturnsAsync(usuarios);
                var r = Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                Restaurant useCase = new Restaurant();                
                useCase.ValidateModel(rest);
                useCase.ValidateUser(usuarios);
                RestaurantService servicio = new RestaurantService(repoRestaurant.Object, repoUsers.Object, Roles.Object, repoEmpleadoRestaurant.Object, mapper, useRestaurant.Object);
                var result = servicio.AddRestaurant(restDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        [Fact]
        public async Task UserDoesNotExist()
        {
            try
            {
                RestaurantesDTO restDTO = data.RestaurantDTO();
                Restaurantes rest = data.Restaurant();
                UsuarioClaims claims = data.UserAdminClaims();
                Usuarios usuarios = data.Users();
                var config = new MapperConfiguration(opts => opts.AddMaps(new[]
                {
                typeof(Mapp),
                }));

                mapper = config.CreateMapper();
                usuarios = null;
                repoRestaurant.Setup(x => x.Add(rest)).Returns(rest);
                repoUsers.Setup(x => x.GetUserID(rest.DocumentoId)).ReturnsAsync(usuarios);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                useRestaurant.Setup(x => x.ValidateModel(rest));
                useRestaurant.Setup(x => x.ValidateUser(usuarios));
                Restaurant useCase = new Restaurant();
                useCase.ValidateModel(rest);
                useCase.ValidateUser(usuarios);
                RestaurantService servicio = new RestaurantService(repoRestaurant.Object, repoUsers.Object, Roles.Object, repoEmpleadoRestaurant.Object, mapper, useRestaurant.Object);
                var result = servicio.AddRestaurant(restDTO);

            }
            catch (Exception ex)
            {
                Assert.Contains("You are trying to add a restaurant to an unregistered User", ex.Message);
            }
        }

        [Fact]
        public async Task ValidatePhone()
        {
            try
            {
                RestaurantesDTO restDTO = data.RestaurantWrongPhone();
                Restaurantes rest = data.Restaurant();
                UsuarioClaims claims = data.UserAdminClaims();
                Usuarios usuarios = data.Users();
                var config = new MapperConfiguration(opts => opts.AddMaps(new[]
                {
                typeof(Mapp),
                }));

                rest.Telefono = "312312asd";
                mapper = config.CreateMapper();
                repoRestaurant.Setup(x => x.Add(rest)).Returns(rest);
                repoUsers.Setup(x => x.GetUserID(rest.DocumentoId)).ReturnsAsync(usuarios);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                Restaurant useCase = new Restaurant();
                useCase.ValidateModel(rest);
                useCase.ValidateUser(usuarios);
                RestaurantService servicio = new RestaurantService(repoRestaurant.Object, repoUsers.Object, Roles.Object, repoEmpleadoRestaurant.Object, mapper, useRestaurant.Object);
                var result = servicio.AddRestaurant(restDTO);

            }
            catch (Exception ex)
            {
                Assert.Contains("Invalid Phone", ex.Message); ;
            }
        }

        [Fact]
        public async Task ValidatePhoneNumberLarge()
        {
            try
            {
                RestaurantesDTO restDTO = data.RestaurantDTO();
                Restaurantes rest = data.Restaurant();
                UsuarioClaims claims = data.UserAdminClaims();
                Usuarios usuarios = data.Users();
                var config = new MapperConfiguration(opts => opts.AddMaps(new[]
                {
                typeof(Mapp),
                }));

                rest.Telefono = "3123123123123123";
                mapper = config.CreateMapper();
                repoRestaurant.Setup(x => x.Add(rest)).Returns(rest);
                repoUsers.Setup(x => x.GetUserID(rest.DocumentoId)).ReturnsAsync(usuarios);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                Restaurant useCase = new Restaurant();
                useCase.ValidateModel(rest);
                useCase.ValidateUser(usuarios);
                RestaurantService servicio = new RestaurantService(repoRestaurant.Object, repoUsers.Object, Roles.Object, repoEmpleadoRestaurant.Object, mapper, useRestaurant.Object);
                var result = servicio.AddRestaurant(restDTO);

            }
            catch (Exception ex)
            {
                Assert.Contains("Invalid Phone", ex.Message); ;
            }
        }

        [Fact]
        public async Task PhoneNumberAceptIndicator()
        {

            try
            {
                RestaurantesDTO restDTO = data.RestaurantDTO();
                Restaurantes rest = data.Restaurant();
                UsuarioClaims claims = data.UserAdminClaims();
                Usuarios usuarios = data.Users();
                var config = new MapperConfiguration(opts => opts.AddMaps(new[]
                {
                typeof(Mapp),
                }));

                restDTO.Telefono = "+573132408264";
                mapper = config.CreateMapper();
                repoRestaurant.Setup(x => x.Add(rest)).Returns(rest);
                repoUsers.Setup(x => x.GetUserID(rest.DocumentoId)).ReturnsAsync(usuarios);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                Restaurant useCase = new Restaurant();
                useCase.ValidateModel(rest);
                useCase.ValidateUser(usuarios);
                RestaurantService servicio = new RestaurantService(repoRestaurant.Object, repoUsers.Object, Roles.Object, repoEmpleadoRestaurant.Object, mapper, useRestaurant.Object);
                var result = servicio.AddRestaurant(restDTO);

            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }
        }

        [Fact]
        public async Task Validanombrederestaurante()
        {
            try
            {
                RestaurantesDTO restDTO = data.RestaurantDTO();
                Restaurantes rest = data.Restaurant();
                UsuarioClaims claims = data.UserAdminClaims();
                Usuarios usuarios = data.Users();
                var config = new MapperConfiguration(opts => opts.AddMaps(new[]
                {
                typeof(Mapp),
                }));

                restDTO.Nombre = "M4CDonalds";
                mapper = config.CreateMapper();
                repoRestaurant.Setup(x => x.Add(rest)).Returns(rest);
                repoUsers.Setup(x => x.GetUserID(rest.DocumentoId)).ReturnsAsync(usuarios);
                Roles.Setup(x => x.getToken()).ReturnsAsync(claims);
                Restaurant useCase = new Restaurant();
                useCase.ValidateModel(rest);
                useCase.ValidateUser(usuarios);
                RestaurantService servicio = new RestaurantService(repoRestaurant.Object, repoUsers.Object, Roles.Object, repoEmpleadoRestaurant.Object, mapper, useRestaurant.Object);
                var result = servicio.AddRestaurant(restDTO);

            }
            catch (Exception ex)
            {
                Assert.Contains("Invalid Restaurant Name", ex.Message); ;
            }
        }
    }
}