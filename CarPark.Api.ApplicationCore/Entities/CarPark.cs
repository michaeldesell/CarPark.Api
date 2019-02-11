using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.Api.ApplicationCore.Entities
{
    public class Carpark
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public ICollection<Floor> Floors { get; set; }
        public int Amountparkedcars { get; set; }
        public int develop_pressure { get; set; }
        public int carpark_rating { get; set; }
        public int SpacesperFloor { get; set; }
        public int Carsarriving { get; set; }
        public int Carsleaving { get; set; }

        public Carpark(string name, User user)
        {
            User = user;
            Name = name;
            SpacesperFloor = 4;
            ICollection<Floor> floors = new List<Floor>();
            Amountparkedcars = 0;
            floors.Add(new Floor(1, SpacesperFloor));
            Floors = floors;
        }

        public void AddFloor()
        {
            int next = Floors.Count;
            next++;
            Floors.Add(new Floor(next, SpacesperFloor));
        }


    }
}
