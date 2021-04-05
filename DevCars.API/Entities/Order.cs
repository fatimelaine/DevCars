using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Entities
{
    public class Order
    {
        public Order(int id, int carId, int customerId, decimal price, List<ExtraOrderItem> items)
        {
            Id = id;
            CarId = carId;
            CustomerId = customerId;
            TotalCost = items.Sum(i => i.Price) + price;

            ExtraItems = items;
        }

        public int Id { get; private set; }
        public int CarId { get; private set; }
        public int CustomerId { get; private set; }
        public decimal TotalCost { get; private set; }
        public List<ExtraOrderItem> ExtraItems { get; private set; }
    }
}
