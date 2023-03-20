using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Aggregates.QuantityAggregate
{
    public class Quantity : Entity, IAggregateRoot
    {
        public Quantity(int quantity)
        {
            AvailableQuantity = quantity;
        }
        public Quantity()
        {
        }

        public int AvailableQuantity { get; set; }

    }
}
