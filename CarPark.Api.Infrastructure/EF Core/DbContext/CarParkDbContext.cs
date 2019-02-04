using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarPark.Api.Infrastructure.EF_Core.Entities;

namespace CarPark.Api.Infrastructure.EF_Core.DbContext
{
    public class CarParkDbContext : IdentityDbContext<User>
    {



    }
}
