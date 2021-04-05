using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.ViewModels
{
    public class OrderDetailsViewModel
    {
        public OrderDetailsViewModel(int carId, int customerId, decimal totalCost, List<string> extraItems)
        {
            CarId = carId;
            CustomerId = customerId;
            TotalCost = totalCost;
            ExtraItems = extraItems;
        }

        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalCost { get; set; }
        public List<string> ExtraItems { get; set; }
    }
}
