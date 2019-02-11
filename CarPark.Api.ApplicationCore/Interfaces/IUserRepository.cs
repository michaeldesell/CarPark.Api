using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPark.Api.ApplicationCore.Entities;
using CarPark.Api.ApplicationCore.Interfaces;
using System.Collections;

namespace CarPark.Api.ApplicationCore.Interfaces
{
    public interface IUserRepository
    {
        void Add(User u);
        void Edit(User u);
        void Remove(User u);
        User FindById(int id);
        IEnumerable GetUsers();
    }
}
