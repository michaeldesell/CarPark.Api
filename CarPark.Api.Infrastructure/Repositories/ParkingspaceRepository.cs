using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPark.Api.ApplicationCore.Entities;
using CarPark.Api.ApplicationCore.Models;
using CarPark.Api.ApplicationCore.Interfaces;
using CarPark.Api.Infrastructure.EF_Core.DBContext;
using AutoMapper;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarPark.Api.Infrastructure.Repositories
{
    public class ParkingspaceRepository : IParkingspaceRepository
    {
        CarParkDbContext Context;
        IMapper Mapper;

        public ParkingspaceRepository(CarParkDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public void Add(Parkingspace p)
        {
            Context.Parkingspaces.Add(p);
        }

        public void Edit(Parkingspace p)
        {
            throw new NotImplementedException();
        }

        public void Remove(Parkingspace p)
        {
            Context.Parkingspaces.Remove(p);
        }

        public Parkingspace FindById(int id)
        {
            var result = Context.Parkingspaces
               .Include(p => p.Car)
               .Include(p => p.Floor)
               .Where(p => p.Id == id).FirstOrDefault();
            return result;
        }

        public Parkingspace GetParkingspace(int pspaceid)
        {
            var result = Context.Parkingspaces
                .Include(p => p.Car)
                .Include(p => p.Floor)
                .Where(ps => ps.Id == pspaceid).FirstOrDefault();
            return result;
        }

        public IEnumerable GetPSpaceByFloor(int floorid)
        {
            var result = Context.Parkingspaces
                .Include(p => p.Car)
                .Include(p => p.Floor)
                .Where(ps => ps.Floor.Id == floorid).ToList();
            return result;
        }

        public Task<int> SaveAllAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}
