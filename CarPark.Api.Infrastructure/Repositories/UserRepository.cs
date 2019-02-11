using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPark.Api.ApplicationCore.Entities;
using CarPark.Api.ApplicationCore.Interfaces;
using CarPark.Api.Infrastructure.EF_Core.DBContext;
using System.Collections;

namespace CarPark.Api.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        CarParkDbContext _context;

        public UserRepository(CarParkDbContext context)
        {
            _context = context;
        }

        public void Add(User u)
        {
            _context.Users.Add(u);
        }

        public void Edit(User u)
        {
            throw new NotImplementedException();
        }

        public void Remove(User u)
        {
            _context.Users.Remove(u);
        }
        public User FindById(int id)
        {
            var result = _context.Users
               .Where(u => u.Id == id).FirstOrDefault();
            return result;
        }

        public IEnumerable GetUsers()
        {
            var result = _context.Users.ToList();
            return result;
        }


    }
}
