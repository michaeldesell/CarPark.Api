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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Usersed
            User CarPark = new User
            {
                Id = "1",
                UserName = "CarParkWeb",
                PasswordHash = "Banan",
                Email = "a@b.se",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            };

            //////Seeding//////////
            User User = new User { Id = "2", UserName = "Testuser", PasswordHash = "Banan", Email = "atarda@b.se", EmailConfirmed = true, PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0 };
            Microsoft.AspNetCore.Identity.IdentityRole carparkrole = new Microsoft.AspNetCore.Identity.IdentityRole { Id = "1", Name = "CarParkWeb", NormalizedName = "carparkweb" };
            Microsoft.AspNetCore.Identity.IdentityRole userrole = new Microsoft.AspNetCore.Identity.IdentityRole { Id = "2", Name = "User", NormalizedName = "user" };
            Microsoft.AspNetCore.Identity.IdentityUserRole<string> connectCarPark = new Microsoft.AspNetCore.Identity.IdentityUserRole<string>() { UserId = CarPark.Id, RoleId = carparkrole.Id };
            Microsoft.AspNetCore.Identity.IdentityUserRole<string> connectUser = new Microsoft.AspNetCore.Identity.IdentityUserRole<string>() { UserId = User.Id, RoleId = userrole.Id };
            Microsoft.AspNetCore.Identity.IdentityRoleClaim<string> connectCarParkClaim = new Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>() { Id = 1, RoleId = carparkrole.Id, ClaimType = "CarParkApp", ClaimValue = "carparkapp" };
            Microsoft.AspNetCore.Identity.IdentityRoleClaim<string> connectUserClaim = new Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>() { Id = 2, RoleId = userrole.Id, ClaimType = "User", ClaimValue = "user" };
            
            modelBuilder.Entity<User>().HasData(CarPark);
            modelBuilder.Entity<User>().HasData(User);
            #endregion
            modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>().HasData(carparkrole);
            modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>().HasData(userrole);

            modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<string>>().HasData(connectCarPark);
            modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<string>>().HasData(connectUser);

            modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>>().HasData(connectCarParkClaim);
            modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>>().HasData(connectUserClaim);
            //////Seeding done//////////


        }
    }
}




  

    //public class CarParkDbContextFactory : IDesignTimeDbContextFactory<CarParkDbContext>
    //{
    //    CarParkDbContext IDesignTimeDbContextFactory<CarParkDbContext>.CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<CarParkDbContext>();
    //        optionsBuilder.UseSqlServer<CarParkDbContext>("server=(localdb)\\MSSQLLocalDB;Database=CarParkDb;Integrated Security=true;MultipleActiveResultSets=true");

    //        return new CarParkDbContext(optionsBuilder.Options);
    //    }
    //}


