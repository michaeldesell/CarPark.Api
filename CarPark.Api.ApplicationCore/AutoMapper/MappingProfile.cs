using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CarPark.Api.ApplicationCore.Entities;
using CarPark.Api.ApplicationCore.Models;

namespace CarPark.Api.ApplicationCore.AutoMapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<User, UserModel>().ReverseMap().ForAllMembers(opt => opt.Condition(srs => srs != null));
            CreateMap<User, User>().ReverseMap().ForAllMembers(opt => opt.Condition(srs => srs != null));
            CreateMap<User, User>().ReverseMap().ForMember(u => u.Id,opt => opt.Ignore());
            CreateMap<Carpark, CarparkModel>().ReverseMap().ForAllMembers(opt => opt.Condition(srs => srs != null)); 
            CreateMap<Car, CarModel>().ReverseMap().ForAllMembers(opt => opt.Condition(srs => srs != null)); 
            CreateMap<Parkingspace, ParkingspaceModel>().ReverseMap().ForAllMembers(opt => opt.Condition(srs => srs != null));
        }

    }
}
