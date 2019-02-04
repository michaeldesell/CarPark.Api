using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarPark.Api.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore.Design;

namespace CarPark.Api.Infrastructure.EF_Core.DbContext
{
    public class CarParkDbContext : IdentityDbContext<User>
    {

        public CarParkDbContext(DbContextOptions<CarParkDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

    }

}
