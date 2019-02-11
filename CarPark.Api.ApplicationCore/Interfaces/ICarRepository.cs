using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CarPark.Api.ApplicationCore.Entities;

namespace CarPark.Api.ApplicationCore.Interfaces
{
    public interface ICarRepository
    {

        void Add(Car c);
        void Edit(Car c);
        void Remove(Car c);
        IEnumerable GetCars();
        Car FindById(int id);
    }
}
