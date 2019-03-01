﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.Api.ApplicationCore.Entities
{
    public class Carpark
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
        public bool Active { get; set; }
        public List<Floor> Floors { get; set; }
        public List<Car> Cars { get; set; }
        public int Amountparkedcars { get; set; }
        public int develop_pressure { get; set; }
        public int carpark_rating { get; set; }
        public int SpacesperFloor { get; set; }
        public int Carsarriving { get; set; }
        public int Carsleaving { get; set; }
        public Carpark()
        {
            Floors = new List<Floor>();
            Cars = new List<Car>();
        }
       

    }
}
