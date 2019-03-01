﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.Api.ApplicationCore.Entities
{
    
    public class Car
    {
        
        public int Id { get; set; }
        public int ParkingspaceId { get; set; }
        public Parkingspace Parkingspace { get; set; }
        public Carpark Carpark { get; set; }
        public string Name { get; set; }

        public Car(string name)
        {
           Name = name;
        }

    }
}
