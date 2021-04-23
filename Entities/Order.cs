using System.Collections.Generic;
using System.Linq;

namespace DevCars.Entities
{
    public class Order
    {
        public Order(int idCar, int idCustomer, decimal price, List<ExtraOrderItem> items)
        {
            IdCar = idCar;
            IdCustomer = idCustomer;
            TotalCost = items.Sum(i => i.Price) + price;

            ExtraItems = items;
        }

        protected Order() { }

        public int Id { get; set; }
        public int IdCar { get; set; }
        public Car Car { get; set; }
        public int IdCustomer { get; set; }
        public Customer Customer { get; set; }
        public decimal TotalCost { get; set; }
        public List<ExtraOrderItem> ExtraItems { get; set; }
    }

    public class ExtraOrderItem
    {
        protected ExtraOrderItem() { }
        public ExtraOrderItem(string description, decimal price)
        {
            Description = description;
            Price = price;
        }


        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int IdOrder { get; set; }

    }
}