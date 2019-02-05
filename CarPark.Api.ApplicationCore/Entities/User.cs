using Microsoft.AspNetCore.Identity;


namespace CarPark.Api.ApplicationCore.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        
    }
}
