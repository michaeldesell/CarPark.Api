using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarPark.Api.ApplicationCore.Entities;

namespace CarPark.Api.ApplicationCore.Interfaces
{
    public interface ICarRepository
    {

        void Add(Car c);
        void Edit(Car c);
        void Remove(Car c);
        Car GetCar(int carid);
        Car GetCarByParkingSpace(int parkingspaceid);
        IEnumerable GetCarsByCarpark(int carparkid);
        Task<int> SaveAllAsync();
    }
}
