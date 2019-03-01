using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarPark.Api.ApplicationCore.Entities;
using CarPark.Api.ApplicationCore.Models;
using CarPark.Api.ApplicationCore.Interfaces;
using CarPark.Api.Infrastructure.EF_Core.DBContext;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarPark.Api.Infrastructure.Repositories
{
    public class CarparkRepository : ICarparkRepository
    {
        public CarParkDbContext Context;
        public IMapper Mapper;

        public CarparkRepository(CarParkDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public void Add(Carpark cp)
        {
            Context.Carparks.Add(Mapper.Map<Carpark>(cp));
        }

        public void Edit(Carpark cp)
        {
            throw new NotImplementedException();
        }

        public void Remove(Carpark cp)
        {
            Context.Carparks.Remove(cp);
        }

        public IEnumerable GetCarparks()
        {
            var result = Context.Carparks.Include(cp => cp.User).Include(cp => cp.Cars).ToList();
            return result;
        }

        public Carpark GetCarpark(int carparkid)
        {
            var result = Context.Carparks.Where(cp => cp.Id == carparkid).Include(cp => cp.User).Include(cp => cp.Cars).FirstOrDefault();

            return result;
        }

        public IEnumerable GetCarparksByUser(string userid)
        {
            var result = Context.Carparks.Where(cp => cp.User.Id.Equals(userid)).Include(cp => cp.User).Include(cp => cp.Cars).ToList();
            return result;


        }

        public Task<int> SaveAllAsync()
        {
            return Context.SaveChangesAsync();

        }

    }
}
