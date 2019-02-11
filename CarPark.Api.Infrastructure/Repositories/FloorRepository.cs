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
    public class FloorRepository : IFloorRepository
    {

        CarParkDbContext _context;

        public FloorRepository(CarParkDbContext context)
        {
            _context = context;
        }

        public void Add(Floor f)
        {

            _context.Floors.Add(f);

        }

        public void Edit(Floor f)
        {
            throw new NotImplementedException();
        }

        public void Remove(Floor f)
        {
            _context.Floors.Remove(f);
        }

        public Floor FindById(int id)
        {
            var result = _context.Floors
                .Where(f => f.Id == id).FirstOrDefault();
            return result;
        }

        public IEnumerable GetFloorParkingspaces(int id)
        {
            var result = _context.Floors
                 .Where(f => f.Id == id)
                 .Select(f => new { f.Parkingspaces }).ToList();
            return result;
        }
    }
}
