using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.Api.ApplicationCore.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Car(string name)
        {
            Name = name;
        }

    }
}
