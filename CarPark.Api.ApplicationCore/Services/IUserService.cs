using CarPark.Api.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.Api.ApplicationCore.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
}
