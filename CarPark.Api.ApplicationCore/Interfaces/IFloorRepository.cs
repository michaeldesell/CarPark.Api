using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarPark.Api.ApplicationCore.Entities;

namespace CarPark.Api.ApplicationCore.Interfaces
{
    public interface IFloorRepository
    {
        void Add(Floor f);
        void Edit(Floor f);
        void Remove(Floor f);
        Floor GetFloor(int floorid);
        IEnumerable GetFloorsByCarpark(int id);
        Task<int> SaveAllAsync();
    }
}
