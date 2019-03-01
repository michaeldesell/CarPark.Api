using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CarPark.Api.ApplicationCore.AutoMapper;

namespace CarPark.Api.xUnitTest.AutoMapper
{
    class FakeIMapper
    {
        IMapper Mapper;

        public FakeIMapper()
        {
            var profile = new MappingProfile();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));

            Mapper = new Mapper(configuration);
            
        }

        public IMapper GetMapper()
        {
            return Mapper;

        }
    }
}
