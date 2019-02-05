using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarPark.Api.ApplicationCore.Entities;

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
