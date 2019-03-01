using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPark.Api.ApplicationCore.Entities;
using CarPark.Api.ApplicationCore.Interfaces;
using System.Collections;
using CarPark.Api.ApplicationCore.Models;
using System.Threading.Tasks;

namespace CarPark.Api.ApplicationCore.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        void Edit(UserModel user);
        void Remove(string id);
        Task<int> SaveAllAsync();
        User GetUser(string id);
        IEnumerable<User> GetUsers();
    }
}
