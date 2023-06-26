using Aplicacion.Interfaces;
using Aplicacion.Servicios;
using AutoMapper;
using Dominio.Mapp;
using Dominio.Modelos;
using Dominio.Repositorios;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using PlazoletaComidas.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class HU1Test:Program
    {      

        private Mock <IUserRespository> repoUser = new Mock<IUserRespository>();

        private IMapper mapper;

        private Data data;

        public HU1Test() 
        {
            data = new Data();           
        }

        [Fact]
        public void UserInserter()
        {            
            var userDTO = data.UserAdmin();
            var config = new MapperConfiguration(opts => opts.AddMaps(new[]
            {
                typeof(MappUserModeltoUserDTO),               
            }));

            mapper = config.CreateMapper();
            UserService service = new UserService(repoUser.Object, mapper);
            service.AddOwner(userDTO);
        }


        [Fact]
        public void ValidEmail() 
        {
            var userDTO = data.WrongEmail();
            var config = new MapperConfiguration(opts => opts.AddMaps(new[]
            {
                typeof(MappUserModeltoUserDTO),
            }));

            mapper = config.CreateMapper();
            var user = mapper.Map<Usuarios>(userDTO);  
            var lstErrores= ValidateModel(user);
            Assert.Equal(1, lstErrores.Count);          
            
        }

        [Fact]
        public void ValidPhone()
        {
            try
            {
                var userDTO = data.WronPhone();
                var config = new MapperConfiguration(opts => opts.AddMaps(new[]
                {
                typeof(MappUserModeltoUserDTO),
            }));


                mapper = config.CreateMapper();
                UserService service = new UserService(repoUser.Object, mapper);
                service.AddOwner(userDTO);

            }catch(Exception ex)
            {
                Assert.Contains("Invalid Phone", ex.Message);
            }           
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

    }
}
