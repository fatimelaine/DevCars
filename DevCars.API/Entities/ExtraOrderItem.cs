using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Entities
{
    public class ExtraOrderItem
    {
        protected ExtraOrderItem() { }

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
