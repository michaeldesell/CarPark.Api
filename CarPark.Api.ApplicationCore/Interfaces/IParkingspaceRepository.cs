using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarPark.Api.ApplicationCore.Entities;

namespace CarPark.Api.ApplicationCore.Interfaces
{
    public interface IParkingspaceRepository
    {
        void Add(Parkingspace ps);
        void Edit(Parkingspace ps);
        void Remove(Parkingspace ps);
        Parkingspace GetParkingspace(int pspaceid);
        IEnumerable GetPSpaceByFloor(int floorid);
        Task<int> SaveAllAsync();
    }
}
