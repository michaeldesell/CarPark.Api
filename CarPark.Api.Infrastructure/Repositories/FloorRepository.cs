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
    public class FloorRepository : IFloorRepository
    {

        CarParkDbContext Context;

        public FloorRepository(CarParkDbContext context)
        {
            Context = context;
        }

        public void Add(Floor f)
        {

            Context.Floors.Add(f);

        }

        public void Edit(Floor f)
        {
            throw new NotImplementedException();
        }

        public void Remove(Floor f)
        {
            Context.Floors.Remove(f);
        }

        public Floor GetFloor(int floorid)
        {
            var result = Context.Floors
                .Include(f => f.Carpark)
                .Include(f => f.Parkingspaces)
                .Where(f => f.Id == floorid).FirstOrDefault();
            return result;
        }

        public IEnumerable GetFloorsByCarpark(int carparkid)
        {
            var result = Context.Floors
                .Include(f => f.Carpark)
                .Include(f => f.Parkingspaces)
                .Where(f => f.Carpark.Id == carparkid).ToList();
            return result;

        }

        public Task<int> SaveAllAsync()
        {
            return Context.SaveChangesAsync();

        }
    }
}
