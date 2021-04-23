using DevCars.Entities;
using DevCars.InputModels;
using DevCars.Persistence;
using DevCars.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.Controllers
{
    [Route("api/costumers")]
    public class CostumersController : ControllerBase
    {

        private readonly DevCarsDbContext _dbContext;
        public CostumersController(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddCustomerInputModel model)
        {
            var customer = new Customer(model.FullName, model.Document, model.BirthDate);
            _dbContext.Costumers.Add(customer);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public IActionResult PostOrder(int id, [FromBody] AddOrderInputModel model)
        {
            var extraItems = model.ExtraItems
                .Select(e => new ExtraOrderItem(e.Description, e.Price))
                .ToList();

            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == model.IdCar);

            var order = new Order(model.IdCar, model.IdCustomer, car.Price, extraItems);

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return CreatedAtAction(
                nameof(GetOrder),
                new { id = order.IdCustomer, orderId = order.Id },
                model
                );
        }

        [HttpGet("{id}/orders/{orderid}")]
        public IActionResult GetOrder(int id, int orderId)
        {
            var order = _dbContext.Orders.
                Include(o => o.ExtraItems)
                .SingleOrDefault(o => o.Id == orderId);

            if(order == null)
            {
                return NotFound();
            }

            var extraItems = order.ExtraItems.Select(e => e.Description).ToList();
            var orderViewMotel = new OrderDetailsViewModel(order.IdCar, order.IdCustomer, order.TotalCost, extraItems);

            return Ok(orderViewMotel);
        }

    }
}
