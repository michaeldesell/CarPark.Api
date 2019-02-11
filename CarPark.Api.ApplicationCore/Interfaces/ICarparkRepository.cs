using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CarPark.Api.ApplicationCore.Entities;

namespace CarPark.Api.ApplicationCore.Interfaces
{
    public interface ICarparkRepository
    {
        void Add(Carpark cp);
        void Edit(Carpark cp);
        void Remove(Carpark cp);
        IEnumerable GetCarparks();
        Carpark FindById(int id);
        Carpark FindActiveByUserId(int userid);
        IEnumerable FindByUserId(int userid);
        IEnumerable GetCarparkFloors(int id); 
    }
}
