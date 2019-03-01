using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarPark.Api.ApplicationCore.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Carpark> Carparks { get; set; }
        public string Token { get; set; }

        public User()
        {
            Carparks = new List<Carpark>();   
        }
    }
}
