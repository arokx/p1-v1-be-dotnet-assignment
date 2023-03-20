using Domain.Aggregates.AirportAggregate;
using Infrastructure;
using System.Collections.Generic;
using System;
using System.Linq;
using Domain.Aggregates.QuantityAggregate;

namespace API.Infrastructure.Seeds
{

    public class QuantitySeeder : FlightsContextSeeder
    {
        public QuantitySeeder(FlightsContext flightsContext) : base(flightsContext)
        {
        }

        public override void Seed()
        {
            if (FlightsContext.Quantity.Any())
            {
                Console.WriteLine("Skipping Airports seeder because table is not empty.");
                return;
            }

            var quantity = new List<Quantity>()
            {
                new Quantity(500)
            };

            FlightsContext.Quantity.AddRange(quantity);
            FlightsContext.SaveChanges();
        }
    }
}
