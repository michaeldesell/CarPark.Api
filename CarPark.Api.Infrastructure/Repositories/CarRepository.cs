using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPark.Api.ApplicationCore.Entities;
using CarPark.Api.ApplicationCore.Interfaces;
using CarPark.Api.Infrastructure.EF_Core.DBContext;
using Microsoft.EntityFrameworkCore;

namespace CarPark.Api.Infrastructure.Repositories
{
    public class CarRepository : ICarRepository
    {
        CarParkDbContext Context;
        public CarRepository(CarParkDbContext context)
        {
            Context = context;
        }
        public void Add(Car c)
        {
            var result = Context.Cars.Add(c);
        }

        public void Edit(Car c)
        {
            throw new NotImplementedException();
        }

        public void Remove(Car c)
        {
            var result = Context.Cars.Remove(c);
        }

        public Car GetCar(int carid)
        {
            var result = Context.Cars
                .Where(c => c.Id == carid)
                .Include(cp => cp.Parkingspace)
                .Include(cp => cp.Carpark)
                .FirstOrDefault();
            return result;
        }

        public Car GetCarByParkingSpace(int parkingspaceid)
        {
            var result = Context.Cars.Where(c => c.Parkingspace.Id == parkingspaceid)
                .Include(cp => cp.Parkingspace)
                .Include(cp => cp.Carpark)
                .FirstOrDefault();
            return result;
        }

        public IEnumerable GetCarsByCarpark(int carparkid)
        {
            var result = Context.Cars.Where(c => c.Carpark.Id == carparkid)
                .Include(cp => cp.Parkingspace)
                .Include(cp => cp.Carpark)
                .ToList();
            return result;
        }

        public Task<int> SaveAllAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}
