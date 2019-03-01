using CarPark.Api.Infrastructure.EF_Core.DBContext;
using CarPark.Api.Infrastructure.Repositories;
using CarPark.Api.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.Api.Infrastructure.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataAccessServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CarParkDbContext>(options =>
              options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
        }

        public static void AddRepServDIService(this IServiceCollection services)
        {
            services.AddScoped<UserRepository, UserRepository>();
            services.AddScoped<CarparkRepository, CarparkRepository>();
            services.AddScoped<FloorRepository, FloorRepository>();
            services.AddScoped<ParkingspaceRepository, ParkingspaceRepository>();
            services.AddScoped<CarRepository, CarRepository>();
            services.AddScoped<UserService, UserService>();


        }

        public static void AddJwtService(this IServiceCollection services,IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration["AppSettings:Secret"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


        }

    }
}
