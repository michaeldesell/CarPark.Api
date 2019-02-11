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
    class CarParkRepository : ICarparkRepository
    {
        CarParkDbContext _context;

        public CarParkRepository(CarParkDbContext context)
        {
            _context = context;
        }

        public void Add(Carpark cp)
        {
            _context.Carparks.Add(cp);
        }

        public void Edit(Carpark cp)
        {
            throw new NotImplementedException();
        }

        public void Remove(Carpark cp)
        {
            _context.Carparks.Remove(cp);
        }

        public IEnumerable GetCarparks()
        {
            var result = _context.Carparks.ToList();
            return result;
        }

        public Carpark FindById(int id)
        {
            var result = _context.Carparks
                 .Where(cp => cp.Id == id).FirstOrDefault();
            return result;
        }

        public Carpark FindActiveByUserId(int userid)
        {
            var result = _context.Carparks
                .Where(cp => cp.User.Id == userid && cp.Active).FirstOrDefault();
            return result;
        }

        public IEnumerable FindByUserId(int userid)
        {
            var result = _context.Carparks
                .Where(cp => cp.User.Id == userid).ToList();
            return result;
        }

        public IEnumerable GetCarparkFloors(int id)
        {
            var result = _context.Carparks
                .Where(cp => cp.Id == id)
                .Select(cp => new { cp.Floors }).ToList();
            return result;
        }
    }
}
