using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;
using CarPark.Api.Infrastructure.ServiceCollectionExtensions;
using CarPark.Api.ApplicationCore.ServiceCollectionExtensions;

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
            services.AddDataAccessServices(Configuration.GetConnectionString("DBConnectionString"));

            //Configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Carpark API", Version = "v1" });
            });

            //Configure Automapper mappings
            services.AddAutoMapperService();

            //Add repositories and services for dependency injection
            services.AddRepServDIService();

            services.AddCors();

            //Add service for JWT
            services.AddJwtService(Configuration);

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

            //Add logging
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //Add SwaggerUI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                        {
                            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Carpark API V1");

                        });

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //Redirect to SwaggerUI
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseMvc();
        }

    }


}
