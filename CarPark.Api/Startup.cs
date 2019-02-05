using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CarPark.Api.Infrastructure.EF_Core.DbContext;
using Microsoft.EntityFrameworkCore;
using CarPark.Api.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using CarPark.Api.Infrastructure.Service;

namespace CarPark.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
         
            DbContextOptionsBuilder<CarParkDbContext> options = new DbContextOptionsBuilder<CarParkDbContext>();

            options.UseSqlServer<CarParkDbContext>(Configuration.GetConnectionString("DBConnectionString"));
            services.AddDbContext<CarParkDbContext>();
            services.AddIdentity<User, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<CarParkDbContext>()
                .AddDefaultTokenProviders();


            services.AddMvc();
            services.AddScoped<iapplicationservice, applicationservice>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {

                app.UseHsts();

            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }

   
}
