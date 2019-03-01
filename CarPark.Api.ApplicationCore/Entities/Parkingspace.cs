﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.Api.ApplicationCore.Entities
{
    public class Parkingspace
    {
        public int Id { get; set; }
        public Floor Floor { get; set; }
        public string Name { get; set; }
        public Car Car { get; set; }
        public bool Available { get; set; }

        public Parkingspace(string Name)
        {
            this.Name = Name;
            Available = true;
        }
    }
}
