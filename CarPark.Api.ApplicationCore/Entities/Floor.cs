﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarPark.Api.ApplicationCore.Entities
{
    public class Floor
    {
        public int Id { get; set; }
        public Carpark Carpark { get; set; }
        public int Floornumber { get; set; }
        public int spaces { get; set; }
        public ICollection<Parkingspace> Parkingspaces { get; set; }
               
    }
}
