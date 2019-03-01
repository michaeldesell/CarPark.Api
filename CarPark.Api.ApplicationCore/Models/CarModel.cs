using CarPark.Api.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarPark.Api.ApplicationCore.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int ParkingspaceId { get; set; }
        public Parkingspace Parkingspace { get; set; }
        public Carpark Carpark { get; set; }
        
    }
}
