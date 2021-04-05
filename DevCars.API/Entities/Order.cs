using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Entities
{
    public class Order
    {
        protected Order() { }

        public Order(int carId, int customerId, decimal price, List<ExtraOrderItem> items)
        {
            CarId = carId;
            CustomerId = customerId;
            TotalCost = items.Sum(i => i.Price) + price;

            ExtraItems = items;
        }

        public int Id { get; private set; }
        public int CarId { get; private set; }
        public Car Car { get; private set; }
        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public decimal TotalCost { get; private set; }
        public List<ExtraOrderItem> ExtraItems { get; private set; }
    }
}
