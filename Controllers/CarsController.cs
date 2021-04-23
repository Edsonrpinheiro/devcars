using DevCars.Entities;
using DevCars.InputModels;
using DevCars.Persistence;
using DevCars.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.Controllers
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
            // Retorna lista de CarItemViewModel
            var cars = _dbContext.Cars;

            var carsViewModel = cars
                .Where(c => c.Status == CarStatusEnum.Available)
                .Select(c => new CarItemViewModel(c.Id, c.Brand, c.Model, c.Price))
                .ToList();

            return Ok(carsViewModel);

            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    var query = "SELECT Id, Brand, Model, Price FROM Cars WHERE Status = 0";

            //    var carsViewModel = sqlConnection.Query<CarItemViewModel>(query);

            //    return Ok(carsViewModel);
            //}
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);
            
            if (car == null)
            {
                return NotFound();    
            }

            var carDetailViewModel = new CarDetailViewModel(
                car.Id,
                car.Brand,
                car.Model,
                car.Vincode,
                car.Year,
                car.Price,
                car.Color,
                car.ProductionDate 
                ) ;

            return Ok(carDetailViewModel);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddCarInputModel model)
        {
            if (model.Model.Length > 50)
            {
                return BadRequest("Modelo não pode ter mais de 50 caracteres.");
            }

            var car = new Car(model.VinCode, model.Brand, model.Model, model.Year, model.Price, model.Color, model.ProductionDate);

            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();
            
            return CreatedAtAction(
                nameof(GetById),
                new { id = car.Id },
                model
                );
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCarInputModel model)
        {
            var car = _dbContext.Cars
                .SingleOrDefault(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            car.Update(model.Color, model.Price);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var car = _dbContext.Cars
                .SingleOrDefault(c => c.Id == id);

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
