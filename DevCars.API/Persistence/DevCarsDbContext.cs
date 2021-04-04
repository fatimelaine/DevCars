using DevCars.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Persistence
{
    public class DevCarsDbContext
    {
        public DevCarsDbContext()
        {
            Cars = new List<Car>
            {
                new Car(1, "1123ABC", "HONDA", "CIVIC", 2021, 100000, "Cinza", new DateTime(2021, 1, 1)),
                new Car(2, "993ABC", "TOYOTA", "COROLLA", 2021, 95000, "AZUL", new DateTime(2021, 1, 1)),
                new Car(3, "131ABC", "CHEVROLET", "ONIX", 2021, 85000, "BRANCO", new DateTime(2021, 1, 1))
            };

            Customers = new List<Customer>
            {
                new Customer(1, "LUCIANO", "12345678", new DateTime(1990, 1, 1)),
                new Customer(2, "GUSTAVO", "995412344", new DateTime(1990, 1, 1)),
                new Customer(2, "GABRIEL", "1478421532", new DateTime(1990, 1, 1))
            };
        }
       
        public List<Car> Cars { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
