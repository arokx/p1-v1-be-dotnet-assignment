using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates.OrderAggregate;
using Domain.Aggregates.QuantityAggregate;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositores
{
    public class QuantityRepository : IQuantityRepository
    {
        private readonly FlightsContext _context;

        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public QuantityRepository(FlightsContext context)
        {
            _context = context;
        }
        public async Task<List<Quantity>> GetAsync()
        {
            return await _context.Quantity.ToListAsync();
        }

        public void Update(Quantity quantity)
        {
            _context.Quantity.Update(quantity);
        }

     
    }


}
