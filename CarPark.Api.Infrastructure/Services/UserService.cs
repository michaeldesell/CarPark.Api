using CarPark.Api.ApplicationCore.Entities;
using CarPark.Api.ApplicationCore.Services;
using CarPark.Api.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CarPark.Api.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private IEnumerable<User> users = new List<User>();
        private UserRepository Userrep;
        private IConfiguration Configuration;

        public UserService(UserRepository userrep, IConfiguration configuration)
        {
            Userrep = userrep;
            users = GetAll();
            Configuration = configuration;
        }

        public User Authenticate(string username, string password)
        {
          
            var user = users.Where(u => u.UserName.Equals(username) && u.PasswordHash.Equals(password)).FirstOrDefault();

            if (user == null)
                return null;

            //Auth successful,generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["AppSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id)


                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            user.PasswordHash = null;
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return Userrep.GetUsers();
        }
    }
}



