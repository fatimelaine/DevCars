using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly DevCarsDbContext _dbContext;

        public CustomersController(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST api/customers
        [HttpPost]
        public IActionResult Post([FromBody] AddCustomerInputModel model)
        {
            var customer = new Customer(model.FullName, model.Document, model.BirthDate);

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return NoContent();
        }

        // POST api/customers/1/orders
        [HttpPost("{id}/orders")]
        public IActionResult PostOrder(int id, [FromBody] AddOrderInputModel model)
        {
            var extraItems = model.ExtraItems
                .Select(e => new ExtraOrderItem(e.Description, e.Price))
                .ToList();

            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == model.CarId);

            var order = new Order(model.CarId, model.CustomerId, car.Price, extraItems);

            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == model.CustomerId);

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetOrder), new { id = order.CustomerId, orderId = order.Id }, model);
        }

        // GET api/customers/1/orders/2
        [HttpGet("{id}/orders/{orderId}")]
        public IActionResult GetOrder(int id, int orderId)
        {
            var order = _dbContext.Orders
                .Include(o => o.ExtraItems)
                .SingleOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }
            
            var extraItems = order.ExtraItems.Select(e => e.Description).ToList();

            var orderViewModel = new OrderDetailsViewModel(order.CarId, order.CustomerId, order.TotalCost, extraItems);

            return Ok(orderViewModel);
        }
    }
}
