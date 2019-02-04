using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarPark.Infrastructure.EFCore
{
    class CarParkDbContext : IdentityDbContext<User>
    {

        public CarParkDbContext(DbContextOptions options) : base(options)
        {

        }
		public DbSet<User> Users { get; set; }
    }
}
