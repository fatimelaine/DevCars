using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
        private readonly DevCarsDbContext _dbContext;

        public CarsController(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/cars
        [HttpGet]
        public IActionResult Get()
        {
            var cars = _dbContext.Cars;

            var carsViewModel = cars
                .Select(c => new CarItemViewModel(c.Id, c.Brand, c.Model, c.Price))
                .ToList();

            return Ok(carsViewModel);
        }

        // GET api/cars/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        // POST api/cars
        [HttpPost]
        public IActionResult Post([FromBody] AddCarInputModel model)
        {
            return Ok();
        }

        // PUT api/cars/1
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCarInputModel model)
        {
            return Ok();
        }

        // DELETE api/cars/1
        [HttpDelete ("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }


    }
}
