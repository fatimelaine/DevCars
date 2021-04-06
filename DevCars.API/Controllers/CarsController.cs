﻿using Dapper;
using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
        private readonly string _connectionString;

        public CarsController(DevCarsDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;


            _connectionString = configuration.GetConnectionString("DevCarsCs");
        }

        // GET api/cars
        [HttpGet]
        public IActionResult Get()
        {
            var cars = _dbContext.Cars;

            var carsViewModel = cars
                .Where(c => c.Status == CarStatusEnum.Available)
                .Select(c => new CarItemViewModel(c.Id, c.Brand, c.Model, c.Price))
                .ToList();

            return Ok(carsViewModel);

            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    var query = "SELECT Id, Brand, Model, Price FROM Cars Where Status = 0";

            //    var carsViewModel = sqlConnection.Query<CarItemViewModel>(query); 

            //    return Ok(carsViewModel);
            //}
        }

        // GET api/cars/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);

            if(car == null)
            {
                return NotFound();
            }

            var carDetailsViewModel = new CarDetailsViewModel(car.Id, car.Brand, car.Model, car.Color, car.VinCode, car.Year, car.Price, car.ProductionDate);

            return Ok(carDetailsViewModel);
        }

        // POST api/cars
        /// <summary>
        /// Cadastrar um Carro
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// {
        ///     "brand": "Honda",
        ///     "model": "Civic",
        ///     "vinCode": "abc123",
        ///     "year": 2021,
        ///     "price": 100000,
        ///     "color": "Cinza",
        ///     "productionDate": "2021-04-05"
        /// }
        /// </remarks>
        /// <param name="model">Dados de entrada de um novo carro</param>
        /// <returns>Objeto recém-criado</returns>
        /// <reponse code="201">Objeto criado com sucesso.</reponse>
        /// <reponse code="400">Dados inválidos.</reponse>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] AddCarInputModel model)
        {
            if (model.Model.Length > 50)
            {
                return BadRequest("Modelo não pode ter mais de 50 caracteres.");
            }

            var car = new Car(model.VinCode, model.Brand, model.Model, model.Year, model.Price, model.Color, model.ProductionDate);

            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = car.Id }, model);
        }

        // PUT api/cars/1
        /// <summary>
        /// Atualizar dados de um Carro
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// {
        ///     "color": "Branco",
        ///     "price": 100000
        /// }
        /// </remarks>
        /// <param name="id">Identificador de um Carro</param>
        /// <param name="model">Dados de alteração</param>
        /// <returns>Não tem retorno.</returns>
        /// <response code="204">Atualização bem-sucedida</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Carro não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] UpdateCarInputModel model)
        {
            if (model.Price < 0)
            {
                return BadRequest("Este é um valor inválido.");
            }

            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            car.Update(model.Color, model.Price);
            _dbContext.SaveChanges();

            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    var query = "UPDATE Cars SET Color = @color, Price = @price WHERE Id = @id";

            //    sqlConnection.Execute(query, new { color = car.Color, price = car.Price, car.Id });
            //}

                return NoContent();
        }

        // DELETE api/cars/1
        [HttpDelete ("{id}")]
        public IActionResult Delete(int id)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            car.SetAsSuspended();
            _dbContext.SaveChanges();

            return NoContent();
        }


    }
}
