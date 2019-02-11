using System;
using System.Collections.Generic;
using System.Text;
using CarPark.Api.ApplicationCore.Entities;

namespace CarPark.Api.ApplicationCore.Interfaces
{
    public interface IParkingspaceRepository
    {
        void Add(Parkingspace p);
        void Edit(Parkingspace p);
        void Remove(Parkingspace p);
        Parkingspace FindById(int id);
    }
}
