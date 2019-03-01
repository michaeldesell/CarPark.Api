using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CarPark.Api.ApplicationCore.Entities;

namespace CarPark.Api.ApplicationCore.Models
{


    public class ParkingspaceModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Car Car { get; set; }
        [Required]
        public bool Available { get; set; }

    }
}
