using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPark.Api.ApplicationCore.Entities;
using CarPark.Api.ApplicationCore.Interfaces;
using CarPark.Api.Infrastructure.EF_Core.DBContext;

namespace CarPark.Api.Infrastructure.Repositories
{
    public class ParkingspaceRepository : IParkingspaceRepository
    {
        CarParkDbContext _context;

        public ParkingspaceRepository(CarParkDbContext context)
        {
            _context = context;
        }

        public void Add(Parkingspace p)
        {
            _context.Parkingspaces.Add(p);
        }

        public void Edit(Parkingspace p)
        {
            throw new NotImplementedException();
        }

        public void Remove(Parkingspace p)
        {
            _context.Parkingspaces.Remove(p);
        }

        public Parkingspace FindById(int id)
        {
            var result = _context.Parkingspaces
               .Where(p => p.Id == id).FirstOrDefault();
            return result;
        }

       
    }
}
