using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarPark.Api.ApplicationCore.Entities;
using CarPark.Api.ApplicationCore.Models;

namespace CarPark.Api.ApplicationCore.Interfaces
{
    public interface ICarparkRepository
    {
        void Add(Carpark cp);
        void Edit(Carpark cp);
        void Remove(Carpark cp);
        IEnumerable GetCarparks();
        Carpark GetCarpark(int carparkid);
        IEnumerable GetCarparksByUser(string userid);
        Task<int> SaveAllAsync();
    }
}
