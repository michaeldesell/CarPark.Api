using System;
using System.Collections.Generic;
using System.Text;
using CarPark.Api.Controllers;
using CarPark.Api.Infrastructure.Repositories;
using AutoMapper;
using CarPark.Api.xUnitTest.AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using CarPark.Api.Infrastructure.EF_Core.DBContext;
using CarPark.Api.xUnitTest.DBContext;

namespace CarPark.Api.xUnitTest.CarPark.Api.Controllers
{
    public class UsersController_Test
    {
        UsersController Controller;
        UserRepository Userrep;
        IMapper Mapper;
        ILogger<UsersController_Test> Logger;
        CarParkDbContext Context;



        public UsersController_Test()
        {
            //InMemoryContext context = new InMemoryContext();
            //Context = context.GetContext();
            //FakeIMapper mapper = new FakeIMapper();
            //Mapper = mapper.GetMapper();
            //var mock = new Mock<ILogger<UsersController_Test>>();
            //Logger = mock.Object;
            //Userrep = new UserRepository(Context,Mapper);
            
            //Controller = new UsersController(Mapper, Userrep, Logger);

        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            var okresult = Controller.Get();

            Assert.IsType<OkObjectResult>(okresult);
        }
    }
}
