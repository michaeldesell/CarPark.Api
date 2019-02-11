using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarPark.Api.ApplicationCore.Entities;
using CarPark.Api.ApplicationCore.Interfaces;
using CarPark.Api.Infrastructure.EF_Core.DBContext;

namespace CarPark.Api.Infrastructure.Repositories
{
    class CarRepository : ICarRepository
    {
        CarParkDbContext _context;
        public CarRepository(CarParkDbContext context)
        {
            _context = context;
        }
        public void Add(Car c)
        {
            var result = _context.Cars.Add(c);
        }

        public void Edit(Car c)
        {
            throw new NotImplementedException();
        }

        public void Remove(Car c)
        {
            var result = _context.Cars.Remove(c);
        }

        public Car FindById(int id)
        {
            var result = _context.Cars
                .Where(c => c.Id == id).FirstOrDefault();
            return result;
        }

        public IEnumerable GetCars()
        {
            var result = _context.Cars.ToList();
            return result;
        }

    }
}
