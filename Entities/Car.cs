using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.Entities
{
    public class Car
    {
        public Car(string vincode, string brand, string model, int year, decimal price, string color, DateTime productionDate)
        {
            Vincode = vincode;
            Brand = brand;
            Model = model;
            Year = year;
            Price = price;
            Color = color;
            ProductionDate = productionDate;

            Status = CarStatusEnum.Available;
            RegisteredAt = DateTime.Now;
        }

        public int Id { get; set; }
        public string Vincode { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public DateTime ProductionDate { get; set; }

        public CarStatusEnum Status { get; set; }
        public DateTime RegisteredAt { get; set; }

        public void Update(string color, decimal price)
        {
            Color = color;
            Price = price;
        }

        public void SetAsSuspended()
        {
            Status = CarStatusEnum.Suspended;
        }
    }
}
