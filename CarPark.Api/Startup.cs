using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CarPark.Api.Infrastructure.EF_Core.DBContext;
using Microsoft.EntityFrameworkCore;
using CarPark.Api.Infrastructure.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.Security.Claims;
using AutoMapper;
using CarPark.Api.ApplicationCore.AutoMapper;
using CarPark.Api.Infrastructure.Repositories;
using CarPark.Api.ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CarPark.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;


                });

            //Fetch Connectionstring from config
            services.AddDbContext<CarParkDbContext>(cfg =>
            {
                cfg.UseSqlServer(
                    Configuration.GetConnectionString("DBConnectionString")
                    );
            }
            , ServiceLifetime.Scoped);

            //Configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Carpark API", Version = "v1" });
            });

            //Configure Automapper mappings

            IMapper Mapper;
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());

            });

            Mapper = mappingConfig.CreateMapper();

            services.AddSingleton(Mapper);

            //Add repositories for dependency injection
            services.AddScoped<UserRepository, UserRepository>();
            services.AddScoped<CarparkRepository, CarparkRepository>();
            services.AddScoped<FloorRepository, FloorRepository>();
            services.AddScoped<ParkingspaceRepository, ParkingspaceRepository>();
            services.AddScoped<CarRepository, CarRepository>();
            services.AddScoped<UserService, UserService>();

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var key = Encoding.ASCII.GetBytes(Configuration["AppSettings:Secret"]);

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





            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {

                app.UseHsts();

            }
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                        {
                            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Carpark API V1");

                        });
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);
            app.UseMvc();
        }

    }


}
