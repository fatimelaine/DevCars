using DevCars.API.InputModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        // POST api/customers
        [HttpPost]
        public IActionResult Post([FromBody] AddCustomerInputModel model)
        {
            return Ok();
        }

        // POST api/customers/1/orders
        [HttpPost("{id}/orders")]
        public IActionResult PostOrder(int id, [FromBody] AddOrderInputModel model)
        {
            return Ok();
        }

        // GET api/customers/1/orders/2
        [HttpGet("{id}/orders/{orderId}")]
        public IActionResult GetOrder(int id, int orderId)
        {
            return Ok();
        }
    }
}
