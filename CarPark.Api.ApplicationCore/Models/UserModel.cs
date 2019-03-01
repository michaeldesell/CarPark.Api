using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CarPark.Api.ApplicationCore.Entities;

namespace CarPark.Api.ApplicationCore.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string PasswordHash { get; set; }
        public List<Carpark> Carparks { get; set; }
        public string Token { get; set; }

    }
}
