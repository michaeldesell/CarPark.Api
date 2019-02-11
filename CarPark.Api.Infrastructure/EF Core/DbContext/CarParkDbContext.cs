using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarPark.Api.ApplicationCore.Entities;

namespace CarPark.Api.Infrastructure.EF_Core.DBContext
{
    public class CarParkDbContext : IdentityDbContext<User>
    {

        public CarParkDbContext(DbContextOptions<CarParkDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Carpark> Carparks { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Parkingspace> Parkingspaces { get; set; }
        public DbSet<Car> Cars { get; set; }
     


    }
 

}
