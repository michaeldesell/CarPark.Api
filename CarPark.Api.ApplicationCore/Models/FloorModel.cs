using CarPark.Api.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarPark.Api.ApplicationCore.Models
{

    public class FloorModel
    {
        public int Id { get; set; }
        [Required]
        public Carpark Carpark { get; set; }
        [Required]
        public int Floornumber { get; set; }
        [Required]
        public int spaces { get; set; }
        public ICollection<Parkingspace> Parkingspaces { get; set; }
    }
}
