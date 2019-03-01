using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CarPark.Api.ApplicationCore.Entities;


namespace CarPark.Api.ApplicationCore.Models
{
    public class CarparkModel
    {
        public int Id { get; set; }
        public User User { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool Active { get; set; }
        public ICollection<Floor> Floors { get; set; }
        public int Amountparkedcars { get; set; }
        public int develop_pressure { get; set; }
        public int carpark_rating { get; set; }
        [Required]
        public int SpacesperFloor { get; set; }
        public int Carsarriving { get; set; }
        public int Carsleaving { get; set; }
    }
}
