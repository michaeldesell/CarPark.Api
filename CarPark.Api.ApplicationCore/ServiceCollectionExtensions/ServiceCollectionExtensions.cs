using AutoMapper;
using CarPark.Api.ApplicationCore.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.Api.ApplicationCore.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddAutoMapperService(this IServiceCollection services)
        {

            IMapper Mapper;
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());

            });

            Mapper = mappingConfig.CreateMapper();

            services.AddSingleton(Mapper);
        }


    }
}
