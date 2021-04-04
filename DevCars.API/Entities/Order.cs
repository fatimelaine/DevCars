using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Entities
{
    public class Order
    {
        public Order(int id, int carId, int customerId, decimal totalCost)
        {
            Id = id;
            CarId = carId;
            CustomerId = customerId;
            TotalCost = totalCost;

            ExtraItems = new List<ExtraOrderItem>();
        }

        public int Id { get; private set; }
        public int CarId { get; private set; }
        public int CustomerId { get; private set; }
        public decimal TotalCost { get; private set; }
        public List<ExtraOrderItem> ExtraItems { get; private set; }


        public class ExtraOrderItem
        {
            public ExtraOrderItem(string description, decimal price)
            {
                Description = description;
                Price = price;
            }

            public int Id { get; private set; }
            public string Description { get; private set; }
            public decimal Price { get; private set; }
            public int OrderId { get; private set; }
        }
    }
}
