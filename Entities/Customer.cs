using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.Entities
{
    public class Customer
    {
        public Customer(string fullName, string document, DateTime birthDate)
        {
            FullName = fullName;
            Document = document;
            BirthDate = birthDate;

            Orders = new List<Order>();
        }

        protected Customer() { }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Document { get; set; }
        public DateTime BirthDate { get; set; }

        public List<Order> Orders { get; set; }

        public void Purchase(Order order)
        {
            Orders.Add(order);
        }
    }
}
