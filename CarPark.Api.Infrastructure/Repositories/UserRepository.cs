using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPark.Api.ApplicationCore.Entities;
using CarPark.Api.ApplicationCore.Models;
using CarPark.Api.ApplicationCore.Interfaces;
using CarPark.Api.Infrastructure.EF_Core.DBContext;
using System.Collections;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarPark.Api.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        CarParkDbContext Context;

        IMapper Mapper;

        public UserRepository(CarParkDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public void Add(User user)
        {
            Context.Users.Add(user);
        }

        public void Edit(UserModel user)
        {


        }

        public void Remove(string id)
        {
            var result = Context.Users.Where(u => u.Id.Equals(id)).FirstOrDefault();
            Context.Users.Remove(result);

        }

        public Task<int> SaveAllAsync()
        {
            return Context.SaveChangesAsync();

        }

        public User GetUser(string id)
        {
            var result = Context.Users
                .Include(u => u.Carparks)
                .Where(u => u.Id.Equals(id)).FirstOrDefault();
            return result;
        }
        public IEnumerable<User> GetUsers()
        {
            var result = Context.Users
                .Include(u => u.Carparks)
                .ToList();
            return result;
        }


    }
}
